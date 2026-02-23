namespace Domain.ValueObjects;

public abstract record Enumeration
{
    public string Codigo { get; }

    protected internal Enumeration() { Codigo = null!; }

    protected internal Enumeration(string codigo)
    {
        Codigo = codigo;
    }

    public override string ToString() => Codigo;
}
