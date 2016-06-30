using System.Collections.Generic;

namespace SurveyApp.Models
{
    public class SurveyClient
    {
        public long SurveyClientId { get; set; }
        public string UserName { get; set; }
        public string PasswordMd5 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public long SurveyResultId { get; set; }

        public ICollection<AnalystSurvey> AnalystSurvey { get; set; }
    }
}