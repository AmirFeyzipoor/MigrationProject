using System.Data;
using FluentMigrator;

namespace MigrationProject.SqlServerMigrations.Migrations;

[Migration(202403021245)]
public class _202403021245_AddRegistrantIdColumnInUsersTable : Migration
{
    public override void Up()
    {
        Alter.Table("Users").AddColumn("RegistrantId").AsString(450).Nullable()
            .ForeignKey("FK_Users_Users",
                "Users",
                "Id");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Users_Users").OnTable("Users");
        Delete.Column("RegistrantId").FromTable("Users");
    }
}