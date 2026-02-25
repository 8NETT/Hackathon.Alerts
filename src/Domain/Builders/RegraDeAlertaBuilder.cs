using Domain.Entities;
using Domain.Extensions;
using Domain.Validation;
using Domain.ValueObjects;

namespace Domain.Builders;

public sealed class RegraDeAlertaBuilder
{
    private RegraDeAlerta _regra = new();

    public RegraDeAlertaBuilder Tipo(TipoSensor tipo) =>
        this.Tee(b => b._regra.Tipo = tipo);
    public RegraDeAlertaBuilder Alvo(EstatisticaAlvo alvo) =>
        this.Tee(b => b._regra.Alvo = alvo);
    public RegraDeAlertaBuilder Operador(OperadorComparacao operador) =>
        this.Tee(b => b._regra.Operador = operador);
    public RegraDeAlertaBuilder Limite(double limite) =>
        this.Tee(b => b._regra.Limite = limite);
    public RegraDeAlertaBuilder JanelasConsecutivas(int janelas) =>
        this.Tee(b => b._regra.JanelasConsecutivas = janelas);
    public RegraDeAlertaBuilder ExigirJanelaCompleta(bool exigir) =>
        this.Tee(b => b._regra.ExigirJanelaCompleta = exigir);
    public RegraDeAlertaBuilder Nome(string nome) =>
        this.Tee(b => b._regra.Nome = nome);
    public RegraDeAlertaBuilder Severidade(Severidade severidade) =>
        this.Tee(b => b._regra.Severidade = severidade);

    public ValidationResult Validar() =>
        new RegraDeAlertaValidator().Validate(_regra);

    public RegraDeAlerta Criar()
    {
        if (!Validar().IsValid)
            throw new InvalidOperationException("Não é possível criar uma regra em um estado inválido.");

        _regra.Id = Guid.NewGuid();

        return _regra;
    }
}
