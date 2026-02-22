namespace Domain.Entities;

public sealed class Talhao : BaseEntity
{
    private List<LeituraAgregada> _leituras = new();

    public Guid ProprietarioId { get; internal set; }
    public IReadOnlyCollection<LeituraAgregada> Leituras => _leituras.AsReadOnly();

    internal Talhao() { }

    public Talhao(Guid id, Guid proprietarioId)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException(nameof(id));
        if (proprietarioId == Guid.Empty)
            throw new ArgumentNullException(nameof(proprietarioId));

        Id = id;
        ProprietarioId = proprietarioId;
    }
}
