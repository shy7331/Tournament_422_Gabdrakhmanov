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
    /// Логика взаимодействия для ViewerPage.xaml
    /// </summary>
    public partial class ViewerPage : Page
    {
        private TournamentDB_422_GabdraxmanovEntities db = new TournamentDB_422_GabdraxmanovEntities();

        public ViewerPage()
        {
            InitializeComponent();
            LoadTournaments();
            LoadMatches();
           
        }

        private void LoadTournaments()
        {
 
            TournamentsDataGrid.ItemsSource = db.Tournaments.ToList();
        }

        private void LoadMatches()
        {
            
            MatchesDataGrid.ItemsSource = db.Matches.ToList();
        }


        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            TournamentsDataGrid.ItemsSource = db.Tournaments
                .Where(t => t.Name.ToLower().Contains(searchText))
                .ToList();
        }
    }
}

