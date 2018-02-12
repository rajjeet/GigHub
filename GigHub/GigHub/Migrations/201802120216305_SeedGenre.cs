namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT dbo.Genres (Id, Name) VALUES (1, 'Jazz');");
            Sql("INSERT dbo.Genres (Id, Name) VALUES (2, 'Country');");
            Sql("INSERT dbo.Genres (Id, Name) VALUES (3, 'Pop');");
            Sql("INSERT dbo.Genres (Id, Name) VALUES (4, 'Rap');");
        }

        public override void Down()
        {
            Sql("DELETE FROM dbo.Genres WHERE ID in (1,2,3,4);");
        }
    }
}
