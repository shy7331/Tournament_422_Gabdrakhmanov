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
    /// Логика взаимодействия для CreateTournamentPage.xaml
    /// </summary>
    public partial class CreateTournamentPage : Page
    {
        private TournamentDB_422_GabdraxmanovEntities db = new TournamentDB_422_GabdraxmanovEntities();

        public CreateTournamentPage()
        {
            InitializeComponent();
            LoadDropdowns();
        }

        private void LoadDropdowns()
        {
            // Загрузка списка игр
            GameComboBox.ItemsSource = db.Game.ToList();
            GameComboBox.DisplayMemberPath = "Name";
            GameComboBox.SelectedValuePath = "Id";

            // Загрузка списка форматов
            FormatComboBox.ItemsSource = db.Format.ToList();
            FormatComboBox.DisplayMemberPath = "Name";
            FormatComboBox.SelectedValuePath = "Id";

            // Загрузка статусов
            StatusComboBox.ItemsSource = db.Status.ToList();
            StatusComboBox.DisplayMemberPath = "Name";
            StatusComboBox.SelectedValuePath = "Id";
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Валидация данных
            if (string.IsNullOrEmpty(NameTextBox.Text) ||
                GameComboBox.SelectedItem == null ||
                FormatComboBox.SelectedItem == null ||
                StatusComboBox.SelectedItem == null ||
                !DatePicker.SelectedDate.HasValue ||
                string.IsNullOrEmpty(PrizeFundTextBox.Text) ||
                string.IsNullOrEmpty(ParticipantsTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(PrizeFundTextBox.Text, out int prizeFund) ||
                !int.TryParse(ParticipantsTextBox.Text, out int participants))
            {
                MessageBox.Show("Призовой фонд и количество участников должны быть числовыми.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Создание турнира
            var newTournament = new Tournaments
            {
                Name = NameTextBox.Text,
                GameId = (int)GameComboBox.SelectedValue,
                FormatId = (int)FormatComboBox.SelectedValue,
                Date = DatePicker.SelectedDate.Value,
                PrizeFund = prizeFund,
                AmountParticipants = participants,
                StatusId = (int)StatusComboBox.SelectedValue
            };

            db.Tournaments.Add(newTournament);

            try
            {
                db.SaveChanges();
                MessageBox.Show("Турнир успешно создан!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                // Возврат к странице организатора
                NavigationService.Navigate(new OrganizerPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Возврат к странице организатора
            NavigationService.Navigate(new OrganizerPage());
        }
    }
}
