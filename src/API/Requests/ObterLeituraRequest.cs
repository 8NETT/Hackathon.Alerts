namespace API.Requests;

public sealed record ObterLeituraRequest
{
    public Guid? TalhaoId { get; set; }
    public string? Tipo { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
}
