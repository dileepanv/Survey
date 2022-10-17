using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Survey_project.Models;
using Survey_project.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpGet("[action]/{Id}")]
        public async Task<ServiceResponse<List<QuestionModel>>> GetAllQuestionsBySurveyId( int Id)
        {
            var record = await _surveyService.GetAllQuestionBySurveyId(Id);
            return record;
        }

        [HttpGet("[action]/{surveyId}/{questionId}")]
        public async Task<ServiceResponse<QuestionModel>> GetQuestion(int surveyId, int questionId)
        {
            var result = await _surveyService.GetQuestionByIdBySurveyId(surveyId, questionId);
            return result;
        }

        [HttpGet("[action]")]
        public async Task<ServiceResponse<List<SurveyModel>>> GetAllSurvey()
        {
            var record = await _surveyService.GetAllSurvey();
            return record;
        }

        [HttpGet("[action]/{id}")]
        public async Task<ServiceResponse<List<SurveyModel>>> GetAllSurveyByCategoryId( int id)
        {
            var record = await _surveyService.GetAllSurveyByCategoryId(id);
            return record;
        }

        [HttpGet("[action]")]
        public async Task<ServiceResponse<List<SurveyModel>>> GetPopularSurvey(bool IsPopular)
        {
            var record = await _surveyService.GetPopularSurvey(IsPopular);
            return record;
        }

        [HttpPost("[action]")]
        public async Task<ApiResponse> AddUseAnswer(UserAnserModel userAnserModel )
        {
            var record = await _surveyService.AddUserAnswer(userAnserModel);
            return record;
        }
    }
}
