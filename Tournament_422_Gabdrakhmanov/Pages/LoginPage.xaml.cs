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

namespace Tournament_422_Gabdrakhmanov.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private  db = new Entities();

        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
       
                var user = db.Organizators.FirstOrDefault(o => o.Login == login);

                if (user != null && user.Password == password)
                {
                    MessageBox.Show("Успешный вход как организатор!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new OrganizerPage());
                    return;
                }

               
                var viewer = db.Viewers.FirstOrDefault(v => v.Login == login);

                if (viewer != null && viewer.Password == password)
                {
                    MessageBox.Show("Успешный вход как зритель!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new ViewerPage());
                    return;
                }


                var participant = db.Player.FirstOrDefault(p => p.NIckaname == login && p.Email == password);

                if (participant != null)
                {
                    MessageBox.Show("Успешный вход как участник!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                   
                    ParticipantPage.CurrentParticipant = participant;

                    NavigationService.Navigate(new ParticipantPage());
                    return;
                }

        
                MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
}
