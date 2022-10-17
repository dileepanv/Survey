using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survey_project.Entities;
using Survey_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey_project.Repository
{
    public class SurveyService : ISurveyService
    {
        private readonly SurveyDbContext _context;
        private readonly IUserServices _userService;

        public SurveyService(SurveyDbContext context, IUserServices userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<ServiceResponse<List<QuestionModel>>> GetAllQuestionBySurveyId(int Id)
        {
            var question = await _context.Question.Where(a => a.SurveyId == Id).Select(s => new QuestionModel
            {
                SurveyId = s.SurveyId,
                Surveyname = _context.Survey.Where(b => b.SurveyId == Id).Select(v => v.SurveyName).FirstOrDefault(),
                QuestionId = s.QuestionId,
                QuestionName = s.QuestionName,
                ImagePath = _context.ImageUrlDetail.FirstOrDefault(x => x.SurveyId.Equals(s.SurveyId) && x.QuestionId.Equals(s.QuestionId)).ImageUrl,
                OptionModel = _context.Options.Where(x => x.QuestionId == s.QuestionId).Select(s => new OptionModel
                {
                    OptionId = s.OptionId,
                    OptionName = s.OptionName
                }).ToList()
            }).ToListAsync();

            if (question.Count != 0)
            {
                return new ServiceResponse<List<QuestionModel>>(200, "Successful", question);
               
            }
            return new ServiceResponse<List<QuestionModel>>(404, "Please enter correct survey id", null);
        }


        public async Task<ServiceResponse<QuestionModel>> GetQuestionByIdBySurveyId(int surveyId, int questionId)
        {
            var question = await _context.Question
                .Where(a => a.SurveyId == surveyId)
                .Where(n => n.QuestionId == questionId).Select(s => new QuestionModel
                {
                    SurveyId = s.SurveyId,
                    Surveyname = _context.Survey.Where(b => b.SurveyId == surveyId).Select(v => v.SurveyName).FirstOrDefault(),
                    QuestionId = s.QuestionId,
                    QuestionName = s.QuestionName,
                    ImagePath = _context.ImageUrlDetail.FirstOrDefault(x => x.SurveyId.Equals(s.SurveyId) && x.QuestionId.Equals(s.QuestionId)).ImageUrl,
                    OptionModel = _context.Options.Where(x => x.QuestionId == questionId).Select(s => new OptionModel
                    {
                        OptionId = s.OptionId,
                        OptionName = s.OptionName
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (question is not null)
            {
                return new ServiceResponse<QuestionModel>(200, "Successful", question);
                
            }
            return new ServiceResponse<QuestionModel>(204, "No Content", question);
        }


        public async Task<ServiceResponse<List<SurveyModel>>> GetAllSurvey()
        {
            var survey = await _context.Survey.Select(e => new SurveyModel
            {
                CategoryId=e.CategoryId,
                CategoryName=_context.Category.Where(x=>x.CategoryId.Equals(e.CategoryId)).Select(n=>n.CategoryName).FirstOrDefault(),
                SurveyId = e.SurveyId,
                SurveyName = e.SurveyName,
                CreateDate = e.CreateDate,
                SurveyImagePath = _context.ImageUrlDetail.FirstOrDefault(x => x.SurveyId.Equals(e.SurveyId)).ImageUrl
            }).ToListAsync();
            if (survey != null)
            {
                return new ServiceResponse<List<SurveyModel>>(200, "Successful", survey);
            }
            else
            {
                return new ServiceResponse<List<SurveyModel>>(204, "No content", null);
            }
        }


        public async Task<ServiceResponse<List<SurveyModel>>> GetAllSurveyByCategoryId(int Id)
        {
            var survey = await _context.Survey.Where(x => x.CategoryId == Id).Select(e => new SurveyModel
            {

                CategoryId = e.CategoryId,
                CategoryName = _context.Category.Where(x => x.CategoryId.Equals(e.CategoryId)).Select(n => n.CategoryName).FirstOrDefault(),
                SurveyId = e.SurveyId,
                SurveyName = e.SurveyName,
                CreateDate = e.CreateDate,
                SurveyImagePath = _context.ImageUrlDetail.FirstOrDefault(x => x.SurveyId.Equals(e.SurveyId)).ImageUrl
            }).ToListAsync();
            if (survey.Count() is not 0)
            {
                return new ServiceResponse<List<SurveyModel>>(200, "Successful", survey);
            }
            else
            {
                return new ServiceResponse<List<SurveyModel>>(404, "Please enter correct category id", null);
            }
        }

        public async Task<ServiceResponse<List<SurveyModel>>> GetPopularSurvey(bool isPopular)
        {
            if (isPopular.Equals(true))
            {
                var survey = await _context.Survey
                    .Where(x => x.Ispopular.Equals(isPopular)).Select(e => new SurveyModel
                    {

                        CategoryId = e.CategoryId,
                        CategoryName = _context.Category.Where(x => x.CategoryId.Equals(e.CategoryId)).Select(n => n.CategoryName).FirstOrDefault(),
                        SurveyId = e.SurveyId,
                        SurveyName = e.SurveyName,
                        CreateDate = e.CreateDate,
                        SurveyImagePath = _context.ImageUrlDetail.FirstOrDefault(x => x.SurveyId.Equals(e.SurveyId)).ImageUrl
                    }).ToListAsync();
                if (survey.Count is not 0)
                {
                    return new ServiceResponse<List<SurveyModel>>(200, "Successful", survey);
                }

                return new ServiceResponse<List<SurveyModel>>(204, "No content", null);
            }
            return new ServiceResponse<List<SurveyModel>>(200, "null", null);
        }


        public async Task<ApiResponse> AddUserAnswer(UserAnserModel userAnserModel)
        {
            var check = _context.Options.Where(s => s.QuestionId == userAnserModel.QuestionId && s.OptionId == userAnserModel.OptionId).FirstOrDefaultAsync();
            if (check is not null)
            {
               
                var UserAnswer = new UserAnswer();
                {
                    UserAnswer.UserId = _userService.UserID;
                    UserAnswer.QuestionId = userAnserModel.QuestionId;
                    UserAnswer.OptionId = userAnserModel.OptionId;
                    UserAnswer.CreateDate = DateTime.Now;
                }

                _context.UserAnswer.Add(UserAnswer);
                await _context.SaveChangesAsync();

                return new ApiResponse("Successfully added", 200);
            }

            return new ApiResponse("Please enter correct data",404);
            

            
        }

    }
}















































