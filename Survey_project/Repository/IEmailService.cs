using Survey_project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey_project.Services
{
    public interface IEmailService
    {
        Task<ServiceResponse<ApiResponse>> ForgotPasswordEmail(string name, string email, string url);

        Task<ApiResponse> VerifyRegistrationEmail(string name, string email, string url);

        public Task<ApiResponse> PaymentEmail(string email, string url);

        public Task<string> SendMail(string subject, string email, string content, List<string> attachments = null);

        public Task<string> SendMail(string subject, List<string> email, string content, List<string> attachments = null);

        public Task PaymentSendMail(string subject, string email, string content, List<string> attachments = null);

        public Task<ApiResponse> VerifySellerEmail(string name, string email, string url);
    }
}