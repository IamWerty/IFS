using System.Windows;

namespace Invaders.Client.UI
{
    public partial class MainWindow : Window
    {
        private MainViewModel VM => (MainViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private async void Connect_Click(object sender, RoutedEventArgs e)
        {
            await VM.Connect();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            await VM.Login();
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await VM.RefreshFiles();
        }
    }
}
