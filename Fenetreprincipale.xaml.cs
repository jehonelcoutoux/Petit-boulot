using PtiBoulot.Models;
using PtiBoulot.Pages;
using PtiBoulot.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace PtiBoulot
{
    public partial class FenetrePrincipale : Window
    {
        private readonly Utilisateur _user;
        private readonly NotificationService _notifService = new();
        private DispatcherTimer _timer = new();

        public FenetrePrincipale()
        {
            InitializeComponent();
            _user = Session.UtilisateurConnecte!;
            AfficherInfosUtilisateur();
            ConfigurerNavigation();
            DemarrerTimerNotifications();
        }

        private void DemarrerTimerNotifications()
        {
            _timer.Interval = TimeSpan.FromSeconds(30);
            _timer.Tick += (s, e) => MettreAJourBadge();
            _timer.Start();
            MettreAJourBadge();
        }

        private void MettreAJourBadge()
        {
            var count = _notifService.CompterNonLues(_user.Id);
            if (count > 0)
            {
                BadgeNotif.Visibility = Visibility.Visible;
                TxtBadgeNotif.Text = count > 9 ? "9+" : count.ToString();
            }
            else
            {
                BadgeNotif.Visibility = Visibility.Collapsed;
            }
        }

        private void AfficherInfosUtilisateur()
        {
            TxtNomUtilisateur.Text = _user.NomComplet;
            TxtAvatar.Text = _user.Initiales;
            TxtRole.Text = _user.Role;

            AvatarBorder.Background = _user.Role switch
            {
                "Admin" => new SolidColorBrush(Color.FromRgb(139, 92, 246)),
                "Client" => new SolidColorBrush(Color.FromRgb(34, 197, 94)),
                "Prestataire" => new SolidColorBrush(Color.FromRgb(59, 130, 246)),
                _ => new SolidColorBrush(Color.FromRgb(100, 116, 139))
            };
        }

        private void ConfigurerNavigation()
        {
            NavPanel.Children.Clear();

            switch (_user.Role)
            {
                case "Admin":
                    AjouterNavItem("🏠  Tableau de bord", () => Naviguer(new TableauDeBordPage()));
                    AjouterNavItem("⚙  Administration", () => Naviguer(new AdministrationPage()));
                    break;

                case "Client":
                    AjouterNavItem("🏠  Tableau de bord", () => Naviguer(new TableauDeBordPage()));
                    AjouterNavItem("📋  Nouvelle offre", () => Naviguer(new NouvelleOffrePage()));
                    AjouterNavItem("👤  Prestataires", () => Naviguer(new PrestatairesPage()));
                    AjouterNavItem("⭐  Mes missions", () => Naviguer(new MissionsPage()));
                    break;

                case "Prestataire":
                    AjouterNavItem("🏠  Tableau de bord", () => Naviguer(new TableauDeBordPage()));
                    AjouterNavItem("🧳  Offres disponibles", () => Naviguer(new OffresDisponiblesPage()));
                    AjouterNavItem("⭐  Mes missions", () => Naviguer(new MissionsPage()));
                    break;
            }

            if (NavPanel.Children.Count > 0 && NavPanel.Children[0] is RadioButton first)
                first.IsChecked = true;
        }

        private void Naviguer(Page page)
        {
            MainFrame.Navigate(page);
        }

        private void AjouterNavItem(string label, Action action)
        {
            var btn = new RadioButton
            {
                Content = label,
                GroupName = "Nav",
                FontSize = 14,
                Padding = new Thickness(12, 10, 12, 10),
                Margin = new Thickness(0, 2, 0, 2),
                Cursor = System.Windows.Input.Cursors.Hand,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                Foreground = new SolidColorBrush(Color.FromRgb(100, 116, 139)),
                HorizontalContentAlignment = HorizontalAlignment.Left
            };

            btn.Checked += (s, e) =>
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(240, 253, 244));
                btn.Foreground = new SolidColorBrush(Color.FromRgb(34, 197, 94));
                action();
            };
            btn.Unchecked += (s, e) =>
            {
                btn.Background = Brushes.Transparent;
                btn.Foreground = new SolidColorBrush(Color.FromRgb(100, 116, 139));
            };

            NavPanel.Children.Add(btn);
        }

        private void BtnNotifications_Click(object sender, RoutedEventArgs e)
        {
            Naviguer(new NotificationsPage());
            _notifService.MarquerToutesCommeLues(_user.Id);
            MettreAJourBadge();
        }
        private void CarteUtilisateur_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Naviguer(new ProfilPage());
        }

        private void BtnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Voulez-vous vous déconnecter ?", "Déconnexion",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _timer.Stop();
                new AuthService().Deconnexion();
                var login = new MainWindow();
                login.Show();
                this.Close();
            }
        }
    }
}