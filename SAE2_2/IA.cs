using System;
using System.Collections.Generic;
using System.Text;

namespace SAE2_2
{
    public class IA
    {
        private int ProfondeurMax;

        public IA(int profondeur)
        {
            ProfondeurMax = profondeur;
        }

        /// <summary>
        /// Méthode appelée par le jeu pour obtenir la colonne choisie par l'IA.
        /// </summary>
        /// <returns>La meilleur colonne à choisir</returns>
        public int ChoisirCoup(Plateau plateauActuel, CouleursPion couleurIA)
        {
            int meilleurScore = int.MinValue;
            int meilleureColonne = 0;

            for (int col = 0; col < plateauActuel.Largeur; col++)
            {
                if (!plateauActuel.ColonnePleine(col))
                {
                    Plateau copiePlateau = plateauActuel.Cloner();
                    copiePlateau.AjouterPion(new Pion(couleurIA), col);

                    int score = MinimaxAlphaBeta(copiePlateau, ProfondeurMax - 1, int.MinValue, int.MaxValue, false, couleurIA);

                    if (score > meilleurScore)
                    {
                        meilleurScore = score;
                        meilleureColonne = col;
                    }
                }
            }

            return meilleureColonne;
        }

        private int MinimaxAlphaBeta(Plateau plateau, int profondeur, int alpha, int beta, bool estMax, CouleursPion couleurIA)
        {
            Heuristique h = new Heuristique(couleurIA, plateau);

            // si un joueur a gagné ou perdu sur ce plateau simulé
            Pion pionGagnant = plateau.VerifierAlignement();
            if (pionGagnant != null)
            {
                return h.CalculerScoreHeuristique();
            }

            if (profondeur == 0)
            {
                return h.CalculerScoreHeuristique();
            }

            CouleursPion couleurAdversaire = (couleurIA == CouleursPion.Bleu) ? CouleursPion.Rouge : CouleursPion.Bleu;

            if (estMax)
            {
                int meilleurScore = int.MinValue;

                for (int col = 0; col < plateau.Largeur; col++)
                {
                    if (!plateau.ColonnePleine(col))
                    {
                        Plateau simulation = plateau.Cloner();
                        simulation.AjouterPion(new Pion(couleurIA), col);

                        int score = MinimaxAlphaBeta(simulation, profondeur - 1, alpha, beta, false, couleurIA);
                        meilleurScore = Math.Max(meilleurScore, score);

                        // Élagage
                        alpha = Math.Max(alpha, score);
                        if (beta <= alpha) break;
                    }
                }
                return meilleurScore;
            }
            else
            {
                int meilleurScore = int.MaxValue;

                for (int col = 0; col < plateau.Largeur; col++)
                {
                    if (!plateau.ColonnePleine(col))
                    {
                        Plateau simulation = plateau.Cloner();
                        simulation.AjouterPion(new Pion(couleurAdversaire), col);

                        int score = MinimaxAlphaBeta(simulation, profondeur - 1, alpha, beta, true, couleurIA);
                        meilleurScore = Math.Min(meilleurScore, score);

                        // Élagage
                        beta = Math.Min(beta, score);
                        if (beta <= alpha) break;
                    }
                }
                return meilleurScore;
            }
        }
    }
}