using Microsoft.AspNetCore.Mvc;
using Survey_project.Models;
using Survey_project.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace Survey_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }


        [HttpPost("[action]")]
        public async Task<ApiResponse> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return new ApiResponse("Invalid Input", 400);
            return await _accountServices.Register(model);
        }

        [HttpPost("[action]")]
        public async Task<ServiceResponse<LoginResponse>> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return new ServiceResponse<LoginResponse>(400, "All fields are mandatory", null);
            return await _accountServices.Login(model);
        }

        [HttpPost("[action]")]
        public async Task<ServiceResponse<ForgetPasswordResponse>> ForgotPassword(ForgotPasswordModel model)
        {
            return await _accountServices.ForgotPassword(model);
        }

        [HttpPost("[action]")]
        public async Task<ApiResponse> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return new ApiResponse("all fields are mandatory", 400);
            return await _accountServices.ResetPassword(model);
        }

        [HttpPost("[action]")]
        public async Task<ApiResponse> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return new ApiResponse("all fields are mandatory", 400);
            return await _accountServices.ChangePassword(model);
        }
    }
}


   
