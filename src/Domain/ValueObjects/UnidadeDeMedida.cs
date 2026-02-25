using Domain.Parsing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Domain.ValueObjects;

public sealed record UnidadeDeMedida : Enumeration
{
    private UnidadeDeMedida() : base() { }

    private UnidadeDeMedida(string codigo) : base(codigo) { }

    public static readonly UnidadeDeMedida Celsius = new("°C");
    public static readonly UnidadeDeMedida Fahrenheit = new("F");
    public static readonly UnidadeDeMedida Kelvin = new("K");
    public static readonly UnidadeDeMedida UmidadeRelativa = new("%");
    public static readonly UnidadeDeMedida Milimetros = new("mm");
    public static readonly UnidadeDeMedida Polegadas = new("in");

    public static UnidadeDeMedida Parse(string value) =>
        UnidadeDeMedidaParser.Parse(value);

    public static bool TryParse(string value, out UnidadeDeMedida? unidade) =>
        UnidadeDeMedidaParser.TryParse(value, out unidade);

    public override string ToString() => Codigo;
}
