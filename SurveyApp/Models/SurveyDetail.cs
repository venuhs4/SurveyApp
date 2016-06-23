using System;
using System.Collections.Generic;

namespace SurveyApp.Models
{
    public class SurveyDetail
    {
        public long SurveyDetailId { get; set; }
        public long SurveyId { get; set; }
        public int SortingIndex { get; set; }
        public SurveyType Type { get; set; }
        public bool Dependent { get; set; }
        public long DependentItemID { get; set; }
        public string Dependancy { get; set; }
        public string Prompt { get; set; }
        public string DelimitedItemList { get; set; }

        public static Dictionary<int, string> GetServeyTypes()
        {
            var list = (SurveyType[])Enum.GetValues(typeof(SurveyType));
            Dictionary<int, string> allValues = new Dictionary<int, string>();
            foreach (var item in list)
            {
                allValues.Add((int)item, GetSurveyTypeDescription(item));
            }
            return allValues;
        }

        private static string GetSurveyTypeDescription(SurveyType item)
        {
            switch (item)
            {
                case SurveyType.CheckBox:
                    return "Check Box";
                case SurveyType.FreeText:
                    return "Free Text";
                default:
                    return item.ToString();
            }
        }
    }
    public enum SurveyType
    {
        FreeText,
        Radio,
        CheckBox,
    }
}