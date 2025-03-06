namespace WebApplicationPR.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public List<string> Preferences { get; set; } = new();
}