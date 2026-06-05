using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;

namespace SAE2_2
{
    public enum EtatPartie
    {
        En_cours,
        Finie
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
        public Joueur[] Joueurs { get; set; }
        public int JoueurActif { get; set; } = 0;

        public Partie(Joueur[] joueurs)
        {
            Plateau = new();
            Etat = EtatPartie.En_cours;
            Coups = new Collection<DataCoup>();
            Joueurs = joueurs;
        }

        public Partie(Joueur j1, Joueur j2)
        {
            Plateau = new();
            Etat = EtatPartie.En_cours;
            Coups = new Collection<DataCoup>();
            Joueurs = new Joueur[] { j1, j2 };
        }

        public Partie(Joueur j1, Joueur j2, int longeur, int largeur)
        {
            Plateau = new(longeur, largeur);
            Etat = EtatPartie.En_cours;
            Coups = new Collection<DataCoup>();
            Joueurs = new Joueur[] { j1, j2 };
        }

        public CouleursPion? TrouverCouleurGagnant()
        {
            Pion? pionGagnant = Plateau.VerifierAlignement();

            if (pionGagnant != null)
            {
                return pionGagnant.Couleur;
            }

            return null;
        }

        public Joueur ChangerJoueur()
        {
            JoueurActif = JoueurActif == 0 ? 1 : 0;
            return Joueurs[JoueurActif];
        }

        public bool JouerCoup(int colonne)
        {
            Pion pion = new Pion(Joueurs[JoueurActif].Couleur);
            Console.WriteLine(pion.Couleur);
            return Plateau.AjouterPion(pion, colonne);
        }

        public override string ToString()
        {
            string text = "";

            foreach (DataCoup coup in Coups)
            {
                text += $"{coup.ToString()}\n";
            }

            return text;
        }
    }
}
