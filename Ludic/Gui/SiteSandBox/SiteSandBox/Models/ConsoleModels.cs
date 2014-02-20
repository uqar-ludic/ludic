using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace SiteSandBox.Models
{
    public class ExerciceConsole
    {
        public ExerciceConsole()
        {
            Errors = new List<ErrorConsole>();
            Comments = new List<CommentaireConsole>();
            Success = new List<SuccessConsole>();
        }

        public enum Actions
        {
            SAVE = 1,
            COMPILE = 2,
            COMMENTER = 3
        }

        public int IdExercice { get; set; }
        public int Value { get; set; }
        public Actions Action { get; set; }
        public string Code { get; set; }
        public string OutSuccess { get; set; }
        public string OutCompil { get; set; }
        public string Subject { get; set; }
        public CommentaireConsole Posted { get; set; }
        public List<CommentaireConsole> Comments { get; set; }     
        public List<ErrorConsole> Errors { get; set; }
        public List<SuccessConsole> Success { get; set; }

        public List<PointF> ProgressBar()
        {
            List<PointF> progress = new List<PointF>();
            float ratio = (Success.Count != 0) ? 100 / Success.Count : 0;
            Console.WriteLine("ratio = " + ratio.ToString());
            foreach (SuccessConsole success in Success)
            {
                if (success.isCompleted)
                    progress.Add(new PointF(ratio, (float)(success.Diff + 10)));
                else
                    progress.Add(new PointF(ratio, (float)success.Diff));

            }
            return progress;
        }
    }

    public class SuccessConsole
    {
        public enum Difficulty 
        {
            EASY = 1,
            MEDIUM = 2,
            HARD = 3
        }
        public int IdSucess { get; set; }
        public Difficulty Diff { get; set; }
        public bool isCompleted { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }

        public SuccessConsole()
        {
        }

        public SuccessConsole(int id, Difficulty difficulty, string titre, string description)
        {
            IdSucess = id;
            Diff = difficulty;
            Titre = titre;
            Description = description;
        }
    }

    public class CommentaireConsole
    {
        public int IdCommentaire { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        
        public CommentaireConsole()
        {

        }

        public CommentaireConsole(int id, string name, DateTime date, string titre, string comment)
        {
            IdCommentaire = id;
            UserName = name;
            Date = date;
            Title = titre;
            Comment = comment;
        }
    }

    public class ErrorConsole
    {
        public int IdError { get; set; }
        public int Line { get; set; }
        public string Message { get; set; }

        public ErrorConsole(int id, int line, string message)
        {
            id = IdError;
            Line = line;
            Message = message;
        }
    }
}