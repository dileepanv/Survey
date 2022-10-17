using Survey_project.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Survey_project.Repository
{
    public interface IAccountServices
    {
        //IQueryable<RoleModel> GetRoles();

        Task<ApiResponse> Register(RegisterModel model);

        Task<ServiceResponse<LoginResponse>> Login(LoginModel model);

        Task<ApiResponse> ChangePassword(ChangePasswordModel model);

        Task<ApiResponse> ResetPassword(ResetPasswordModel model);

        Task<ServiceResponse<ForgetPasswordResponse>> ForgotPassword(ForgotPasswordModel model);

    }
}