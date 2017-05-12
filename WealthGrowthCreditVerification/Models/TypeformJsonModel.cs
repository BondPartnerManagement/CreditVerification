using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WealthGrowthCreditVerification.Models
{
    /// <summary>
    /// This Model is purely used in order to parse the received Typeform JsonObject to a C# object;
    /// </summary>
    public class TypeformJsonModel
    {
        public class Hidden
        {
            public string referralagent { get; set; }
        }

        public class Field
        {
            public string id { get; set; }
            public string title { get; set; }
            public string type { get; set; }
        }

        public class Definition
        {
            public string id { get; set; }
            public string title { get; set; }
            public List<Field> fields { get; set; }
        }

        public class Field2
        {
            public string id { get; set; }
            public string type { get; set; }
        }

        public class Choice
        {
            public string label { get; set; }
        }

        public class Answer
        {
            public string type { get; set; }
            public string text { get; set; }
            public Field2 field { get; set; }
            public string email { get; set; }
            public Choice choice { get; set; }
            public int? number { get; set; }
            public bool? boolean { get; set; }
        }

        public class FormResponse
        {
            public string form_id { get; set; }
            public string token { get; set; }
            public string submitted_at { get; set; }
            public Hidden hidden { get; set; }
            public Definition definition { get; set; }
            public List<Answer> answers { get; set; }
        }

        public class RootObject
        {
            public string event_id { get; set; }
            public string event_type { get; set; }
            public FormResponse form_response { get; set; }
        }
    }
}