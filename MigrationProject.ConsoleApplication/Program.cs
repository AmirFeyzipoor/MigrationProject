using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MigrationProject.ConsoleApplication;
using MigrationProject.ConsoleApplication.Infrastructures;
using MigrationProject.SqlServerMigrations;

var configs = GetMigrationConfigs();

CreateDatabaseIfNotExist(configs.ConnectionString);

var runner = CreateMigrationRunner(configs);

UpdateDatabase(runner, configs.MigrationCommand, configs.MigrationVersion);


static void CreateDatabaseIfNotExist(string connectionString)
{
    var databaseName = GetDatabaseName(connectionString);
    var masterConnectionString = CreateMasterConnectionString(
        connectionString,
        "master");
    var commandScript = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = {databaseName})" +
                        $"CREATE DATABASE {databaseName}";

    using var connection = new SqlConnection(masterConnectionString);
    using var command = new SqlCommand(commandScript, connection);
    connection.Open();
    command.ExecuteNonQuery();
    connection.Close();
}

static string CreateMasterConnectionString(
    string connectionString,
    string databaseName)
{
    var builder = new SqlConnectionStringBuilder(connectionString)
    {
        InitialCatalog = databaseName
    };
    return builder.ConnectionString;
}

static string GetDatabaseName(string connectionString)
{
    return new SqlConnectionStringBuilder(connectionString).InitialCatalog;
}

void UpdateDatabase(IMigrationRunner migrationRunner, string migrationCommand, int migrationVersion)
{
    if (migrationCommand == "Up")
    {
        if (migrationVersion == 0)
        {
            migrationRunner.MigrateUp();
        }
        else
        {
            migrationRunner.MigrateUp(migrationVersion);
        }
    }
    else if (migrationCommand == "Down")
    {
        migrationRunner.MigrateDown(migrationVersion);
    }
}

MigrationConfigs GetMigrationConfigs()
{
    var configurations = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(
            "appsettings.json",
            optional: true,
            reloadOnChange: false)
        .Build();
    var migrationConfigs = new MigrationConfigs();
    configurations.Bind(migrationConfigs);
    return migrationConfigs;
}

IMigrationRunner CreateMigrationRunner(MigrationConfigs configs1)
{
    return new ServiceCollection()
        .AddFluentMigratorCore()
        .ConfigureRunner(_ => _
            .AddSqlServer()
            .WithGlobalConnectionString(configs1.ConnectionString)
            .ScanIn(typeof(ScriptManager).Assembly).For.All())
        .AddSingleton<ScriptManager>()
        .AddLogging(_ => _.AddFluentMigratorConsole())
        .BuildServiceProvider().GetRequiredService<IMigrationRunner>();
}