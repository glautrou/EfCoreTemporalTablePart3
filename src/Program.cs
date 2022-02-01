// See https://aka.ms/new-console-template for more information
using EfCoreTemporalTablePart3.Model;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("DEBUT");

var db = new DemoTemporelleContext();
//Re-création DB
//db.Database.EnsureDeleted();
//db.Database.EnsureCreated();

//Ajout d'un jeu de données :
Console.WriteLine("Ajout d'un jeu de données...");
//- Entreprises
var entreprise1 = new Entreprise { Nom = "Webnet", Adresse = "1, avenue de la Cristallerie 92310 SEVRES" };
db.Entreprise.Add(entreprise1);
var entreprise2 = new Entreprise { Nom = "Microsoft", Adresse = "37 Quai du Président Roosevelt 92130 Issy-les-Moulineaux" };
db.Entreprise.Add(entreprise2);
//- Employés
var employe1 = new Employe { Prenom = "Gilles", Nom = "Lautrou", Entreprise = entreprise1 };
db.Employe.Add(employe1);
var employe2 = new Employe { Prenom = "Patricia", Nom = "Martin", Entreprise = entreprise2 };
db.Employe.Add(employe2);
var employe3 = new Employe { Prenom = "Jacques", Nom = "Dupont", Entreprise = entreprise2 };
db.Employe.Add(employe3);
//- Persist
db.SaveChanges();

var dateAjout = DateTime.Now;
Thread.Sleep(5_000); //Attente 5 secondes pour mieux différencier les dates

//Modification du jeu de données :
Console.WriteLine("Modification du jeu de données...");
//- "Patricia Martin" change de nom en "Martin Durant"
employe2.Nom = "Martin Durant";
//- "Jacques Dupont" rejoint "Webnet"
employe3.EntrepriseId = 1;
//- Renommer "Microsoft" en "Microsoft France"
entreprise2.Nom = "Microsoft France";
//- Persist
db.SaveChanges();

//Requêtage sans temporalité
Console.WriteLine("Requêtage sans temporalité...");
db.Employe
    .Select(i => $"\t- {i.Prenom} {i.Nom} - {i.Entreprise.Nom}")
    .ToList()
    .ForEach(i => Console.WriteLine(i)
);

//Requêtage avec temporalité
Console.WriteLine("Requêtage avec temporalité...");
Console.WriteLine("\tDonnées telles que lors du 1er ajout en début de programme :");
db.Employe
    .TemporalAsOf(dateAjout.ToUniversalTime()) //Temporal=UTC, donc ne pas oublier ToUniversalTime()
    .Select(i => $"\t\t- {i.Prenom} {i.Nom} - {i.Entreprise.Nom}")
    .ToList()
    .ForEach(i => Console.WriteLine(i)
);
Console.WriteLine("\tGlobalité des données (anciennes + actuelles) :");
db.Employe
    .TemporalAll()
    .OrderBy(i => EF.Property<DateTime>(i, "PeriodStart"))
    .Select(i => $"\t\t- {i.Prenom} {i.Nom} - début = {EF.Property<DateTime>(i, "PeriodStart")}, fin = {EF.Property<DateTime>(i, "PeriodEnd")}")
    .ToList()
    .ForEach(i => Console.WriteLine(i)
);

//Restore
Console.WriteLine("\tRestauration donnée historique :");
//- Restaurer ancienne version des données : "Microsoft France" en "Microsoft"
Console.WriteLine("\t> Ancienne version existante :");
db = new DemoTemporelleContext();
var entrepriseModifiee = db.Entreprise
    .TemporalAll()
    .Where(i => i.Id == entreprise2.Id)
    .OrderBy(i => EF.Property<DateTime>(i, "ValideDu"))
    .First();
db.Entry(entrepriseModifiee).State = EntityState.Modified;
db.SaveChanges();
db.Entreprise
    .Select(i => $"\t\t- {i.Nom}")
    .ToList()
    .ForEach(i => Console.WriteLine(i)
);
//- Restaurer donnée supprimée : Jacques Dupont
db = new DemoTemporelleContext();
Console.WriteLine("\t> Donnée supprimée :");
var idEmployeSupprime = employe3.Id;
db.Employe.Remove(employe3);
db.SaveChanges();
db.Employe
    .Select(i => $"\t\t- Avant : {i.Prenom} {i.Nom}")
    .ToList()
    .ForEach(i => Console.WriteLine(i)
);
var employeSupprimee = db.Employe
    .TemporalAll()
    .Where(i => i.Id == idEmployeSupprime)
    .OrderBy(i => EF.Property<DateTime>(i, "PeriodStart"))
    .Last();
employeSupprimee.Id = 0; //Auto-increment, nécessaire de réinitialiser pour éviter exception
db.Employe.Add(employeSupprimee);
db.SaveChanges();
db.Employe
    .Select(i => $"\t\t- Après : {i.Prenom} {i.Nom}")
    .ToList()
    .ForEach(i => Console.WriteLine(i)
);

Console.WriteLine("FIN");