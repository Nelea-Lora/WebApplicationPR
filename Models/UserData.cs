namespace WebApplicationPR.Models;

public class UserData
{
    public static List<User> Users { get; set; } = new List<User>
    {
        new User
        {
            Id = 1,
            Name = "Алексей",
            Email = "alexey@example.com",
            Preferences = new List<string> { "Технологии", "Спорт", "Музыка" }
        },
        new User
        {
            Id = 2,
            Name = "Мария",
            Email = "maria@example.com",
            Preferences = new List<string> { "Книги", "Путешествия" }
        },
        new User
        {
            Id = 3,
            Name = "Иван",
            Email = "ivan@example.com",
            Preferences = new List<string> { "Игры", "Кино", "Технологии" }
        }
    };
}