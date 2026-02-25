using Domain.Entities;

namespace Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    private string? _connectionString;

    public ApplicationDbContext() : base() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public ApplicationDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Talhao> Talhoes => Set<Talhao>();
    public DbSet<LeituraAgregada> Leituras => Set<LeituraAgregada>();
    public DbSet<RegraDeAlerta> Regras => Set<RegraDeAlerta>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
}
