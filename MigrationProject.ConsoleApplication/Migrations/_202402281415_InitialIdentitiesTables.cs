using FluentMigrator;
using MigrationProject.ConsoleApplication.Infrastructures;

namespace MigrationProject.SqlServerMigrations.Migrations;

[Migration(202402281415)]
public class _202402281415_InitialIdentitiesTables : Migration
{
    private readonly ScriptManager _scriptManager;

    public _202402281415_InitialIdentitiesTables(
        ScriptManager resourceManager)
    {
        _scriptManager = resourceManager;
    }
    public override void Up()
    {
        var scriptBasePath = typeof(_202402281415_InitialIdentitiesTables)
            .Namespace + ".Scripts";
        var script = _scriptManager.Read(
            "_202402281415_InitialIdentitiesTables.sql", 
            scriptBasePath);

        Execute.Sql(script);
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_UserRoles_Users_UserId").OnTable("UserRoles");
        Delete.ForeignKey("FK_UserRoles_Roles_RoleId").OnTable("UserRoles");
        Delete.Table("UserRoles");
        
        Delete.ForeignKey("FK_UserRefreshTokens_Users_UserId").OnTable("UserRefreshTokens");
        Delete.Table("UserRefreshTokens");
        
        Delete.ForeignKey("FK_UserClaims_Users_UserId").OnTable("UserClaims");
        Delete.Table("UserClaims");
        
        Delete.Table("Users");
        
        Delete.Table("Roles");
    }
}