using System;
using System.Collections.Generic;
using System.Text;

namespace SAE2_2
{
    public enum CouleursPion
    {
        Bleu,
        Rouge
    }

    public class Pion
    {
        public CouleursPion Couleur { get; set; }

        public Pion(CouleursPion couleur)
        {
            Couleur = couleur;
        }
    }
}
