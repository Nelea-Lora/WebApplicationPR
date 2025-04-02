namespace WebApplicationPR.Models;

public class RecommendationDto
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}