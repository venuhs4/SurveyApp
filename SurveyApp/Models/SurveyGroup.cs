using System.Collections.Generic;

namespace SurveyApp.Models
{
    public class SurveyGroup
    {
        public long SurveyGroupId { get; set; }
        public string SurveyGroupName { get; set; }

        public ICollection<SurveyGroupMap> SurveyGroupsMap { get; set; }
    }
}