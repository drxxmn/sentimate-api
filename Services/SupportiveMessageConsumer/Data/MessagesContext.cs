using Microsoft.EntityFrameworkCore;

public class MessagesContext : DbContext
{
    public MessagesContext(DbContextOptions<MessagesContext> options) : base(options) { }

    public DbSet<SupportiveMessage> SupportiveMessages { get; set; }
}

public class SupportiveMessage
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty; // Initialize as empty string
}