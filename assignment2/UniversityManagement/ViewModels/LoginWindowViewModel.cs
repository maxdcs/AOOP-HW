using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using UniversityManagement.Models;
using UniversityManagement.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace UniversityManagement.ViewModels
{
    public partial class LoginWindowViewModel : ObservableObject
    {
        private readonly UserManager _userManager = new();

        [ObservableProperty]
        private string username = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [RelayCommand]
        private void Login()
        {
            ErrorMessage = string.Empty;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Username and password are required";
                return;
            }

            User? user = _userManager.AuthenticateUser(Username, Password);
            if (user == null)
            {
                ErrorMessage = "Invalid username or password";
                return;
            }

            var mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(user)
            };

            mainWindow.Show();
            
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (desktop.MainWindow is LoginWindow loginWindow)
                {
                    desktop.MainWindow = mainWindow;
                    loginWindow.Close();
                }
            }
        }
    }
}