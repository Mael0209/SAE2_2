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
    bool alerteMessage = false;
    string messageErreur = "";
    CouleursPion? couleurGagnant = null;
    bool matchNul = false;

    while (partie.Etat != EtatPartie.Finie && !matchNul)
    {
        Console.Clear();

        Joueur joueurCourant = partie.Joueurs[partie.JoueurActif];

        if (alerteMessage)
        {
            alerteMessage = false;
            Console.WriteLine(messageErreur);
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
            Console.WriteLine($"Choisissez une colonne (1 à {partie.Plateau.Largeur}) :");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int choix) && choix >= 1 && choix <= partie.Plateau.Largeur)
            {
                if (!partie.JouerCoup(choix - 1))
                {
                    alerteMessage = true;
                    messageErreur = "Cette colonne est pleine";
                }
                else
                {
                    partie.ChangerJoueur();
                }
            }
            else
            {
                alerteMessage = true;
                messageErreur = "Saisie invalide ! Veuillez entrer un nombre correct.";
            }
        }

        couleurGagnant = partie.TrouverCouleurGagnant();

        if (couleurGagnant is not null)
        {
            partie.Etat = EtatPartie.Finie;
        }
        else if (partie.Plateau.EstPlein())
        {
            matchNul = true;
        }
    }

    Console.Clear();
    partie.Plateau.Affiche();
    string? textGagnant = matchNul ? "Match nul" : Enum.GetName(couleurGagnant!.Value);
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