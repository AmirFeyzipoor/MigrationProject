using FluentMigrator;

namespace MigrationProject.SqlServerMigrations.Migrations;

[Migration(202402291730)]
public class _202402291730_InitialAuditLogsTable : Migration
{
    public override void Up()
    {
        Create.Table("AuditLogs")
            .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
            .WithColumn("UserId").AsString().NotNullable()
            .WithColumn("EntityName").AsString().NotNullable()
            .WithColumn("Action").AsString().NotNullable()
            .WithColumn("Timestamp").AsDateTime().NotNullable()
            .WithColumn("Changes").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("AuditLogs");
    }
}