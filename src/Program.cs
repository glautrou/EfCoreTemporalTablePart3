// See https://aka.ms/new-console-template for more information
using EfCoreTemporalTablePart3.Model;

Console.WriteLine("DEBUT");

var db = new DemoTemporelleContext();

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
employe3.EntrepriseId = 2;
//- Renommer "Microsoft" en "Microsoft France"
entreprise2.Nom = "Microsoft France";
//- Persist
db.SaveChanges();

Console.WriteLine("FIN");