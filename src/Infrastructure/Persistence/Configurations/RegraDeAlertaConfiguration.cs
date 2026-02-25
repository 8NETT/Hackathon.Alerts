using Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

internal sealed class RegraDeAlertaConfiguration : IEntityTypeConfiguration<RegraDeAlerta>
{
    public void Configure(EntityTypeBuilder<RegraDeAlerta> builder)
    {
        builder.ToTable("RegraDeAlerta");
        builder.HasKey(r => r.Id);
        builder.OwnsOne(r => r.Tipo, tipo =>
            tipo.Property(t => t.Codigo).HasColumnName("Tipo").HasMaxLength(1).IsRequired());
        builder.OwnsOne(r => r.Alvo, alvo =>
            alvo.Property(a => a.Codigo).HasColumnName("Alvo").HasMaxLength(5).IsRequired());
        builder.OwnsOne(r => r.Operador, operador =>
            operador.Property(o => o.Codigo).HasColumnName("Operador").HasMaxLength(3).IsRequired());
        builder.Property(r => r.Limite).IsRequired();
        builder.Property(r => r.JanelasConsecutivas).IsRequired();
        builder.Property(r => r.ExigirJanelaCompleta).IsRequired();
        builder.Property(r => r.Nome).HasMaxLength(50).IsRequired();
        builder.OwnsOne(r => r.Severidade, severidade =>
            severidade.Property(s => s.Codigo).HasColumnName("Severidade").HasMaxLength(1).IsRequired());
        builder.Property(r => r.Ativa).IsRequired();
    }
}
