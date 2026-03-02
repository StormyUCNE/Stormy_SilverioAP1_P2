using Microsoft.EntityFrameworkCore;
using Stormy_SilverioAP1_P2.Models;

namespace Stormy_SilverioAP1_P2.DAL;
public class Contexto: DbContext
{
    public Contexto(DbContextOptions<Contexto> options): base(options) { }
    public DbSet<ViajesEspaciales> ViajesEspaciales { get; set; }
}

