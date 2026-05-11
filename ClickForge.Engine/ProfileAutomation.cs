using ClickForge.Models;

namespace ClickForge.Engine;

public class ProfileAutomation
{
    private bool IsRunning { get; set; } = true;

    public async Task RunProfile(Profile profile)
    {
        while (IsRunning)
        {
            // Execute action
            
            await Task.Delay(TimeSpan.FromSeconds(profile.Action.Delay));

            if (!profile.Loop)
                break;
        }
    }
}