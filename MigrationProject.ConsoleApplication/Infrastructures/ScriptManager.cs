namespace MigrationProject.ConsoleApplication.Infrastructures;

public class ScriptManager
{
    public string Read(string name, string? scriptBasePath = null)
    {
        var assembly = typeof(ScriptManager).Assembly;
        scriptBasePath ??= typeof(ScriptManager).Namespace + ".Scripts";

        var scriptPath = $"{scriptBasePath}.{name}";
        using var stream = assembly.GetManifestResourceStream(scriptPath);

        if (stream == null)
            throw new Exception(message: "Stream is Null");

        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}