using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurveyApp.Models
{
    public class AnalystSurveyType
    {
        public long AnalystSurveyTypeId { get; set; }
        public string AnalystTypeName { get; set; }
        public string Description { get; set; }

        public ICollection<AnalystSurvey> AnalystSurvey { get; set; }
    }
}