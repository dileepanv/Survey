using Newtonsoft.Json;
using System;

namespace Survey_project.Models
{
    public class SurveyModel
    {
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("surveyId")]
        public int SurveyId { get; set; }

        [JsonProperty("surveyName")]
        public string SurveyName { get; set; }

        [JsonProperty("surveyImagePath")]
        public string SurveyImagePath { get; set; }

        [JsonProperty("createDate")]
        public DateTime CreateDate { get; set; }
    }
}
