namespace UniversityManagement.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniversityManagement.Models;
using UniversityManagement.Views;

public partial class MainWindowViewModel : ViewModelBase
{
  public static MainWindowViewModel? Instance { get; private set; }

  public string Greeting { get; } = "Welcome to University Management System!";

  [ObservableProperty]
  public SubjectManager subjectManager = new();

  [ObservableProperty]
  public Teacher currentTeacher = new Teacher("Jane Doe", "janedoe123", "123");

  [ObservableProperty]
  public Student currentStudent = new Student("John Smith", "johnsmith", "123");
  
  // ViewModels for the tabs
  [ObservableProperty]
  public TeacherWindowViewModel teacherViewModel;
  
  [ObservableProperty]
  public StudentWindowViewModel studentViewModel;

  public MainWindowViewModel()
  {
    Instance = this;
    
    // Create and initialize the view models
    TeacherViewModel = new TeacherWindowViewModel();
    StudentViewModel = new StudentWindowViewModel();
  }
}