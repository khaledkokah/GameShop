namespace GameShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterCustomerTypeIdField : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "CustomerType_Id", "dbo.CustomerTypes");
            DropIndex("dbo.Customers", new[] { "CustomerType_Id" });
            DropColumn("dbo.Customers", "CustomerTypeId");
            RenameColumn(table: "dbo.Customers", name: "CustomerType_Id", newName: "CustomerTypeId");
            DropPrimaryKey("dbo.CustomerTypes");
            AlterColumn("dbo.Customers", "CustomerTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerTypes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CustomerTypes", "Id");
            CreateIndex("dbo.Customers", "CustomerTypeId");
            AddForeignKey("dbo.Customers", "CustomerTypeId", "dbo.CustomerTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CustomerTypeId", "dbo.CustomerTypes");
            DropIndex("dbo.Customers", new[] { "CustomerTypeId" });
            DropPrimaryKey("dbo.CustomerTypes");
            AlterColumn("dbo.CustomerTypes", "Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.Customers", "CustomerTypeId", c => c.Byte());
            AddPrimaryKey("dbo.CustomerTypes", "Id");
            RenameColumn(table: "dbo.Customers", name: "CustomerTypeId", newName: "CustomerType_Id");
            AddColumn("dbo.Customers", "CustomerTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "CustomerType_Id");
            AddForeignKey("dbo.Customers", "CustomerType_Id", "dbo.CustomerTypes", "Id");
        }
    }
}
