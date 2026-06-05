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
        public Joueur? gagnant { get; set; } // null si aucun gagnant

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
                for (int i = Longueur; i >= 0; i--)
                {
                    Case @case = Cases[i, colonne];
                    if (@case.Contenu == null)
                    {
                        @case.Contenu = pion;
                        break;
                    }
                }
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
                    texte += $" {Cases[i,j].ToString()} ";
                Console.WriteLine($"{texte}\n");
            }
        }
    }
}
