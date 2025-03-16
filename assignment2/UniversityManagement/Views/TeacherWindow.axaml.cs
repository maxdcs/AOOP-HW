using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UniversityManagement.ViewModels;

namespace UniversityManagement.Views;

public partial class TeacherWindow : Window
{
    public TeacherWindow()
    {
        InitializeComponent();
        DataContext = new TeacherWindowViewModel();
    }
}