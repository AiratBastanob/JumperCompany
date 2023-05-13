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
using System.Windows.Shapes;

namespace LanguageSchool
{
    /// <summary>
    /// Логика взаимодействия для LookAgentsForStaff.xaml
    /// </summary>
    public partial class LookAgentsForStaff : Window
    {
        public LookAgentsForStaff()
        {
            InitializeComponent();
            LoadComboBox1();
            LoadComboBox2();
        }

        /// <summary>
        /// Подгрузка данных для ComboBox
        /// </summary>
        private void LoadComboBox1()
        {

        }

        private void LoadComboBox2()
        {

        }

        /// <summary>
        /// Обработчик для ComboBox
        /// </summary>
        private void ComboBox_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
           
        }

        /// <summary>
        /// Обработчик для ComboBox
        /// </summary>
        private void ComboBox_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Логика обработки посика
        /// </summary>
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Img_Click_1(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Переход к последней странице
        /// </summary>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
