using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SiteSandBox.Models
{
    public class SandBoxContext : DbContext
    {

        public DbSet<Theme> Themes { get; set; }
        public DbSet<Exercice> Exercices { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<Historique>  Historiques{ get; set; }
        public DbSet<Save> Saves { get; set; }
    }

    public class Exercice
    {
        public Exercice()
        {
            Commentaires = new List<Commentaire>();
        }
        public int ExerciceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sujet { get; set; }
        public int Value { get; set; }
        public int ThemeId { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual ICollection<Commentaire> Commentaires { get; set; }
    }

    public class Theme
    {
        public Theme()
        {
            Exercices = new List<Exercice>();
        }

        public int ThemeId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Exercice> Exercices { get; set; }
    }

    public class Commentaire
    {
        public int CommentaireId { get; set; }
        public string Comment { get; set; }
        public int Value { get; set; }
        public int ExerciceId { get; set; }
        public virtual Exercice Exercice { get; set; }
    }

    public class Historique
    {
        public Historique()
        {
            Saves = new List<Save>();
        }
        public int HistoriqueId { get; set; }
        public DateTime lastLogin { get; set; }
        public int UserId { get; set; }
        public int LastExerciceId { get; set; }
        public virtual Exercice LastExercice { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<Save> Saves { get; set; }
    }

    public class Save
    {
        public int SaveId { get; set; }
        public string Solution { get; set; }
        public int ExerceId { get; set; }
        public virtual Exercice Exercice { get; set; }
    }
}