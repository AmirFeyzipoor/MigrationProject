using FluentMigrator;

namespace MigrationProject.SqlServerMigrations.Migrations;

[Migration(202403031045)]
public class _202403031045_AddEntityPrimaryKeyColumnInAuditLogsTable : Migration
{
    public override void Up()
    {
        Alter.Table("AuditLogs")
            .AddColumn("EntityPrimaryKey")
            .AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Column("EntityPrimaryKey").FromTable("AuditLogs");
    }
}