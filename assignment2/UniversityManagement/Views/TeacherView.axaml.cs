using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UniversityManagement.ViewModels;

namespace UniversityManagement.Views
{
  public partial class TeacherView : UserControl
  {
    public TeacherView()
    {
      InitializeComponent();
      // Removed: DataContext = new TeacherWindowViewModel();
      // DataContext now comes from parent binding
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}