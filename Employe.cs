namespace EfCoreTemporalTablePart3;

public partial class Employe
{
    public int Id { get; set; }
    public string Prenom { get; set; }
    public string Nom { get; set; }
    public int EntrepriseId { get; set; }

    public virtual Entreprise Entreprise { get; set; }
}
