using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UniversityManagement.ViewModels;

namespace UniversityManagement.Views
{
  public partial class StudentView : UserControl
  {
    public StudentView()
    {
      InitializeComponent();
      // Removed: DataContext = new StudentWindowViewModel();
      // DataContext now comes from parent binding
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}