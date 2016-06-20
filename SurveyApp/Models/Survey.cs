using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurveyApp.Models
{
    public class Survey
    {
        public long SurveyId { get; set; }
        [MaxLength(50)]
        public string Duty { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string LinkId { get; set; }

        public ICollection<SurveyDetail> SurveyDetails { get; set; }
        public ICollection<SurveyResult> SurveyResults { get; set; }
        public ICollection<SurveyGroup> SurveyGroups { get; set; }
    }
}