using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UniversityManagement.ViewModels;

namespace UniversityManagement.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginWindowViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}