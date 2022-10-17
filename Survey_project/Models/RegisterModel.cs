using Newtonsoft.Json;

namespace Survey_project.Models
{
    public class RegisterModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("roleId")]
        public int RoleId { get; set; }
    }

    public class LoginModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

    }
    public class VerifyOtpModel
    {

        [JsonProperty("loginOtp")]
        public string LoginOtp { get; set; }
    }

    public class ChangePasswordModel
    {
        [JsonProperty("currentEmailId")]
        public string CurrentEmailId { get; set; }

        [JsonProperty("currentPassword")]
        public string CurrentPassword { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
    public class ForgotPasswordModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("loginOtp")]
        public string LoginOtp { get; set; }

    }
    public class ResetPasswordModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }

    public class RefreshTokenModel
    {
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }

    public class ForgetPasswordResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

    }
}
    