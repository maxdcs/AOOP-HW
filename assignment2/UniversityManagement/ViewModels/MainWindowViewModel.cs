namespace UniversityManagement.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniversityManagement.Models;
using System.Linq;
using UniversityManagement.Views;

public partial class MainWindowViewModel : ViewModelBase
{
  public static MainWindowViewModel? Instance { get; private set; }
  
  public string Greeting { get; } = "Welcome to University Management System!";
  
  [ObservableProperty]
  public SubjectManager subjectManager = new();
  
  [ObservableProperty]
  public UserManager userManager = new();
  
  [ObservableProperty]
  public Teacher currentTeacher;
  
  [ObservableProperty]
  public Student currentStudent;
  
  [ObservableProperty]
  public TeacherWindowViewModel teacherViewModel;
  
  [ObservableProperty]
  public StudentWindowViewModel studentViewModel;
  
  public MainWindowViewModel()
  {
    Instance = this;
    
    // Get teachers and students from the loaded data
    var teachers = userManager.GetAllTeachers();
    var students = userManager.GetAllStudents();
    
    // Set current users (you might want to implement a login system later)
    currentTeacher = teachers.FirstOrDefault() ?? new Teacher("Default Teacher", "default", "123");
    currentStudent = students.FirstOrDefault() ?? new Student("Default Student", "default", "123");
    
    // Create and initialize the view models
    TeacherViewModel = new TeacherWindowViewModel();
    StudentViewModel = new StudentWindowViewModel();
  }
}