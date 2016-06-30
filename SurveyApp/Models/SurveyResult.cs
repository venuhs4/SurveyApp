using System;
using System.Collections.Generic;

namespace SurveyApp.Models
{
    public class SurveyResult
    {
        public long SurveyResultId { get; set; }
        public long SurveyId { get; set; }                              
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<SurveyClient> SurveyClient { get; set; }
    }
}