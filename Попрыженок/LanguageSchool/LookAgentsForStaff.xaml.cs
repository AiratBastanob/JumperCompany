﻿using System;
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
using Microsoft.Win32;
using System.IO;
using System.Globalization;
using System.Windows.Controls.Primitives;

namespace LanguageSchool
{ 

    /// <summary>
    /// Логика взаимодействия для LookAgentsForStaff.xaml
    /// </summary>
    public partial class LookAgentsForStaff : Window
    {
        LanguageDBEntities dbmodel = new LanguageDBEntities();
        Agent _currentAgent = new Agent();
        private int selectPriority, selectName;
        List<Agent> agents = new List<Agent>();
        private string FilePath { get; set; }
        private byte[] imageData;
        public LookAgentsForStaff()
        {
            InitializeComponent();
            DataContext = _currentAgent;
            DataGridAgent.ItemsSource = dbmodel.Agent.ToList();
            DataContext = _currentAgent;
            LoadComponent(false);
            LoadComboBox1();
            LoadComboBox2();
        }

        /// <summary>
        /// Логика закрашивание агента
        /// </summary>
        private void DataGridAgent_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            // получаем текущего агента из строки
            Agent agent = e.Row.Item as Agent;

            // находим сумму количества реализуемого товара по данному агенту
            int totalProductionQuantity = dbmodel.ProductSale.Where(ps => ps.IdAgent == agent.Id).Sum(ps => ps.ProductionQuantity) ?? 0;

            // устанавливаем цвет имени агента в зависимости от суммы
            if (totalProductionQuantity <= 10000)
            {
            }
            else if (totalProductionQuantity >= 10000 && totalProductionQuantity <= 50000)
            {
            }
            else if (totalProductionQuantity >= 50000 && totalProductionQuantity <= 150000)
            {
            }
            else if (totalProductionQuantity >= 150000 && totalProductionQuantity <= 500000)
            {
            }
            else if (totalProductionQuantity > 500000)
            {
                e.Row.Foreground = Brushes.LightGreen;
            }
        }

        /// <summary>
        /// Подгрузка данных для ComboBox
        /// </summary>
        private void LoadComboBox1()
        {
            PriorityComboBox.Items.Add("По возрастанию приоритет");
            PriorityComboBox.Items.Add("По убыванию приоритет");
            PriorityComboBox.Items.Add("По умолчанию приоритет");           
        }

        private void LoadComboBox2()
        {
            NameComboBox.Items.Add("По возрастанию имя");
            NameComboBox.Items.Add("По убыванию имя");
            NameComboBox.Items.Add("По умолчанию имя");
        }


        /// <summary>
        /// Логика подгрузки данных при выборе фильтрации
        /// </summary>
        private void LoadComponent(bool Check)
        {
            using (var db = new LanguageDBEntities())
            {
                var AgentsAll = db.Agent.ToList();
                if (Check == false)
                {
                    agents = AgentsAll;
                    DataGridAgent.ItemsSource = agents;
                }
                else
                {
                    agents = AgentsAll;
                    if (!string.IsNullOrEmpty(SearchTextBox.Text))
                    {
                        var currentProductName = dbmodel.Agent.ToList();
                        currentProductName = currentProductName.Where(p => p.EmailAgent.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
                        agents = currentProductName;
                    }
                    if (selectPriority == 0)
                    {
                        agents = agents.OrderBy(items => items.Priotity).ToList();
                        DataGridAgent.ItemsSource = agents;
                    }
                    if (selectPriority == 1)
                    {
                        agents = agents.OrderByDescending(items => items.Priotity).ToList();
                        DataGridAgent.ItemsSource = agents;
                    }
                    if (selectPriority == 2)
                    {                        
                        DataGridAgent.ItemsSource = agents;
                    }                    
                    if (selectName == 0)
                    {
                       agents = agents.OrderBy(items => items.NameAgent).ToList();
                        DataGridAgent.ItemsSource = agents;
                    }

                    if (selectName == 1)
                    {
                        agents = agents.OrderByDescending(items => items.NameAgent).ToList();
                        DataGridAgent.ItemsSource = agents;
                    }
                    }
                    if (selectName == 2)
                    {
                        DataGridAgent.ItemsSource = agents;
                    }
                }
            }        
        

        /// <summary>
        /// Обработчик для ComboBox
        /// </summary>
        private void ComboBox_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            int select = PriorityComboBox.SelectedIndex;
            switch (select)
            {
                case 0:
                    selectPriority = 0;
                    break;
                case 1:
                    selectPriority = 1;
                    break;
                case 2:
                    selectPriority = 2;
                    break;              
                default:
                    selectPriority = 2;
                    break;
            }
            LoadComponent(true);
        }

