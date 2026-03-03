using Microsoft.EntityFrameworkCore;
using Stormy_SilverioAP1_P2.DAL;
using Stormy_SilverioAP1_P2.Models;
using System.Linq.Expressions;
namespace Stormy_SilverioAP1_P2.Service;

public class ViajesEspacialesService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar()
    {
        return false;
    }
    public async Task<bool> Exise()
    {
        return false;
    }
    public async Task<bool> Insertar()
    {
        return false;
    }
    public async Task<bool> Modificar()
    {
        return false;
    }
    public async Task<bool> Buscar()
    {
        return false;
    }
    public async Task<bool> Eliminar()
    {
        return false;
    }
    public async Task<List<ViajesEspaciales>> Listar(Expression<Func<ViajesEspaciales, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.ViajesEspaciales.Where(criterio).AsNoTracking().ToListAsync();
    }
}