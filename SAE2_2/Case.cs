using System;
using System.Collections.Generic;
using System.Text;

namespace SAE2_2
{
    public class Case
    {
        public int Ligne { get; set; }
        public int Colonne { get; set; }
        public Pion? Contenu { get; set; } // Contient un Pion par référence. Est créé à la valeur null

        public Case(int ligne, int colonne)
        {
            Ligne = ligne;
            Colonne = colonne;
        }

        public Case(int ligne, int colonne, Pion contenu)
        {
            Ligne = ligne;
            Colonne = colonne;
            Contenu = contenu;
        }

        public bool EstLibre()
        {
            return Contenu == null;
        }

        public override string ToString()
        {
            return "-";
        }
    }
}
