using Microsoft.EntityFrameworkCore;
using Stormy_SilverioAP1_P2.DAL;
using Stormy_SilverioAP1_P2.Models;
using System.Linq.Expressions;

namespace Stormy_SilverioAP1_P2.Service;

public class TiposPuntosService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<List<TiposPuntos>> Listar(Expression<Func<TiposPuntos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposPuntos.Where(criterio).AsNoTracking().ToListAsync();
    }
}

