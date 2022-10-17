using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Survey_project.Models
{
    public class QuestionModel
    {
        [JsonProperty("surveyId")]
        public int SurveyId { get; set; }

        [JsonProperty("surveyname")]
        public string Surveyname { get; set; }

        [JsonProperty("questionId")]
        public int QuestionId { get; set; }

        [JsonProperty("questionName")]
        public string QuestionName { get; set; }

        [JsonProperty("imagePath")]
        public string ImagePath { get; set; }

        [JsonProperty("optionModel")]
        public List<OptionModel> OptionModel { get; set; }
        
    }

    public class OptionModel
    {
        [JsonProperty("optionId")]
        public int OptionId { get; set; }

        [JsonProperty("optionName")]
        public string OptionName { get; set; }
    }


    public class UserAnserModel
    { 
        [JsonProperty("questionId")]
      
        public int QuestionId { get; set; }

        [JsonProperty("optionId")]

        public int OptionId { get; set; }
    }
}
