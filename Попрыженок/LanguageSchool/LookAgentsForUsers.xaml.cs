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
    /// Логика взаимодействия для LookAgents.xaml
    /// </summary>
    public partial class LookAgentsForUsers : Window
    {
        public LookAgentsForUsers()
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
        /// Логика обработки посика
        /// </summary>
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
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

        private void DiscountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PriceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
