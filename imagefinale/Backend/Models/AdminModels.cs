namespace InfiniSoft.Admin.Models;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class ContactMessage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
}

public class SiteStatistic
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty; // e.g., "Visit", "ServiceClick"
    public string Value { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
