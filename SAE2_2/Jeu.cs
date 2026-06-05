using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SAE2_2
{
    public struct DataTour
    {
        public Joueur Gagnant { get; set; } = null;

        public DataTour(Joueur gagnant)
        {
            Gagnant = gagnant;
        }

        public override string ToString()
        {
            return $"Gagnant: {Gagnant.ToString()}";
        }
    }

    public class Jeu
    {
        public Joueur[] Joueurs { get; set; }
        public Collection<DataTour> dataTours { get; set; }
        public Partie Partie { get; set; }

        public Jeu(Joueur j1, Joueur j2)
        {
            Joueurs = new Joueur[2] { j1, j2 };
            dataTours = new Collection<DataTour>();
            Partie = null!;
        }

        public Partie DemarrerPartie()
        {
            if (Partie is not null)
            {
                Partie = new Partie(Joueurs);
                return Partie;
            }

            return Partie!;
        }

        public void AfficheData()
        {
            foreach(DataTour data in dataTours)
            {
                Console.WriteLine(data.ToString());
            }
            Console.WriteLine("\n");
        }
    }
}
