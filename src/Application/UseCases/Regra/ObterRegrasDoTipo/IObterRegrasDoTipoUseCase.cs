using Application.DTOs;

namespace Application.UseCases.Regra.ObterRegrasDoTipo;

public interface IObterRegrasDoTipoUseCase : IUseCase<string, IEnumerable<RegraDTO>>
{
}
