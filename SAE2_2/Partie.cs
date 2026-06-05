using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SAE2_2
{
    public enum EtatPartie
    {
        En_cours,
        finie
    }

    public struct DataCoup
    {
        public Joueur Joueur { get; set; }
        public int Ligne { get; set; }
        public int Colonne { get; set; }

        public DataCoup(Joueur joueur, int ligne, int colonne)
        {
            Joueur = joueur;
            Ligne = ligne;
            Colonne = colonne;
        }

        public override string ToString()
        {
            return $"----\n{Joueur.ToString()} \nPosition: {Ligne} {Colonne}\n----";
        }
    }

    public class Partie
    {
        public EtatPartie Etat { get; set; }
        public Collection<DataCoup> Coups { get; set; }
        public Plateau Plateau { get; set; }

        public Partie()
        {
            Plateau = new();
            Etat = EtatPartie.En_cours;
            Coups = new Collection<DataCoup>();
        }

        public Partie(int longeur, int largeur)
        {
            Plateau = new(longeur, largeur);
            Etat = EtatPartie.En_cours;
            Coups = new Collection<DataCoup>();
        }

        public CouleursPion TrouverCouleurGagnant()
        {
            Pion? pionGagnant = Plateau.VerifierAlignement();

            return pionGagnant.Couleur;
        }

        public override string ToString()
        {
            string text = "";
            
            foreach(DataCoup coup in Coups)
            {
                text += $"{coup.ToString}\n";
            }

            return text;
        }
    }
}
