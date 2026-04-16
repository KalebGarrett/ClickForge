namespace ClickForge.Models;

public class Profile
{
    public string Name { get; set; }
    public bool Loop { get; set; }
    public string WindowName { get; set; }
    public List<Action> Actions { get; set; }
}