namespace InfiniSoft.Admin.Models;

public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
}
