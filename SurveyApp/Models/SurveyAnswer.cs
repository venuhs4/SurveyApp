namespace SurveyApp.Models
{
    public class SurveyAnswer
    {
        public long SurveyAnswerId { get; set; }
        public long SurveyDetailId { get; set; }
        public long SurveyResultId { get; set; }
        public string SurveyDetailAnswer { get; set; }
        public bool RegisteredUser { get; set; }
    }
}