using BLL.Interfaces;
using ProjectWPF.ViewModels;
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
