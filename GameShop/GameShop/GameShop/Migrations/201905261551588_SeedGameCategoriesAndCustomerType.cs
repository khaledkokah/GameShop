namespace GameShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedGameCategoriesAndCustomerType : DbMigration
    {
        public override void Up()
        {
            Sql("Insert Into Categories(Name) Values('Adventure')");
            Sql("Insert Into Categories(Name) Values('Arcade')");
            Sql("Insert Into CustomerTypes(Id,Name) Values(1,'Casual Gamer')");
            Sql("Insert Into CustomerTypes(Id,Name) Values(2,'Core Gamer')");
        }
        
        public override void Down()
        {
        }
    }
}