        /// <summary>
        /// Обработчик для ComboBox
        /// </summary>
        private void ComboBox_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            int select = NameComboBox.SelectedIndex;
            switch (select)
            {
                case 0:
                    selectName = 0;
                    break;
                case 1:
                    selectName = 1;
                    break;
                case 2:
                    selectName = 2;
                    break;
                default:
                    selectName = 2;
                    break;
            }
            LoadComponent(true);
        }

        /// <summary>
        /// Логика обработки посика
        /// </summary>
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentAgentEmail = dbmodel.Agent.ToList();
            currentAgentEmail = currentAgentEmail.Where(p => p.EmailAgent.ToLower().Contains(SearchTextBox.Text.ToLower()) || p.PhoneAgent.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            DataGridAgent.ItemsSource = currentAgentEmail.OrderBy(p => p.EmailAgent).ToList();
            LoadComponent(true);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            bool allright = false;
            Agent item = DataGridAgent.SelectedItem as Agent;
            foreach (Agent agent in dbmodel.Agent)
            {
                if (agent.Id == item.Id)
                {
                    _currentAgent =agent;
                    break;
                }
            }
            StringBuilder errors = new StringBuilder();
            _currentAgent.TypeAgent = TypeAgentTextBox.Text;
            _currentAgent.NameAgent = NameAgentTextBox.Text;
            _currentAgent.EmailAgent = EmailAgentTextBox.Text;
            _currentAgent.PhoneAgent = PhoneAgentTextBox.Text;
            _currentAgent.Address = AddressTextBox.Text;
            _currentAgent.Priotity = Convert.ToInt32(PriotityTextBox.Text);
            _currentAgent.Director = DirectorTextBox.Text;
            _currentAgent.INN = INNTextBox.Text;
            _currentAgent.KPP = KPPTextBox.Text;
            if (AgentPhotoImage.Source != null)
            {
                BitmapImage bitmapImage = AgentPhotoImage.Source as BitmapImage;
                MemoryStream memStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(memStream);
                _currentAgent.Photo = memStream.ToArray();
            }


            if (_currentAgent.TypeAgent == null)
                errors.AppendLine("Введите тип агента" + "\n");
            if (_currentAgent.NameAgent == null)
                errors.Append("Введите имя" + "\n");
            if (_currentAgent.EmailAgent == null)
                errors.Append("Введите почту" + "\n");
            if (_currentAgent.PhoneAgent == null)
                errors.AppendLine("Введите телефон" + "\n");
            if (_currentAgent.Address == null)
                errors.Append("Введите адрес" + "\n");
            if (_currentAgent.Priotity == 0)
                errors.Append("Введите приоритет" + "\n");
            if (_currentAgent.Director == null)
                errors.Append("Выберите диреткора" + "\n");
            if (_currentAgent.INN == null)
                errors.Append("Выберите ИНН" + "\n");
            if (_currentAgent.KPP == null)
                errors.Append("Выберите КПП" + "\n");         
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentAgent.TypeAgent != null && _currentAgent.NameAgent != null && _currentAgent.EmailAgent != null && _currentAgent.PhoneAgent != null
                && _currentAgent.Address != null && _currentAgent.Priotity != null
                && _currentAgent.Director != null && _currentAgent.INN != null && _currentAgent.KPP != null)
                allright = true;
            if (allright == true)
            {
                try
                {
                    dbmodel.SaveChanges();
                    MessageBox.Show("Информация успешно изменена!", "Окно оповещений");
                    DataGridAgent.Items.Refresh();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        /// <summary>
        /// Логика удаления агента
        /// </summary>        
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using(LanguageDBEntities language = new LanguageDBEntities()) { 
            var agentsForDeleting = DataGridAgent.SelectedItems.Cast<Agent>().ToList();

                if (MessageBox.Show($"Вы точно хотите удалить {agentsForDeleting.Count()} следующие", "Внимание!",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var agent in agentsForDeleting)
                    {
                        var productSales = dbmodel.ProductSale.Where(ps => ps.Agent.Id == agent.Id).ToList();

                        if (productSales.Count > 0)
                        {
                            MessageBox.Show($"Агент {agent.NameAgent} не может быть удален, так как он реализует товар.", "Ошибка удаления");
                        }
                        else
                        {
                            var existingAgent = dbmodel.Agent.Find(agent.Id);

                            if (existingAgent != null)
                            {
                                try
                                {
                                    language.Agent.Attach(agent);
                                    language.Agent.Remove(agent);
                                    language.SaveChanges();
                                    MessageBox.Show("Данные удалены!", "Окно оповещений");
                                    DataGridAgent.ItemsSource = language.Agent.ToList();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message.ToString());
                                }                              
                            }
                        }
                    }
                }               
            }
        }



        /// <summary>
        /// Логика добавление агента
        /// </summary>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();          

            if (_currentAgent.TypeAgent == null)
                errors.AppendLine("Введите тип агента" + "\n");
            if (_currentAgent.NameAgent == null)
                errors.Append("Введите имя" + "\n");
            if (_currentAgent.EmailAgent == null)
                errors.Append("Введите почту" + "\n");
            if (_currentAgent.PhoneAgent == null)
                errors.AppendLine("Введите телефон" + "\n");
            if (_currentAgent.Address == null)
                errors.Append("Введите адрес" + "\n");
            if (_currentAgent.Priotity == 0)
                errors.Append("Введите приоритет" + "\n");
            if (_currentAgent.Director == null)
                errors.Append("Выберите диреткора" + "\n");
            if (_currentAgent.INN == null)
                errors.Append("Выберите ИНН" + "\n");
            if (_currentAgent.KPP == null)
                errors.Append("Выберите КПП" + "\n");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }            

            if (_currentAgent.TypeAgent != null && _currentAgent.NameAgent != null && _currentAgent.EmailAgent != null && _currentAgent.PhoneAgent != null
                && _currentAgent.Address != null && _currentAgent.Priotity != null
                && _currentAgent.Director != null && _currentAgent.INN != null && _currentAgent.KPP != null)
            {
                dbmodel.Agent.Add(_currentAgent);
            }

            dbmodel.SaveChanges();
            MessageBox.Show("Информация успешно добавлена!", "Окно оповещений");
            DataGridAgent.ItemsSource = dbmodel.Agent.ToList();
        }

        /// <summary>
        /// Логика выбора картинки из компьютера
        /// </summary>
        private void Img_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                DefaultExt = "*.png;*.png",
                Filter = "файл png или jpg (2.png)|*jpg;*.png",
                Title = "Выберите фото"
            };
            if (!(ofd.ShowDialog() == true))
                return;
            FilePath = ofd.FileName;
            using (FileStream fs = new System.IO.FileStream(FilePath, FileMode.Open))
            {
                imageData = new byte[fs.Length];
                fs.Read(imageData, 0, imageData.Length);

                var bitmap = new BitmapImage();
                MemoryStream ms = new MemoryStream(imageData);
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                AgentPhotoImage.Source = (ImageSource)bitmap;
                _currentAgent.Photo = imageData;
            }
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

        /// <summary>
        /// Обработка на нажатия элемента из списка
        /// </summary>
        private void DataGridAgent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Agent item = DataGridAgent.SelectedItem as Agent;
            if (item != null)
            {
                foreach (Agent agent in dbmodel.Agent)
                {
                    if (agent.Id == item.Id)
                    {
                        _currentAgent = agent;
                        break;
                    }
                }
            }

            UpdateButton.IsEnabled = true;
            TypeAgentTextBox.Text = _currentAgent.TypeAgent;
            NameAgentTextBox.Text = _currentAgent.NameAgent;
            EmailAgentTextBox.Text = _currentAgent.EmailAgent;
            PhoneAgentTextBox.Text = _currentAgent.PhoneAgent;
            AddressTextBox.Text = _currentAgent.Address;
            PriotityTextBox.Text = Convert.ToString(_currentAgent.Priotity);
            DirectorTextBox.Text = _currentAgent.Director;
            INNTextBox.Text = _currentAgent.INN;
            KPPTextBox.Text = _currentAgent.KPP;
            if (_currentAgent.Photo != null)
            {
                var bitmap = new BitmapImage();
                MemoryStream ms = new MemoryStream(_currentAgent.Photo);
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                AgentPhotoImage.Source = (ImageSource)bitmap;
            }        
        }
    }
}
