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
    /// Логика взаимодействия для ParticipantPage.xaml
    /// </summary>
    public partial class ParticipantPage : Page
    {
        private TournamentDB_422_GabdraxmanovEntities db = new TournamentDB_422_GabdraxmanovEntities();

        public static Player CurrentParticipant { get; set; }

        public ParticipantPage()
        {
            InitializeComponent();
            LoadParticipantData();
        
         
            if (CurrentParticipant.Role == 1)
            {
               
                LoadTeams();
                LoadTournamentsForTeams();
            }
            else
            {
               
                TeamComboBox.IsEnabled = false;
                TournamentForTeamComboBox.IsEnabled = false;
                MessageBox.Show("Вы не являетесь капитаном команды и не можете управлять командами.",
                    "Доступ ограничен", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            LoadParticipantData(); 
        }

        private void LoadParticipantData()
        {
            if (CurrentParticipant != null)
            {
                NameTextBlock.Text = CurrentParticipant.Name;
                NicknameTextBlock.Text = CurrentParticipant.NIckaname;
                EmailTextBlock.Text = CurrentParticipant.Email;
            }
        }
       
        private void LoadTeams()
        {
            try
            {
         
                if (CurrentParticipant.Role != 1)
                {
                    MessageBox.Show("Вы не являетесь капитаном команды и не можете управлять командами.",
                        "Доступ запрещен", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

           
                var teams = db.Team
                    .Where(t => db.Player.Any(p => p.TeamId == t.Id && p.Id == CurrentParticipant.Id && p.Role == 1))
                    .Select(t => new
                    {
                        t.Id,
                        t.Name
                    })
                    .ToList();

                TeamComboBox.ItemsSource = teams;
                TeamComboBox.DisplayMemberPath = "Name";
                TeamComboBox.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки команд: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void LoadTournamentsForTeams()
        {
            try
            {
            
                var tournaments = db.Tournaments
                    .Select(t => new
                    {
                        t.Id,
                        t.Name
                    })
                    .ToList();
                TournamentForTeamComboBox.ItemsSource = tournaments;
                TournamentForTeamComboBox.DisplayMemberPath = "Name";
                TournamentForTeamComboBox.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки турниров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RegisterTeamForTournamentButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeamComboBox.SelectedValue == null || TournamentForTeamComboBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите команду и турнир для регистрации.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

      
            if (CurrentParticipant.Role != 1)
            {
                MessageBox.Show("Только капитаны могут регистрировать команды на турниры.", "Доступ запрещен", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int selectedTeamId = (int)TeamComboBox.SelectedValue;
            int selectedTournamentId = (int)TournamentForTeamComboBox.SelectedValue;

            try
            {
           
                var existingRegistration = db.TournamentTeam.FirstOrDefault(tt =>
                    tt.TournamentId == selectedTournamentId && tt.TeamId == selectedTeamId);

                if (existingRegistration != null)
                {
                    MessageBox.Show("Команда уже зарегистрирована на этот турнир.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

     
                var registration = new TournamentTeam
                {
                    TournamentId = selectedTournamentId,
                    TeamId = selectedTeamId,
                    RegistrationDate = DateTime.Now
                };

                db.TournamentTeam.Add(registration);
                db.SaveChanges();

                MessageBox.Show("Команда успешно зарегистрирована на турнир!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации команды: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

     
    }
}
