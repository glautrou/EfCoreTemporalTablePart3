namespace EfCoreTemporalTablePart3;

public partial class Entreprise
{
    public Entreprise()
    {
        Employe = new HashSet<Employe>();
    }

    public int Id { get; set; }
    public string Nom { get; set; }
    public string Adresse { get; set; }
    public DateTime SysStartTime { get; set; }
    public DateTime SysEndTime { get; set; }

    public virtual ICollection<Employe> Employe { get; set; }
}
