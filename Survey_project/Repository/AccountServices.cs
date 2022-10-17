using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Survey_project.Entities;
using Survey_project.GlobalVariables;
using Survey_project.Models;
using Survey_project.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Survey_project.Repository
{
    public class AccountServices : IAccountServices
    {
        private readonly SurveyDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AccountServices(SurveyDbContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        //public IQueryable<RoleModel> GetRoles()
        //{
        //    return _context.Role.Select(s => new RoleModel
        //    {
        //        Id = s.Id,
        //        RoleName = s.Name
        //    });
        //}

        public async Task<ApiResponse> Register(RegisterModel model)
        {
            var checkUser = await _context.User.Where(r => r.Email == model.Email).FirstOrDefaultAsync();
            if (checkUser != null)
                return new ApiResponse("The email already exists", 400);

            var user = new User
            {
                Email = model.Email,
                Password = Encipher(model.Password),
                Name = model.Name,
                CreatedDate = DateTime.UtcNow
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            UserRole role = new()
            {
                RoleId = model.RoleId,
                UserId = user.Id
            };

            await _context.AddAsync(role);
            await _context.SaveChangesAsync();


            PasswordResetToken token = new()
            {
                Token = Guid.NewGuid().ToString(),
                Email = user.Email,
                ExpiryDate = DateTime.Now.AddDays(3),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            await _context.PasswordResetToken.AddAsync(token);
            await _context.SaveChangesAsync();

            return new ApiResponse("Register successfull", 200);
        }
        public async Task<ServiceResponse<LoginResponse>> Login(LoginModel model)
        {
            var checkUser = await _context.User.Where(u => u.Email == model.Email).Include(u => u.UserRole).ThenInclude(u => u.Role).FirstOrDefaultAsync();
            if (checkUser is null)
                return new ServiceResponse<LoginResponse>(400, Constants.CheckEmail, null);

            if ((Decipher(checkUser?.Password) == model.Password))
            {
                var response = new LoginResponse();
                List<string> roles = checkUser.UserRole.Select(x => x.Role.Name).ToList();
                response.Roles = roles;
                response.Username = $"{checkUser.Email}";
                var refreshToken = GenerateRefreshToken();
                checkUser.RefreshToken.Add(refreshToken);
                await _context.SaveChangesAsync();
                response.AccessToken = AccessToken(checkUser, roles);
                response.ExpiresIn = 43200000;
                response.TokenType = "bearer";
                response.RefreshToken = refreshToken.Token;
                return new ServiceResponse<LoginResponse>(200, "login Using Token", response);
            }
            else
            {
                if (checkUser == null)
                {
                    return new ServiceResponse<LoginResponse>(400, "Enter valid email", null);
                }
                else
                {
                    return new ServiceResponse<LoginResponse>(400, "Enter valid password", null);

                }

            }


        }

        public async Task<ServiceResponse<ForgetPasswordResponse>> ForgotPassword(ForgotPasswordModel model)
        {
            try
            {
                var appuser = await _context.User.Where(x => x.Email == model.Email).FirstOrDefaultAsync();
                var response = new ForgetPasswordResponse();
                List<string> roles = appuser.UserRole.Select(x => x.Role.Name).ToList();
                response.Email = $"{appuser.Email}";
                var verificaction = _context.Verification.Where(x => x.UserId == appuser.Id && DateTime.Now <= x.CreatedDate.AddMinutes(5)).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(model.LoginOtp))
                {
                    var otp = GenerateRandomOTP();
                    var saveOtp = SaveOtp(otp, appuser.Id);
                    await _emailService.SendMail("Survey - your otp is", model.Email, otp);
                    return new ServiceResponse<ForgetPasswordResponse>(202, "otp sent to email", response);
                }
                else if (verificaction.CreatedDate <= verificaction.CreatedDate.AddMinutes(5) && string.IsNullOrEmpty(model.LoginOtp) == true)
                {
                    return new ServiceResponse<ForgetPasswordResponse>(202, "otp has already been sent. Please check the mail", response);
                }
                {
                    var code = _context.Verification.Where(x => x.UserId == appuser.Id).OrderByDescending(s => s.Id).Select(s => s.Code).FirstOrDefault();
                    if (model.LoginOtp == verificaction.Code)
                    {
                        verificaction.IsOtpVerified = true;
                        _context.Update(appuser);
                        await _context.SaveChangesAsync();
                    }
                    else
                        return new ServiceResponse<ForgetPasswordResponse>(401, "Otp is Invalid", response);
                }
                if (appuser != null)
                {
                    var token = new PasswordResetToken()
                    {
                        Token = Guid.NewGuid().ToString(),
                        Email = model.Email,
                        ExpiryDate = DateTime.UtcNow.AddHours(3)
                    };
                    response.AccessToken = token.Token;
                    var email = System.Text.Encoding.UTF8.GetBytes(appuser.Email);
                    await _context.PasswordResetToken.AddAsync(token);
                    await _context.SaveChangesAsync();

                    return new ServiceResponse<ForgetPasswordResponse>(200, " Otp Succesfully validated", response);
                }
                return new ServiceResponse<ForgetPasswordResponse>(400, Constants.CheckEmail, response);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ForgetPasswordResponse>(400, ex.Message, null);
            }
        }
        public async Task<ApiResponse> ResetPassword(ResetPasswordModel model)
        {
            var appuser = await _context.User.Where(x => x.Email == model.Email).FirstOrDefaultAsync();
            if (appuser != null)
            {
                var prt = await _context.PasswordResetToken.Where(x => x.Email == model.Email && x.Token == model.Token).FirstOrDefaultAsync();
                if (prt != null)
                {
                    appuser.Password = Encipher(model.Password);
                    _context.Entry(appuser).State = EntityState.Modified;
                    _context.Remove(prt);
                    await _context.SaveChangesAsync();
                    return new ApiResponse(Constants.SuccessResponse, 200);
                }
            }
            return new ApiResponse("Wrong ResetToken Token And Wrong Email or password", 404);
        } 

        public async Task<ApiResponse> ChangePassword(ChangePasswordModel model)
        {
            var appuser = await _context.User.Where(x => x.Email == model.CurrentEmailId).FirstOrDefaultAsync();
            if (appuser is not null && (Decipher(appuser.Password) == model.Password))
            {
                appuser.Password = Encipher(model.Password);
                _context.Entry(appuser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ApiResponse("password has been changed successfully", 200);
            }
            else
                return new ApiResponse("please enter a valid password", 400);
        }
    




        private string Encipher(string password)
        {
            string key = "abcdefghijklmnopqrstuvwxyz1234567890";
            byte[] bytesBuff = Encoding.Unicode.GetBytes(password);
            using (System.Security.Cryptography.Aes aes = System.Security.Cryptography.Aes.Create())
            {
                Rfc2898DeriveBytes crypto = new(key,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                aes.Key = crypto.GetBytes(32);
                aes.IV = crypto.GetBytes(16);
                using MemoryStream mStream = new();
                using (CryptoStream cStream = new(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cStream.Write(bytesBuff, 0, bytesBuff.Length);
                    cStream.Close();
                }
                password = Convert.ToBase64String(mStream.ToArray());
            }
            return password;
        }

        private string AccessToken(User appUser, List<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtOptions:SecurityKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration.GetValue<string>("JwtOptions:Issuer"),
                Audience = _configuration.GetValue<string>("JwtOptions:Audience"),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, appUser.Email),
                    new Claim(ClaimTypes.NameIdentifier, $"{appUser.Id}"),
                    new Claim("roles", JsonConvert.SerializeObject(roles))
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            roles.ForEach(x => tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, x)));
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static RefreshToken GenerateRefreshToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string Decipher(string password)
        {
            string key = "abcdefghijklmnopqrstuvwxyz1234567890";
            password = password.Replace(" ", "+");
            byte[] bytesBuff = Convert.FromBase64String(password);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes crypto = new(key,
                new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                aes.Key = crypto.GetBytes(32);
                aes.IV = crypto.GetBytes(16);
                using MemoryStream mStream = new();
                using (CryptoStream cStream = new(mStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cStream.Write(bytesBuff, 0, bytesBuff.Length);
                    cStream.Close();
                }
                password = Encoding.Unicode.GetString(mStream.ToArray());
            }
            return password;
        }


        private Verification SaveOtp(string otp, int userId)
        {
            Verification verification = new()
            {
                Code = otp,
                CreatedDate = DateTime.Now,
                UserId = userId
            };
            _context.Verification.Add(verification);
            _context.SaveChangesAsync();
            return verification;


        }

        private string GenerateRandomOTP()
        {
            var otpChars = "1234567890";
            var r = new Random();
            var RandomChars = new char[6];
            for (int i = 0; i <= RandomChars.Length - 1; i++)
            {
                RandomChars[i] = otpChars[r.Next(otpChars.Length)];
            }
            var RandomString = new string(RandomChars);
            return RandomString;
        }

    }
}
