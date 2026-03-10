using Microsoft.EntityFrameworkCore;
using Stormy_SilverioAP1_P2.DAL;
using Stormy_SilverioAP1_P2.Models;
using System.Linq.Expressions;

namespace Stormy_SilverioAP1_P2.Service;

public class AsignacionesPuntosService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(AsignacionesPuntos asignaciones)
    {
        if (!await Existe(asignaciones.IdAsignacion))
            return await Insertar(asignaciones);
        else
            return await Modificar(asignaciones);
    }
    private async Task<bool> Existe(int asignacionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.AsignacionesPuntos.AnyAsync(a => a.IdAsignacion == asignacionId);
    }
    private async Task<bool> Insertar(AsignacionesPuntos asignaciones)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var estudiante = await contexto.Estudiantes.FirstOrDefaultAsync(e => e.EstudianteId == asignaciones.EstudianteId);
        if(estudiante != null)
            estudiante.BalancePuntos += asignaciones.TotalPuntos;
        contexto.AsignacionesPuntos.Add(asignaciones);
        return await contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(AsignacionesPuntos asignaciones)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var asignacionAnterior = await contexto.AsignacionesPuntos
            .Include(d => d.ListaDetalle)
            .FirstOrDefaultAsync(a => a.IdAsignacion == asignaciones.IdAsignacion);

        if (asignacionAnterior == null)
            return false;

        var estudiante = await contexto.Estudiantes
            .FirstOrDefaultAsync(e => e.EstudianteId == asignaciones.EstudianteId);

        if (estudiante != null)
        {
            estudiante.BalancePuntos -= asignacionAnterior.TotalPuntos;
            estudiante.BalancePuntos += asignaciones.TotalPuntos;
        }

        contexto.DetalleAsignaciones.RemoveRange(asignacionAnterior.ListaDetalle);

        foreach (var detalle in asignaciones.ListaDetalle)
        {
            asignacionAnterior.ListaDetalle.Add(new DetalleAsignacion
            {
                TipoPuntoId = detalle.TipoPuntoId,
                CantidadPuntos = detalle.CantidadPuntos
            });
        }

        contexto.Entry(asignacionAnterior).CurrentValues.SetValues(asignaciones);

        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<AsignacionesPuntos?> Buscar(int asignacionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.AsignacionesPuntos.Include(d=> d.ListaDetalle).FirstOrDefaultAsync(a => a.IdAsignacion == asignacionId);
    }
    public async Task<bool> Eliminar(int asignacionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var asignacionAnterior = await contexto.AsignacionesPuntos
            .Include(d => d.ListaDetalle)
            .FirstOrDefaultAsync(a => a.IdAsignacion == asignacionId);

        if (asignacionAnterior == null)
            return false;

        var estudiante = await contexto.Estudiantes
            .FirstOrDefaultAsync(e => e.EstudianteId == asignacionAnterior.EstudianteId);

        if (estudiante != null)
            estudiante.BalancePuntos -= asignacionAnterior.TotalPuntos;

        contexto.AsignacionesPuntos.Remove(asignacionAnterior);

        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<List<AsignacionesPuntos>> Listar(Expression<Func<AsignacionesPuntos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.AsignacionesPuntos.Include(d => d.ListaDetalle).Include(e => e.Estudiantes).Where(criterio).AsNoTracking().ToListAsync();
    }
}
