using Application.DTOs;

namespace Application.UseCases.Regra.ObterRegrasAtivas;

public interface IObterRegrasAtivasUseCase : IUseCase<object?, IEnumerable<RegraDTO>>
{
}
