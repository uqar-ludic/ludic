namespace SiteSandBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        ThemeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ThemeId);
            
            CreateTable(
                "dbo.Exercices",
                c => new
                    {
                        ExerciceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Sujet = c.String(),
                        Value = c.Int(nullable: false),
                        ThemeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExerciceId)
                .ForeignKey("dbo.Themes", t => t.ThemeId, cascadeDelete: true)
                .Index(t => t.ThemeId);
            
            CreateTable(
                "dbo.Commentaires",
                c => new
                    {
                        CommentaireId = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Value = c.Int(nullable: false),
                        ExerciceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentaireId)
                .ForeignKey("dbo.Exercices", t => t.ExerciceId, cascadeDelete: true)
                .Index(t => t.ExerciceId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Historiques",
                c => new
                    {
                        HistoriqueId = c.Int(nullable: false, identity: true),
                        lastLogin = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HistoriqueId)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Saves",
                c => new
                    {
                        SaveId = c.Int(nullable: false, identity: true),
                        Solution = c.String(),
                        ExerceId = c.Int(nullable: false),
                        Exercice_ExerciceId = c.Int(),
                        Historique_HistoriqueId = c.Int(),
                    })
                .PrimaryKey(t => t.SaveId)
                .ForeignKey("dbo.Exercices", t => t.Exercice_ExerciceId)
                .ForeignKey("dbo.Historiques", t => t.Historique_HistoriqueId)
                .Index(t => t.Exercice_ExerciceId)
                .Index(t => t.Historique_HistoriqueId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Saves", new[] { "Historique_HistoriqueId" });
            DropIndex("dbo.Saves", new[] { "Exercice_ExerciceId" });
            DropIndex("dbo.Historiques", new[] { "UserId" });
            DropIndex("dbo.Commentaires", new[] { "ExerciceId" });
            DropIndex("dbo.Exercices", new[] { "ThemeId" });
            DropForeignKey("dbo.Saves", "Historique_HistoriqueId", "dbo.Historiques");
            DropForeignKey("dbo.Saves", "Exercice_ExerciceId", "dbo.Exercices");
            DropForeignKey("dbo.Historiques", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Commentaires", "ExerciceId", "dbo.Exercices");
            DropForeignKey("dbo.Exercices", "ThemeId", "dbo.Themes");
            DropTable("dbo.Saves");
            DropTable("dbo.Historiques");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Commentaires");
            DropTable("dbo.Exercices");
            DropTable("dbo.Themes");
        }
    }
}
