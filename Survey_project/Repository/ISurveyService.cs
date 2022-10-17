using Survey_project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey_project.Repository
{
    public interface ISurveyService
    {
        Task<ServiceResponse<List<QuestionModel>>> GetAllQuestionBySurveyId(int Id);
        Task<ServiceResponse<QuestionModel>> GetQuestionByIdBySurveyId(int surveyId, int questionId);
        Task<ServiceResponse<List<SurveyModel>>> GetAllSurvey();
        Task<ServiceResponse<List<SurveyModel>>> GetAllSurveyByCategoryId(int Id);
        Task<ServiceResponse<List<SurveyModel>>> GetPopularSurvey(bool isPopular);
        Task<ApiResponse> AddUserAnswer(UserAnserModel userAnserModel);
    }
}
