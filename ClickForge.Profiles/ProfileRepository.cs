using ClickForge.Models;
using System.Text.Json;
using ClickForge.Engine;

namespace ClickForge.Profiles;

public class ProfileRepository
{
    public void Create(Profile profile)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(profile, options);
        File.WriteAllText($@"{Directories.ProfileDirectory}\{profile.Name}.json", json);
    }

    public async Task Load(string name)
    {
        var json = await File.ReadAllTextAsync($@"{Directories.ProfileDirectory}\{name}.json");

        var profile = JsonSerializer.Deserialize<Profile>(json);
        
        // Move to Program class. Return profile?
        Console.WriteLine($"\nProfile Name: {profile!.Name}");
        Console.WriteLine($"Is Loopable: {profile.Loop}");
        Console.WriteLine($"Target Window Name: {profile.WindowName}");
        Console.WriteLine($"Input Type: {profile.Action.InputType}");
        Console.WriteLine($"Input Data: {profile.Action.InputData}");
        Console.WriteLine($"Delay: {profile.Action.Delay}\n");
        
        var automation = new ProfileAutomation();
        await automation.RunProfile(profile);
    }

    public void Delete(string name) =>
        File.Delete($@"{Directories.ProfileDirectory}\{name}.json");


    public string?[] GetFileNames() =>
        GetFiles().Select(Path.GetFileNameWithoutExtension).ToArray();


    private string[] GetFiles()
    {
        if (!Directory.Exists(Directories.ProfileDirectory))
        {
            Directory.CreateDirectory(Directories.ProfileDirectory);
        }

        return Directory.GetFiles
        (
            Directories.ProfileDirectory,
            "*.json",
            SearchOption.AllDirectories
        );
    }
}