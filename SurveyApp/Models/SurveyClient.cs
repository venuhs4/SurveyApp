using System.Collections.Generic;

namespace SurveyApp.Models
{
    public class SurveyClient
    {
        public long SurveyClientId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }

        //public ICollection<SurveyResult> SurveyResult { get; set; }
    }
}