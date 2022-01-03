namespace EfCoreTemporalTablePart3.Model;

public partial class Entreprise
{
    public Entreprise()
    {
        Employe = new HashSet<Employe>();
    }

    public int Id { get; set; }
    public string Nom { get; set; }
    public string Adresse { get; set; }

    public virtual ICollection<Employe> Employe { get; set; }
}
