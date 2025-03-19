using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UniversityManagement.ViewModels;

namespace UniversityManagement.Views
{
  public partial class StudentWindow : Window
  {
    public StudentWindow()
    {
      InitializeComponent();
      DataContext = new StudentWindowViewModel();
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}