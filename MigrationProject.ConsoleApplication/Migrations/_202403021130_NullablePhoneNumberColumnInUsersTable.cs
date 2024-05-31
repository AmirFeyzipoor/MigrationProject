using FluentMigrator;

namespace MigrationProject.SqlServerMigrations.Migrations;

[Migration(202403021130)]
public class _202403021130_NullablePhoneNumberColumnInUsersTable : Migration
{
    public override void Up()
    {
        Alter.Table("Users")
            .AlterColumn("PhoneNumber").AsString().Nullable();
    }

    public override void Down()
    {
        Alter.Table("Users")
            .AlterColumn("PhoneNumber").AsString().NotNullable();
    }
}