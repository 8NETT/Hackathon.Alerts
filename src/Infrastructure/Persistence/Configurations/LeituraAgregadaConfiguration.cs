using Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

internal sealed class LeituraAgregadaConfiguration : IEntityTypeConfiguration<LeituraAgregada>
{
    public void Configure(EntityTypeBuilder<LeituraAgregada> builder)
    {
        builder.ToTable("LeituraAgregada");
        builder.HasKey(l => l.Id);
        builder.OwnsOne(l => l.Tipo, tipo => 
            tipo.Property(t => t.Codigo).HasColumnName("Tipo").HasMaxLength(1).IsRequired());
        builder.OwnsOne(l => l.Unidade, unidade =>
            unidade.Property(u => u.Codigo).HasColumnName("Unidade").HasMaxLength(5).IsRequired());
        builder.OwnsOne(l => l.Janela, janela =>
        {
            janela.Property(j => j.Inicio).HasColumnName("Inicio").IsRequired();
            janela.Property(j => j.Fim).HasColumnName("Fim").IsRequired();
        });
        builder.OwnsOne(l => l.Estatisticas, estatisticas =>
        {
            estatisticas.Property(e => e.Minima).HasColumnName("Minima").IsRequired();
            estatisticas.Property(e => e.Maxima).HasColumnName("Maxima").IsRequired();
            estatisticas.Property(e => e.Media).HasColumnName("Media").IsRequired();
            estatisticas.Property(e => e.Soma).HasColumnName("Soma").IsRequired();
            estatisticas.Property(e => e.Quantidade).HasColumnName("Quantidade").IsRequired();
        });
        builder.Property(l => l.PrimeiroTimestamp).IsRequired();
        builder.Property(l => l.UltimoTimestamp).IsRequired();
        builder.Property(l => l.UltimoValor).IsRequired();
    }
}