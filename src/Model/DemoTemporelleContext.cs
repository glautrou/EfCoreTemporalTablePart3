using Microsoft.EntityFrameworkCore;

namespace EfCoreTemporalTablePart3.Model;

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
            optionsBuilder.UseSqlServer("data source=.;initial catalog=DemoTemporelleEf6;Integrated Security=SSPI");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Définitions sans temporalité
        modelBuilder.Entity<Employe>(entity =>
        {
            entity.Property(e => e.Nom)
                .IsRequired()
                .HasMaxLength(80);

            entity.Property(e => e.Prenom)
                .IsRequired()
                .HasMaxLength(80);

            entity.HasOne(d => d.Entreprise)
                .WithMany(p => p.Employe)
                .HasForeignKey(d => d.EntrepriseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employe_Entreprise");
        });

        modelBuilder.Entity<Entreprise>(entity =>
        {
            entity.Property(e => e.Adresse).HasMaxLength(80);
            entity.Property(e => e.Nom).HasMaxLength(80);

        });

        //Ajout temporalités
        modelBuilder
            .Entity<Employe>()
            .ToTable("Employe", b => b.IsTemporal());

        modelBuilder.Entity<Entreprise>()
            .ToTable(
                "Entreprise",
                b => b.IsTemporal(
                    b =>
                    {
                        //Changement des noms par défaut
                        b.HasPeriodStart("ValideDu");
                        b.HasPeriodEnd("ValideAu");
                        b.UseHistoryTable("EntrepriseHistorique");
                    }));

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
