namespace UniversityManagement.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniversityManagement.Models;
using UniversityManagement.Views;

public partial class MainWindowViewModel : ViewModelBase
{
  public static MainWindowViewModel? Instance { get; private set; }

  public string Greeting { get; } = "Welcome to Avalonia!";

  [ObservableProperty]
  public SubjectManager subjectManager = new();

  [ObservableProperty]
  public Teacher currentTeacher = new Teacher("Jane Doe", "janedoe123", "123");

  public MainWindowViewModel()
  {
    Instance = this;
  }

  [RelayCommand]
  private void OpenTeacherWindow()
  {
    new TeacherWindow().Show();
  }
}
