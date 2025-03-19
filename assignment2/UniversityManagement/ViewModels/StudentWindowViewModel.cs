using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using UniversityManagement.Models;

namespace UniversityManagement.ViewModels
{
  public partial class StudentWindowViewModel : ObservableObject
  {
    public static StudentWindowViewModel? Current { get; private set; }

    private SubjectManager? sharedSubjectManager;
    private List<Subject> allAvailableSubjects = new();
    private List<Subject> allEnrolledSubjects = new();

    [ObservableProperty]
    private Student? templateStudent;

    [ObservableProperty]
    private ObservableCollection<Subject> availableSubjects = new();

    [ObservableProperty]
    private ObservableCollection<Subject> enrolledSubjects = new();

    [ObservableProperty]
    private Subject? selectedSubject;

    [ObservableProperty]
    private bool isViewingDetails = false;

    [ObservableProperty]
    private string enrollmentMessage = string.Empty;

    [ObservableProperty]
    private bool showEnrollmentMessage = false;

    [ObservableProperty]
    private string dropMessage = string.Empty;

    [ObservableProperty]
    private bool showDropMessage = false;
    
    // Search functionality
    [ObservableProperty]
    private string searchText = string.Empty;

    public StudentWindowViewModel()
    {
      Current = this;
      sharedSubjectManager = MainWindowViewModel.Instance?.SubjectManager;
      TemplateStudent = MainWindowViewModel.Instance?.CurrentStudent;

      if (sharedSubjectManager != null && TemplateStudent != null)
      {
        LoadSubjects();
      }
    }

    private void LoadSubjects()
    {
      if (sharedSubjectManager == null || TemplateStudent == null)
        return;

      allEnrolledSubjects = sharedSubjectManager.GetEnrolledSubjectsByStudentId(TemplateStudent.Id);
      
      var enrolledIds = allEnrolledSubjects.Select(s => s.Id).ToHashSet();
      allAvailableSubjects = sharedSubjectManager.subjectList
                    .Where(s => !enrolledIds.Contains(s.Id))
                    .ToList();

      ApplyFilter();
    }
    
    partial void OnSearchTextChanged(string value)
    {
      ApplyFilter();
    }
    
    private void ApplyFilter()
    {
      if (string.IsNullOrWhiteSpace(SearchText))
      {
        AvailableSubjects = new ObservableCollection<Subject>(allAvailableSubjects);
        EnrolledSubjects = new ObservableCollection<Subject>(allEnrolledSubjects);
        return;
      }
      
      var filteredAvailable = allAvailableSubjects
          .Where(s => s.Name?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true)
          .ToList();
      AvailableSubjects = new ObservableCollection<Subject>(filteredAvailable);
      
      var filteredEnrolled = allEnrolledSubjects
          .Where(s => s.Name?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true)
          .ToList();
      EnrolledSubjects = new ObservableCollection<Subject>(filteredEnrolled);
    }

    [RelayCommand]
    private void EnrollInSubject(Subject subject)
    {
      if (subject == null || sharedSubjectManager == null || TemplateStudent == null)
        return;

      sharedSubjectManager.EnrollStudentInSubjectId(TemplateStudent.Id, subject.Id);
    
      EnrollmentMessage = $"Successfully enrolled in {subject.Name}!";
      ShowEnrollmentMessage = true;
    
      Task.Delay(3000).ContinueWith(_ => ShowEnrollmentMessage = false);

      LoadSubjects();
    }

    [RelayCommand]
    private void DropSubject(Subject subject)
    {
      if (subject == null || sharedSubjectManager == null || TemplateStudent == null)
        return;

      sharedSubjectManager.RemoveSubjectFromStudent(TemplateStudent.Id, subject.Id);

      DropMessage = $"Successfully dropped {subject.Name}!";
      ShowDropMessage = true;

      Task.Delay(3000).ContinueWith(_ => ShowDropMessage = false);

      LoadSubjects();
    }

    [RelayCommand]
    private void ViewSubjectDetails(Subject subject)
    {
      SelectedSubject = subject;
      IsViewingDetails = true;
    }

    [RelayCommand]
    private void CloseDetails()
    {
      IsViewingDetails = false;
    }
  }
}