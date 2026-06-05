using SAE2_2;

Joueur j1 = new("aaaa", TypeJoueur.Humain, CouleursPion.Bleu);
Joueur j2 = new("bbbbnn", TypeJoueur.IA, CouleursPion.Rouge);

Jeu jeu = new(j1, j2);