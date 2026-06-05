using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SAE2_2
{
    public class Plateau
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

        public Plateau Cloner()
        {
            Plateau plateau = new(Longueur, Largeur);
            for (int i = 0; i < Longueur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    if (this.Cases[i, j].Contenu != null)
                    {
                        plateau.Cases[i, j].Contenu = new Pion(Cases[i, j].Contenu!.Couleur);
                    }
                }
            }
            return plateau;
        }

        public bool AjouterPion(Pion pion, int colonne)
        {
            if (!ColonnePleine(colonne))
                for (int i = Longueur - 1; i >= 0; i--)
                {
                    Case @case = Cases[i, colonne];
                    if (@case.Contenu == null)
                    {
                        @case.Contenu = pion;
                        break;
                    }
                }
            return !ColonnePleine(colonne);
        }

        public Pion VerifierAlignement()
        {
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

            return null;
        }

        public bool EstPlein()
        {
            bool plein = true;

            for (int i = 0; i < Longueur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    if (Cases[i,j].Contenu is null)
                    {
                        plein = false;
                        break;
                    }
                }
            }

            return plein;
        }

        public bool ColonnePleine(int nColonne)
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
