using Application.Persistence;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public sealed class TalhaoRepository : Repository<Talhao>, ITalhaoRepository
{
    public TalhaoRepository(ApplicationDbContext context) : base(context) { }
}
