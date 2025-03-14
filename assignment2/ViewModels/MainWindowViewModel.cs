using assignment2.Models;

namespace assignment2.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";


    Student firstStudent = new Student("matej", "matr24", "123", "Student");
    
}
