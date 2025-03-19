namespace UniversityManagement.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniversityManagement.Models;
using System.Linq;
using UniversityManagement.Views;

public partial class MainWindowViewModel : ViewModelBase
{
    public static MainWindowViewModel? Instance { get; private set; }
  
    public string Greeting { get; } = "Hello!";
  
    [ObservableProperty]
    public SubjectManager subjectManager = new();
  
    [ObservableProperty]
    public UserManager userManager = new();
  
    [ObservableProperty]
    public Teacher? currentTeacher;
  
    [ObservableProperty]
    public Student? currentStudent;
  
    [ObservableProperty]
    public TeacherWindowViewModel? teacherViewModel;
  
    [ObservableProperty]
    public StudentWindowViewModel? studentViewModel;

    [ObservableProperty]
    private bool isStudentRole;

    [ObservableProperty]
    private bool isTeacherRole;
  
    public MainWindowViewModel()
    {
        Instance = this;
        InitWithDefaultUser();
    }

    public MainWindowViewModel(User authenticatedUser)
    {
        Instance = this;
        InitWithAuthenticatedUser(authenticatedUser);
    }

    private void InitWithDefaultUser()
    {
        var teachers = UserManager.GetAllTeachers();
        var students = UserManager.GetAllStudents();
        
        CurrentTeacher = teachers.FirstOrDefault() ?? new Teacher("Default Teacher", "default", "123");
        CurrentStudent = students.FirstOrDefault() ?? new Student("Default Student", "default", "123");
        
        TeacherViewModel = new TeacherWindowViewModel();
        StudentViewModel = new StudentWindowViewModel();

        // Show both roles by default
        IsTeacherRole = true;
        IsStudentRole = true;
    }

    private void InitWithAuthenticatedUser(User user)
    {
        if (user is Teacher teacher)
        {
            CurrentTeacher = teacher;
            TeacherViewModel = new TeacherWindowViewModel();
            IsTeacherRole = true;
            IsStudentRole = false;
        }
        else if (user is Student student)
        {
            CurrentStudent = student;
            StudentViewModel = new StudentWindowViewModel();
            IsStudentRole = true;
            IsTeacherRole = false;
        }
    }
}