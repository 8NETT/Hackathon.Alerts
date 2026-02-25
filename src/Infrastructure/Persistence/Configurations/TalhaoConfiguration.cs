using Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

internal sealed class TalhaoConfiguration : IEntityTypeConfiguration<Talhao>
{
    public void Configure(EntityTypeBuilder<Talhao> builder)
    {
        builder.ToTable("Talhao");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.ProprietarioId).IsRequired();
        builder.HasMany(t => t.Leituras)
            .WithOne()
            .HasForeignKey(l => l.TalhaoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
