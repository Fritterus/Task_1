using BLL.Interfaces;
using DAL.Models;
using Microsoft.Win32;
using ProjectWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IFileService fileService, IFilter filter, IUserService userService)
        {
            InitializeComponent();
            DataContext = new MainViewModel(fileService, filter, userService);
        }
    }
}
