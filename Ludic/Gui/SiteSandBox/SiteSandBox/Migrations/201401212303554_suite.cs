namespace SiteSandBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class suite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Historiques", "LastExerciceId", c => c.Int(nullable: false));
            AddColumn("dbo.Historiques", "LastExercice_ExerciceId", c => c.Int());
            AddForeignKey("dbo.Historiques", "LastExercice_ExerciceId", "dbo.Exercices", "ExerciceId");
            CreateIndex("dbo.Historiques", "LastExercice_ExerciceId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Historiques", new[] { "LastExercice_ExerciceId" });
            DropForeignKey("dbo.Historiques", "LastExercice_ExerciceId", "dbo.Exercices");
            DropColumn("dbo.Historiques", "LastExercice_ExerciceId");
            DropColumn("dbo.Historiques", "LastExerciceId");
        }
    }
}
