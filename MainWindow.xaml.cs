using System.Windows;
using PtiBoulot.Services;

namespace PtiBoulot
{
    public partial class MainWindow : Window
    {
        private readonly AuthService _auth = new();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void LinkInscription_Click(object sender, RoutedEventArgs e)
        {
            var inscription = new InscriptionWindow();
            inscription.Show();
            this.Close();
        }
        private void BtnConnecter_Click(object sender, RoutedEventArgs e)
        {
            TxtErreur.Visibility = Visibility.Collapsed;

            var email = TxtEmail.Text.Trim();
            var motDePasse = TxtPassword.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(motDePasse))
            {
                TxtErreur.Text = "Veuillez remplir tous les champs.";
                TxtErreur.Visibility = Visibility.Visible;
                return;
            }

            var (succes, message, utilisateur) = _auth.Connexion(email, motDePasse);

            if (succes)
            {
                var fenetre = new FenetrePrincipale();
                fenetre.Show();
                this.Close();
            }
            else
            {
                TxtErreur.Text = message;
                TxtErreur.Visibility = Visibility.Visible;
            }
        }
    }
}