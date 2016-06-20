namespace SurveyApp.Models
{
    public class Survey
    {
        public long SurveyId { get; set; }
        public string Duty { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LinkId { get; set; }
    }
}