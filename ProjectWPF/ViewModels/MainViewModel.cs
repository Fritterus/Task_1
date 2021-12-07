using BLL.Interfaces;
using BLL.ModelsDTO;
using Microsoft.Win32;
using ProjectWPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ProjectWPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IFileService _fileService;
        private readonly IFilter _filter;
        private readonly IUserService _userService;

        private int _currentPage = 0;
        private const int RECORDS = 10;

        private ObservableCollection<UserDTO> _users;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public string City { get; set; } = "";
        public string Country { get; set; } = "";
        public DateTime? Date { get; set; } = null;

        public ObservableCollection<UserDTO> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged("Users");
            }
        }

        public MainViewModel(IFileService fileService, IFilter filter, IUserService userService)
        {
            _fileService = fileService;
            _filter = filter;
            _userService = userService;

            Users = new ObservableCollection<UserDTO>(_userService.GetAllUsers(0, RECORDS));
        }

        public RelayCommand ShowPreviousPage => new(obj =>
        {
            if (_currentPage == 0)
            {
                return;
            }

            Users = new ObservableCollection<UserDTO>(_userService.GetAllUsers((--_currentPage) * RECORDS, RECORDS));
        });

        public RelayCommand ShowNextPage => new(obj =>
        {
            if (RECORDS > _userService.Count)
            {
                return;
            }

            if (_currentPage >= (_userService.Count / RECORDS))
            {
                return;
            }

            Users = new ObservableCollection<UserDTO>(_userService.GetAllUsers((++_currentPage) * RECORDS, RECORDS));
        });

        public RelayCommand OpenFile => new(obj =>
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
                    _fileService.ImportFromCsvToDatabase(pathFile);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                Users = new ObservableCollection<UserDTO>(_userService.GetAllUsers(_currentPage, RECORDS));
            }
        });

        public RelayCommand SaveToFile => new(obj =>
        {
            var users = new List<UserDTO>();

            try
            {
                users = _filter.Filtration(Date, Name, LastName, Patronymic, City, Country);
            }
            catch (ArgumentException ex)
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
        });

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
