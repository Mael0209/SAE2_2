using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;

namespace SAE2_2
{
    public class Heuristique
    {
        public CouleursPion Couleur { get; set; }
        public int Score { get; private set; }
        public Plateau Plateau { get; set; }

        public Heuristique(CouleursPion couleur, Plateau plateau)
        {
            Couleur = couleur;
            Score = 0;
            Plateau = plateau.Cloner();
        }

        public int Evaluation()
        {
            int nbCouples = 0, nbTriples = 0;

            for (int i = 0; i < Plateau.Longueur; i++)
            {
                for (int j = 0; j < Plateau.Largeur; j++)
                {
                    Case @case = Plateau.Cases[i, j];

                    if (@case.Contenu != null && @case.Contenu.Couleur == Couleur)
                    {
                        // Vérification horizontale vers la droite
                        if (j + 1 < Plateau.Largeur && Plateau.Cases[i, j + 1].Contenu?.Couleur == Couleur)
                        {
                            nbCouples++;
                            if (j + 2 < Plateau.Largeur && Plateau.Cases[i, j + 2].Contenu?.Couleur == Couleur)
                            {
                                nbTriples++;
                            }
                        }

                        // Vérification verticale vers le bas
                        if (i + 1 < Plateau.Longueur && Plateau.Cases[i + 1, j].Contenu?.Couleur == Couleur)
                        {
                            nbCouples++;
                            if (i + 2 < Plateau.Longueur && Plateau.Cases[i + 2, j].Contenu?.Couleur == Couleur)
                            {
                                nbTriples++;
                            }
                        }

                        // Diagonale Descendante (haut-gauche vers bas-droite)
                        if (i + 1 < Plateau.Longueur && j + 1 < Plateau.Largeur && Plateau.Cases[i + 1, j + 1].Contenu?.Couleur == Couleur)
                        {
                            nbCouples++;
                            if (i + 2 < Plateau.Longueur && j + 2 < Plateau.Largeur && Plateau.Cases[i + 2, j + 2].Contenu?.Couleur == Couleur)
                            {
                                nbTriples++;
                            }
                        }

                        // Diagonale Montante (bas-gauche vers haut-droite)
                        if (i - 1 >= 0 && j + 1 < Plateau.Largeur && Plateau.Cases[i - 1, j + 1].Contenu?.Couleur == Couleur)
                        {
                            nbCouples++;
                            if (i - 2 >= 0 && j + 2 < Plateau.Largeur && Plateau.Cases[i - 2, j + 2].Contenu?.Couleur == Couleur)
                            {
                                nbTriples++;
                            }
                        }
                    }
                }
            }

            return nbCouples + nbTriples;
        }
    }
}
