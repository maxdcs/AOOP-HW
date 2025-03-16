namespace UniversityManagement.ViewModels;

using CommunityToolkit.Mvvm.Input;
using UniversityManagement.Views;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    [RelayCommand]
    private void OpenTeacherWindow(){
      new TeacherWindow().Show();
    }
}
