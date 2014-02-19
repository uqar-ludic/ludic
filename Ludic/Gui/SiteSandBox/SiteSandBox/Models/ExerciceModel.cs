using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteSandBox.Models
{
    public class ExerciceModel
    {
        public ExerciceModel()
        {
            Errors = new List<Error>();
        }

        public int IdExercice { get; set; }
        public string Code { get; set; }
        public string OutSuccess { get; set; }
        public string OutCompil { get; set; }
        public string Subject { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public int IdError { get; set; }
        public int Line { get; set; }
        public string Message { get; set; }

        public Error(int id, int line, string message)
        {
            id = IdError;
            Line = line;
            Message = message;
        }
    }
}