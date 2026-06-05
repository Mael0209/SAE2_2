using System;
using System.Collections.Generic;
using System.Text;

namespace SAE2_2
{
    public class Plateau // A FAIRE : VERIFIER ALIGNEMENT
    {
        public Case[,] Cases { get; set; }
        public int Longueur { get; set; } = 6;
        public int Largeur { get; set; } = 7;

        public Plateau()
        {
            Cases = new Case[Longueur, Largeur];

            for (int i = 0; i < Longueur; i++)
                for (int j = 0; j < Largeur; j++)
                    Cases[i, j] = new Case(i, j);
        }

        public Plateau(int longueur, int largeur)
        {
            Longueur = longueur;
            Largeur = largeur;
            Cases = new Case[Longueur, Largeur];

            for (int i = 0; i < Longueur; i++)
                for (int j = 0; j < Largeur; j++)
                    Cases[i, j] = new Case(i, j);
        }

        public void AjouterPion(Pion pion, int colonne)
        {
            if (!ColonnePleine(colonne))
                for (int i = Longueur-1; i >= 0; i--)
                {
                    Case @case = Cases[i, colonne];
                    if (@case.Contenu == null)
                    {
                        @case.Contenu = pion;
                        break;
                    }
                }
        }

        /// <summary>
        /// Vérifie dans tout le plateau si un alignement de 4 pions existe
        /// </summary>
        /// <returns>la référence d'un point de l'alignement (peut être null)</returns>
        public Pion VerifierAlignement()
        {
            // On parcourt toute la grille
            for (int i = 0; i < Longueur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    Case caseCourante = Cases[i, j];

                    if (caseCourante.EstLibre())
                        continue;

                    Pion? pionCourant = caseCourante?.Contenu;

                    CouleursPion couleurCherchee = pionCourant.Couleur;

                    // Vérification Horizontale (vers la droite)
                    // L'opérateur ?. permet de vérifier si Contenu est null en toute sécurité
                    if (j + 3 < Largeur &&
                        Cases[i, j + 1].Contenu?.Couleur == couleurCherchee &&
                        Cases[i, j + 2].Contenu?.Couleur == couleurCherchee &&
                        Cases[i, j + 3].Contenu?.Couleur == couleurCherchee)
                    {
                        return pionCourant;
                    }

                    // Vérification Verticale (vers le bas)
                    if (i + 3 < Longueur &&
                        Cases[i + 1, j].Contenu?.Couleur == couleurCherchee &&
                        Cases[i + 2, j].Contenu?.Couleur == couleurCherchee &&
                        Cases[i + 3, j].Contenu?.Couleur == couleurCherchee)
                    {
                        return pionCourant;
                    }

                    // Vérification Diagonale (vers le bas à droite)
                    if (i + 3 < Longueur && j + 3 < Largeur &&
                        Cases[i + 1, j + 1].Contenu?.Couleur == couleurCherchee &&
                        Cases[i + 2, j + 2].Contenu?.Couleur == couleurCherchee &&
                        Cases[i + 3, j + 3].Contenu?.Couleur == couleurCherchee)
                    {
                        return pionCourant;
                    }

                    // Vérification Diagonale (vers le haut à droite)
                    if (i - 3 >= 0 && j + 3 < Largeur &&
                        Cases[i - 1, j + 1].Contenu?.Couleur == couleurCherchee &&
                        Cases[i - 2, j + 2].Contenu?.Couleur == couleurCherchee &&
                        Cases[i - 3, j + 3].Contenu?.Couleur == couleurCherchee)
                    {
                        return pionCourant;
                    }
                }
            }

            // Aucun gagnant trouvé
            return null;
        }

        public bool ColonnePleine(int nColonne) // On a juste besoin de vérifier la ligne la plus haute
        {
            return Cases[0, nColonne].Contenu != null;
        }

        public void Affiche()
        {
            Console.WriteLine("\n");
            for (int i = 0; i < Longueur; i++)
            {
                string texte = "";
                for (int j = 0; j < Largeur; j++)
                    texte += $" {Cases[i, j].ToString()} ";
                Console.WriteLine($"{texte}\n");
            }
        }
    }
}
