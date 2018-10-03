namespace BecaDotNet.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_CLIENTE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NOME = c.String(nullable: false, maxLength: 200, unicode: false),
                        CNPJ = c.Long(nullable: false),
                        CONTATO = c.String(nullable: false, maxLength: 200, unicode: false),
                        IS_ACTIVE = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TB_PROJETO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NOME = c.String(nullable: false, maxLength: 200, unicode: false),
                        DATA_INICIO = c.DateTime(nullable: false),
                        DATA_FINAL = c.DateTime(nullable: false),
                        CLIENTE_ID = c.Int(nullable: false),
                        IS_ACTIVE = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TB_CLIENTE", t => t.CLIENTE_ID)
                .Index(t => t.CLIENTE_ID);
            
            CreateTable(
                "dbo.TB_PROJETO_USER",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PROJETO_ID = c.Int(nullable: false),
                        USER_ID = c.Int(nullable: false),
                        RESPONSAVEL = c.Boolean(nullable: false),
                        DATA_INICIO = c.DateTime(nullable: false),
                        DATA_FIM = c.DateTime(),
                        IS_ACTIVE = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TB_PROJETO", t => t.PROJETO_ID, cascadeDelete: true)
                .ForeignKey("dbo.TB_USER", t => t.USER_ID, cascadeDelete: true)
                .Index(t => t.PROJETO_ID)
                .Index(t => t.USER_ID);
            
            CreateTable(
                "dbo.TB_USER",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FULL_NAME = c.String(nullable: false, maxLength: 200, unicode: false),
                        EMAIL = c.String(nullable: false, maxLength: 200, unicode: false),
                        LOGIN = c.String(nullable: false, maxLength: 100, unicode: false),
                        PASSWORD = c.String(nullable: false, maxLength: 150, unicode: false),
                        REGISTER_DATE = c.DateTime(nullable: false),
                        USER_TYPE_ID = c.Int(nullable: false),
                        SUPERIOR_ID = c.Int(),
                        PROJETO_ID = c.Int(),
                        IS_ACTIVE = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TB_PROJETO", t => t.PROJETO_ID)
                .ForeignKey("dbo.TB_USER", t => t.SUPERIOR_ID)
                .ForeignKey("dbo.TB_USER_TYPE", t => t.USER_TYPE_ID, cascadeDelete: true)
                .Index(t => t.USER_TYPE_ID)
                .Index(t => t.SUPERIOR_ID)
                .Index(t => t.PROJETO_ID);
            
            CreateTable(
                "dbo.TB_USER_TYPE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DESC_USER_TYPE = c.String(nullable: false, maxLength: 200, unicode: false),
                        IS_ACTIVE = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TB_USER_TYPE_USER",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        USER_ID = c.Int(nullable: false),
                        USER_TYPE_ID = c.Int(nullable: false),
                        CREATED_DATE = c.DateTime(nullable: false),
                        START_DATE = c.DateTime(nullable: false),
                        END_DATE = c.DateTime(),
                        IS_ACTIVE = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TB_PROJETO_USER", "USER_ID", "dbo.TB_USER");
            DropForeignKey("dbo.TB_USER", "USER_TYPE_ID", "dbo.TB_USER_TYPE");
            DropForeignKey("dbo.TB_USER", "SUPERIOR_ID", "dbo.TB_USER");
            DropForeignKey("dbo.TB_USER", "PROJETO_ID", "dbo.TB_PROJETO");
            DropForeignKey("dbo.TB_PROJETO_USER", "PROJETO_ID", "dbo.TB_PROJETO");
            DropForeignKey("dbo.TB_PROJETO", "CLIENTE_ID", "dbo.TB_CLIENTE");
            DropIndex("dbo.TB_USER", new[] { "PROJETO_ID" });
            DropIndex("dbo.TB_USER", new[] { "SUPERIOR_ID" });
            DropIndex("dbo.TB_USER", new[] { "USER_TYPE_ID" });
            DropIndex("dbo.TB_PROJETO_USER", new[] { "USER_ID" });
            DropIndex("dbo.TB_PROJETO_USER", new[] { "PROJETO_ID" });
            DropIndex("dbo.TB_PROJETO", new[] { "CLIENTE_ID" });
            DropTable("dbo.TB_USER_TYPE_USER");
            DropTable("dbo.TB_USER_TYPE");
            DropTable("dbo.TB_USER");
            DropTable("dbo.TB_PROJETO_USER");
            DropTable("dbo.TB_PROJETO");
            DropTable("dbo.TB_CLIENTE");
        }
    }
}
