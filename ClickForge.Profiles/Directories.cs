namespace ClickForge.Profiles;

public static class Directories
{
    private static readonly string Username = Environment.UserName;
    public static readonly string ProfileDirectory = $@"C:\Users\{Username}\AppData\Roaming\ClickForge\Profiles";
}