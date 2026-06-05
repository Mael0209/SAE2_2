using System;
using System.Collections.Generic;
using System.Text;

namespace SAE2_2
{
    public enum TypeJoueur
    {
        Humain,
        IA
    }

    public class Joueur
    {
        public string Nom { get; set; }
        public TypeJoueur Type { get; set; }
        public Pion PionAssocie { get; set; }

        public Joueur(string nom, TypeJoueur type)
        {
            Nom = nom;
            Type = type;
        }

        public override string ToString()
        {
            return $"Nom: {Nom}, type: {Type}";
        }
    }
}
