using System;
using System.Collections.Generic;
using System.Text;

namespace SAE2_2
{
    public class Jeu
    {
        public Joueur[] Joueurs { get; set; }
        public Plateau Plateau { get; set; }
        public int JoueurCourant { get; set; } // Indice du joueur actuel dans le tableau joueurs

        public Jeu(Joueur j1, Joueur j2)
        {
            Plateau = new Plateau();
            Joueurs = new Joueur[2] { j1, j2 };
        }
    }
}
