using System.Windows;
using System.Windows.Media;
using PtiBoulot.Data;
using PtiBoulot.Models;

namespace PtiBoulot
{
    public partial class InscriptionWindow : Window
    {
        public InscriptionWindow()
        {
            InitializeComponent();
        }

        private void RbRole_Checked(object sender, RoutedEventArgs e)
        {
            if (PanelPrestataire == null) return;
            PanelPrestataire.Visibility = RbPrestataire.IsChecked == true
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void BtnInscrire_Click(object sender, RoutedEventArgs e)
        {
            TxtErreur.Visibility = Visibility.Collapsed;

            // Validation
            if (string.IsNullOrWhiteSpace(TxtNom.Text))
            {
                Erreur("Le nom complet est obligatoire."); return;
            }
            if (string.IsNullOrWhiteSpace(TxtEmail.Text))
            {
                Erreur("L'email est obligatoire."); return;
            }
            if (TxtPassword.Password.Length < 6)
            {
                Erreur("Le mot de passe doit contenir au moins 6 caractères."); return;
            }
            if (string.IsNullOrWhiteSpace(TxtVille.Text))
            {
                Erreur("La ville est obligatoire."); return;
            }

            try
            {
                using var db = new AppDbContext();

                // Vérifier email unique
                if (db.Utilisateurs.Any(u => u.Email == TxtEmail.Text.Trim()))
                {
                    Erreur("Cet email est déjà utilisé."); return;
                }

                var role = RbPrestataire.IsChecked == true ? "Prestataire" : "Client";

                var user = new Utilisateur
                {
                    NomComplet = TxtNom.Text.Trim(),
                    Email = TxtEmail.Text.Trim(),
                    MotDePasse = BCrypt.Net.BCrypt.HashPassword(TxtPassword.Password),
                    Role = role,
                    Ville = TxtVille.Text.Trim()
                };

                if (role == "Prestataire")
                {
                    user.Competences = TxtCompetences.Text.Trim();
                    if (decimal.TryParse(TxtTarif.Text, out decimal tarif))
                        user.TarifHoraire = tarif;
                }

                db.Utilisateurs.Add(user);
                db.SaveChanges();

                MessageBox.Show("✅ Compte créé avec succès ! Connectez-vous.",
                    "Bienvenue", MessageBoxButton.OK, MessageBoxImage.Information);

                var login = new MainWindow();
                login.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                Erreur($"Erreur : {ex.Message}");
            }
        }

        private void Erreur(string msg)
        {
            TxtErreur.Text = msg;
            TxtErreur.Visibility = Visibility.Visible;
        }

        private void LinkConnexion_Click(object sender, RoutedEventArgs e)
        {
            var login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}