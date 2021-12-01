using BLL;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ProjectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IFileService _fileService;
        private readonly IFilter _filter;
        private readonly IUserService _userService;

        private int _currentPage = 0;
        private const int RECORDS = 10;

        public MainWindow(IFileService fileService, IFilter filter, IUserService userService)
        {
            InitializeComponent();
            _fileService = fileService;
            _filter = filter;
            _userService = userService;

            UsersGrid.ItemsSource = _userService.GetAllUsers(0, RECORDS);
        }


        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage == 0)
            {
                return;
            }

            UsersGrid.ItemsSource = _userService.GetAllUsers((--_currentPage) * RECORDS, RECORDS);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (RECORDS > _userService.Count)
            {
                return;
            }

            if (_currentPage >= (_userService.Count / RECORDS))
            {
                return;
            }

            UsersGrid.ItemsSource = _userService.GetAllUsers((++_currentPage) * RECORDS, RECORDS);
        }

        private async void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {

            var openFileDialog = new OpenFileDialog
            {
                Filter = "Csv files (*.csv)|*.csv"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var pathFile = openFileDialog.FileName;

                try
                {
                    await _fileService.ImportFromCsvToDatabase(pathFile);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                UsersGrid.ItemsSource = _userService.GetAllUsers(_currentPage, RECORDS);
            }
        }

        private void BtnSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            string lastName = LastName.Text;
            string patronymic = Patronymic.Text;
            string city = City.Text;
            string country = Country.Text;

            DateTime? date = null;

            try
            {
                if (!string.IsNullOrEmpty(Date.Text))
                {
                    date = DateTime.ParseExact(Date.Text, "dd.MM.yyyy", null);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            var users = new List<User>();

            try
            {
                users = _filter.Filtration(date, name, lastName, patronymic, city, country);
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Csv file (*.csv)|*.csv|Xml file (*.xml)|*.xml"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var pathFile = saveFileDialog.FileName;
                var fileFormat = Path.GetExtension(saveFileDialog.FileName);

                try
                {
                    if (fileFormat == ".csv")
                    {
                        _fileService.WriteToCsvFile(pathFile, users);
                    }
                    else if (fileFormat == ".xml")
                    {
                        _fileService.WriteToXmlFile(pathFile, users);
                    }
                    else
                    {
                        throw new IOException("Wrong file format.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }
    }
}
