using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using System.Collections.ObjectModel;
using UniversityManagement.Models;

namespace UniversityManagement.ViewModels
{
  public partial class StudentWindowViewModel : ObservableObject
  {
    public static StudentWindowViewModel? Current { get; private set; }

    private SubjectManager? sharedSubjectManager;

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



    public StudentWindowViewModel()
    {
      Current = this;
      sharedSubjectManager = MainWindowViewModel.Instance?.SubjectManager;
      TemplateStudent = MainWindowViewModel.Instance?.CurrentStudent;

      // Initialize the subject lists
      if (sharedSubjectManager != null && TemplateStudent != null)
      {
        LoadSubjects();
      }
    }

    private void LoadSubjects()
    {
      if (sharedSubjectManager == null || TemplateStudent == null)
        return;

      // Get enrolled subjects
      var studentEnrolled = sharedSubjectManager.GetEnrolledSubjectsByStudentId(TemplateStudent.Id);
      EnrolledSubjects = new ObservableCollection<Subject>(studentEnrolled);

      // Get all subjects and filter out the ones already enrolled in
      var enrolledIds = studentEnrolled.Select(s => s.Id).ToHashSet();
      var available = sharedSubjectManager.subjectList
                      .Where(s => !enrolledIds.Contains(s.Id))
                      .ToList();

      AvailableSubjects = new ObservableCollection<Subject>(available);
    }

    [RelayCommand]
    private void EnrollInSubject(Subject subject)
    {
      if (subject == null || sharedSubjectManager == null || TemplateStudent == null)
        return;

      sharedSubjectManager.EnrollStudentInSubjectId(TemplateStudent.Id, subject.Id);

      // Set confirmation message
      EnrollmentMessage = $"Successfully enrolled in {subject.Name}!";
      ShowEnrollmentMessage = true;

      // Hide message when refreshing subjects or enrolling in another course
      Task.Delay(3000).ContinueWith(_ => ShowEnrollmentMessage = false);

      // Refresh both lists
      LoadSubjects();
    }

    [RelayCommand]
    private void DropSubject(Subject subject)
    {
      if (subject == null || sharedSubjectManager == null || TemplateStudent == null)
        return;

      sharedSubjectManager.RemoveSubjectFromStudent(TemplateStudent.Id, subject.Id);

      // Set confirmation message
      DropMessage = $"Successfully dropped {subject.Name}!";
      ShowDropMessage = true;

      // Hide message after 3 seconds
      Task.Delay(3000).ContinueWith(_ => ShowDropMessage = false);

      // Refresh both lists
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