namespace SurveyApp.Models
{
    public class SurveyDetail
    {
        public long SurveyDetailId { get; set; }
        public long SurveyId { get; set; }
        public SurveyType Type { get; set; }
        public bool Dependent { get; set; }
        public long DependentItemID { get; set; }
        public string Dependancy { get; set; }
        public string Prompt { get; set; }
        public string DelimitedItemList { get; set; }
    }
    public enum SurveyType
    {
        FreeText,
        Radio,
        CheckBox
    }
}