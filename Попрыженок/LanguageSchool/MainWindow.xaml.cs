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
using Microsoft.Win32;
using System.IO;

namespace LanguageSchool
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LanguageDBEntities dbmodel = new LanguageDBEntities();
        public MainWindow()
        {
            InitializeComponent();            
        }
        int captha;
        bool check = false;     

        /// <summary>
        /// Логика авторизации
        /// </summary>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                MessageBox.Show("Введите данные");
                return;
            }
            using (LanguageDBEntities jump = new LanguageDBEntities())
            {
                foreach (User user in jump.User)
                {
                    if (user.UserLogin == LoginTextBox.Text)
                    {
                        if (user.UserPassword == PasswordTextBox.Text)
                        {
                            check = false;
                            if (user.Role == "Пользователь")
                            {
                                check = false;
                                MessageBox.Show("Авторазация успешна", "Клиент");
                                LookAgentsForUsers window = new LookAgentsForUsers();
                                window.Show();
                                this.Close();
                                return;
                            }
                            else if (user.Role == "Администратор")
                            {
                                check = false;
                                MessageBox.Show("Авторазация успешна", "Администратор");
                                LookAgentsForStaff window = new LookAgentsForStaff();
                                window.Show();
                                this.Close();
                                return;
                            }
                        }
                        else
                        {
                            check = true;
                            MessageBox.Show("Неверный пароль");
                            LoginButton.Visibility = Visibility.Hidden;
                            GenerateCaptha();
                            return;
                        }
                    }
                    else
                    {
                        check = true;
                    }

                }
                if (check == true)
                {
                    LoginButton.Visibility = Visibility.Hidden;
                    GenerateCaptha();
                    check = false;
                    MessageBox.Show("Такой пользователь не существует");
                    return;
                }
            }
        }
        /// <summary>
        /// Логика создания капчи
        /// </summary>

        private void GenerateCaptha()
        {
            int min = 1000;
            int max = 9999;
            Random random = new Random();
            captha = random.Next(min, max);
            CapthaLabel.Content = "Капча: " + captha;
        }

        /// <summary>
        /// Логика проверки капчи
        /// </summary>

        private void CapthaButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CapthTextBox.Text))
            {
                MessageBox.Show("Введите данные");
                return;
            }
            else
            {
                if (int.Parse(CapthTextBox.Text) == captha)
                {
                    LoginButton.Visibility = Visibility.Visible;
                    check = false;
                }
                else
                {
                    MessageBox.Show("Проверь данные с капчой");
                    return;
                }
            }
        }
    }
}