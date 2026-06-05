using SAE2_2;

Joueur creerJoueur(List<string> couleursDispo)
{
    string? nom;
    TypeJoueur type;
    int choix;
    CouleursPion couleur;

    do
    {
        Console.WriteLine("Saisissez le nom du joueur : ");
        nom = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(nom));

    do
    {
        Console.WriteLine("Joueur ou IA ? \n1- Joueur \n2- IA");
    } while (!int.TryParse(Console.ReadLine(), out choix) || (choix != 1 && choix != 2));

    type = choix == 1 ? TypeJoueur.Humain : TypeJoueur.IA;

    string couleurChoisie;

    if (couleursDispo.Count == 1)
    {
        couleurChoisie = couleursDispo.First();
    }
    else
    {
        int choixCouleur = -1;
        do
        {
            Console.WriteLine("Couleur: ");
            for (int i = 0; i < couleursDispo.Count; i++)
            {
                Console.WriteLine($"{i}- {couleursDispo[i]}");
            }

            if (!int.TryParse(Console.ReadLine(), out choixCouleur))
            {
                Console.WriteLine("Saisissez un nombre valide.");
                choixCouleur = -1;
            }
        } while (choixCouleur < 0 || choixCouleur >= couleursDispo.Count);

        couleurChoisie = couleursDispo[choixCouleur];
    }

    couleur = couleurChoisie == "Bleu" ? CouleursPion.Bleu : CouleursPion.Rouge;

    couleursDispo.Remove(couleurChoisie);

    Console.Clear();

    return new Joueur(nom, type, couleur);
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

        Joueur joueurCourant = partie.Joueurs[partie.JoueurActif];

        if (alertePleine)
        {
            alertePleine = false;
            Console.WriteLine("Cette colonne est pleine");
        }

        Console.WriteLine($"Joueur: {joueurCourant.Nom}");
        Console.WriteLine("Plateau: ");
        partie.Plateau.Affiche();

        if (joueurCourant.Type == TypeJoueur.IA)
        {
            Console.WriteLine($"L'IA {joueurCourant.Nom} réfléchit...");
            System.Threading.Thread.Sleep(1000);

            IA ia = new(5);
            partie.JouerCoup(ia.ChoisirCoup(partie.Plateau, joueurCourant.Couleur));

            partie.ChangerJoueur();
        }
        else
        {
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
        }

        couleurGagnant = partie.TrouverCouleurGagnant();

        if (couleurGagnant is not null)
        {
            partie.Etat = EtatPartie.Finie;
        }
    }

    Console.Clear();
    string? textGagnant = couleurGagnant is null ? "Aucun gagnant" : Enum.GetName(couleurGagnant.Value);
    Console.WriteLine($"Partie finie ! \nGagnant: {textGagnant}");
}

List<string> couleursDispo = Enum.GetNames(typeof(CouleursPion)).ToList();

Joueur j1 = creerJoueur(couleursDispo), j2 = creerJoueur(couleursDispo);

Jeu jeu = initJeu(j1, j2);

bool partieFinie = false;

while (!partieFinie)
{
    int choix;
    do
    {
        Console.WriteLine("1- Lancer partie \n0- arrêter partie");
        choix = Convert.ToInt32(Console.ReadLine());

        switch (choix)
        {
            case 1:
                lancerPartie(jeu);
                break;
            case 0:
                partieFinie = true;
                break;
            default:
                break;
        }
    } while (choix != 0);
}