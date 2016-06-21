﻿using System;

namespace SurveyApp.Models
{
    public class SurveyResult
    {
        public long SurveyResultId { get; set; }
        public long SurveyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public long UserId { get; set; }
    }
}