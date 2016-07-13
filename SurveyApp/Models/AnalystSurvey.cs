using System;
using System.Collections.Generic;

namespace SurveyApp.Models
{
    public class AnalystSurvey
    {
        public long AnalystSurveyId { get; set; }
        public long ClientId { get; set; }
        public DateTime Created { get; set; }
        public long AnalystSurveyTypeId { get; set; }   
        public string Response { get; set; }
        public string InternalNotes { get; set; }
    }
}