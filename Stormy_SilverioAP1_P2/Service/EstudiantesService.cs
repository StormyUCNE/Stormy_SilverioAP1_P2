using Microsoft.EntityFrameworkCore;
using Stormy_SilverioAP1_P2.DAL;
using Stormy_SilverioAP1_P2.Models;
using System.Linq.Expressions;
namespace Stormy_SilverioAP1_P2.Service;

public class EstudiantesService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<List<Estudiantes>> Listar(Expression<Func<Estudiantes, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes.Where(criterio).AsNoTracking().ToListAsync();
    }
}