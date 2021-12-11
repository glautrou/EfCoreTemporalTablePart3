using Microsoft.EntityFrameworkCore;

namespace EfCoreTemporalTablePart3;

public partial class DemoTemporelleContext : DbContext
{
    public DemoTemporelleContext()
    {
    }

    public DemoTemporelleContext(DbContextOptions<DemoTemporelleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employe> Employe { get; set; }
    public virtual DbSet<Entreprise> Entreprise { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("data source=.;initial catalog=DemoTemporelle;Integrated Security=SSPI");
        }
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
