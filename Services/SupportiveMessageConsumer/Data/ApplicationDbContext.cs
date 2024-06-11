public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<SupportiveMessage> SupportiveMessages { get; set; }
}

public class SupportiveMessage
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string Sender { get; set; }
    public DateTime Timestamp { get; set; }
}