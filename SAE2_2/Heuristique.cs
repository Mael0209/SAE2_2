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

        public int CalculerScoreHeuristique()
        {
            CouleursPion couleurAdversaire = Couleur == CouleursPion.Bleu ? CouleursPion.Rouge : CouleursPion.Bleu;

            Pion pionGagnant = Plateau.VerifierAlignement();
            if (pionGagnant != null)
            {
                if (pionGagnant.Couleur == Couleur)
                    return 100000;
                else
                    return -100000;
            }

            Score = Evaluation(Couleur) - Evaluation(couleurAdversaire);

            return Score;
        }

        private int Evaluation(CouleursPion couleurJoueur)
        {
            int nbCouples = 0;
            int nbTriples = 0;

            for (int i = 0; i < Plateau.Longueur; i++)
            {
                for (int j = 0; j < Plateau.Largeur; j++)
                {
                    Case @case = Plateau.Cases[i, j];

                    if (@case.Contenu != null && @case.Contenu.Couleur == couleurJoueur)
                    {
                        // 1. Horizontale
                        if (j + 1 < Plateau.Largeur && Plateau.Cases[i, j + 1].Contenu?.Couleur == couleurJoueur)
                        {
                            nbCouples++;
                            if (j + 2 < Plateau.Largeur && Plateau.Cases[i, j + 2].Contenu?.Couleur == couleurJoueur) nbTriples++;
                        }

                        // 2. Verticale
                        if (i + 1 < Plateau.Longueur && Plateau.Cases[i + 1, j].Contenu?.Couleur == couleurJoueur)
                        {
                            nbCouples++;
                            if (i + 2 < Plateau.Longueur && Plateau.Cases[i + 2, j].Contenu?.Couleur == couleurJoueur) nbTriples++;
                        }

                        // 3. Diagonale Descendante
                        if (i + 1 < Plateau.Longueur && j + 1 < Plateau.Largeur && Plateau.Cases[i + 1, j + 1].Contenu?.Couleur == couleurJoueur)
                        {
                            nbCouples++;
                            if (i + 2 < Plateau.Longueur && j + 2 < Plateau.Largeur && Plateau.Cases[i + 2, j + 2].Contenu?.Couleur == couleurJoueur) nbTriples++;
                        }

                        // 4. Diagonale Montante
                        if (i - 1 >= 0 && j + 1 < Plateau.Largeur && Plateau.Cases[i - 1, j + 1].Contenu?.Couleur == couleurJoueur)
                        {
                            nbCouples++;
                            if (i - 2 >= 0 && j + 2 < Plateau.Largeur && Plateau.Cases[i - 2, j + 2].Contenu?.Couleur == couleurJoueur) nbTriples++;
                        }
                    }
                }
            }

            return nbCouples + nbTriples;
        }
    }
}
