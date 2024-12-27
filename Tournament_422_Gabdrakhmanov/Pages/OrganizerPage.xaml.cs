using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tournament_422_Gabdrakhmanov.Components;

namespace Tournament_422_Gabdrakhmanov.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrganizerPage.xaml
    /// </summary>
    public partial class OrganizerPage : Page
    {
        private TournamentDB_422_GabdraxmanovEntities db = new TournamentDB_422_GabdraxmanovEntities();

        public OrganizerPage()
        {
            InitializeComponent();
            LoadTournaments();
            LoadMatches();
            LoadParticipants();
        }

        private void LoadTournaments()
        {
            TournamentsDataGrid.ItemsSource = db.Tournaments.ToList();
        }

        private void LoadMatches()
        {
            MatchesDataGrid.ItemsSource = db.Matches.ToList();
        }

        private void LoadParticipants()
        {
            ParticipantsDataGrid.ItemsSource = db.Player.ToList();
        }

        private void CreateTournamentButton_Click(object sender, RoutedEventArgs e)
        {
           
            NavigationService.Navigate(new CreateTournamentPage());
        }
    }
}

