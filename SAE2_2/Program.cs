using SAE2_2;

Joueur creerJoueur()
{
    string? nom;
    TypeJoueur type;
    int choix;

    do
    {
        Console.WriteLine("Saisissez le nom du joueur : ");
        nom = Console.ReadLine();
    } while (nom.IsWhiteSpace());

    do
    {
        Console.WriteLine("Joueur ou IA ? \n1- Joueur \n2- IA");
        choix = Convert.ToInt32(Console.ReadLine());
    } while (choix != 1 && choix != 2);

    type = choix == 1 ? TypeJoueur.Humain : TypeJoueur.IA;

    Console.Clear();

    return new Joueur(nom, type);
}

Jeu initJeu(Joueur j1, Joueur j2)
{
    return new Jeu(j1, j2);
}

void lancerPartie(Jeu jeu)
{
    Partie partie = new(jeu.Joueurs[0], jeu.Joueurs[1]);
    bool alertePleine = false;
    CouleursPion? couleurGagnant = null;

    while (partie.Etat != EtatPartie.Finie)
    {
        Console.Clear();

        if (alertePleine)
        {
            alertePleine = false;
            Console.WriteLine("Cette colonne est pleine");
        }

        Console.WriteLine($"Joueur: {partie.Joueurs[partie.JoueurActif].Nom}");
        Console.WriteLine("Plateau: ");
        partie.Plateau.Affiche();

        Console.WriteLine("Choisissez une colonne");

        int choix = Convert.ToInt32(Console.ReadLine());

        if (!partie.JouerCoup(choix - 1))
        {
            alertePleine = true;
        }
        else
        {
            partie.ChangerJoueur();
        }

        couleurGagnant = partie.TrouverCouleurGagnant();

        if (couleurGagnant is not null)
        {
            partie.Etat = EtatPartie.Finie;
        }
    }

    Console.Clear();
    Console.WriteLine($"Partie finie ! \nGagnant: {couleurGagnant}");
}

Joueur j1 = creerJoueur(), j2 = creerJoueur();

j2.Couleur = CouleursPion.Rouge; // A CHANGER

Jeu jeu = initJeu(j1, j2);

bool partieFinie = false;

while (!partieFinie)
{
    int choix;
    do
    {
        Console.WriteLine("1- Lancer partie");
        choix = Convert.ToInt32(Console.ReadLine());

        switch (choix)
        {
            case 1:
                lancerPartie(jeu);
                break;
            default:
                break;
        }
    } while (choix != 0);
}