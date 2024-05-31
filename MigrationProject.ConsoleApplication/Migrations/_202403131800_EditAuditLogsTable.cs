using FluentMigrator;
namespace MigrationProject.SqlServerMigrations.Migrations;

[Migration(202403131800)]
public class _202403131800_EditAuditLogsTable : Migration
{
    public override void Up()
    {
        Delete.Column("EntityPrimaryKey").FromTable("AuditLogs");
        Delete.Column("EntityName").FromTable("AuditLogs");
        Alter.Table("AuditLogs")
            .AddColumn("RegistrantId")
            .AsString().NotNullable().WithDefaultValue("a00c9ce9-6db0-4c73-b0e2-4f549176da9f");
        
        Rename.Table("AuditLogs").To("UserAuditLogs");
    }

    public override void Down()
    {
        Rename.Table("UserAuditLogs").To("AuditLogs");

        Delete.Column("RegistrantId").FromTable("AuditLogs");
        Alter.Table("AuditLogs")
            .AddColumn("EntityPrimaryKey")
            .AsString().NotNullable();
        Alter.Table("AuditLogs")
            .AddColumn("EntityName")
            .AsString().NotNullable();
    }
}