using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UniversityManagement.Models;

namespace UniversityManagement.ViewModels
{
  public partial class TeacherWindowViewModel : ObservableObject
  {
    public static TeacherWindowViewModel? Current { get; private set; }

    private SubjectManager? sharedSubjectManager;

    [ObservableProperty]
    private Teacher? templateTeacher;

    [ObservableProperty]
    private ObservableCollection<Subject> subjectList = new();

    [ObservableProperty]
    private string newSubjectName = "";

    [ObservableProperty]
    private string newSubjectDescription = "";

    [ObservableProperty]
    private Subject? subjectBeingEdited;

    [ObservableProperty]
    private string editSubjectName = "";

    [ObservableProperty]
    private string editSubjectDescription = "";

    [ObservableProperty]
    private bool isEditingSubject = false;
    
    // New properties for confirmation messages
    [ObservableProperty]
    private string addSubjectMessage = string.Empty;

    [ObservableProperty]
    private bool showAddSubjectMessage = false;

    [ObservableProperty]
    private string deleteSubjectMessage = string.Empty;

    [ObservableProperty]
    private bool showDeleteSubjectMessage = false;

    public TeacherWindowViewModel()
    {
      Current = this;
      sharedSubjectManager = MainWindowViewModel.Instance?.SubjectManager;
      TemplateTeacher = MainWindowViewModel.Instance?.CurrentTeacher;

      // Initialize the subject list from the shared manager
      if (sharedSubjectManager != null && TemplateTeacher != null)
      {
        LoadSubjects();
      }
    }

    private void LoadSubjects()
    {
      var teacherSubjects = sharedSubjectManager?.GetCreatedSubjectsByTeacherId(TemplateTeacher?.Id ?? Guid.Empty) ?? new List<Subject>();
      SubjectList = new ObservableCollection<Subject>(teacherSubjects);
    }

    [RelayCommand]
    private void AddSubject()
    {
      if (string.IsNullOrWhiteSpace(NewSubjectName) ||
          sharedSubjectManager == null ||
          TemplateTeacher == null)
      {
        return;
      }

      sharedSubjectManager.CreateAndAddNewSubject(
        NewSubjectName,
        NewSubjectDescription,
        TemplateTeacher.Id
      );

      // Set confirmation message
      AddSubjectMessage = $"Successfully created subject: {NewSubjectName}!";
      ShowAddSubjectMessage = true;

      // Hide message after 3 seconds
      Task.Delay(3000).ContinueWith(_ => ShowAddSubjectMessage = false);

      // Refresh the subject list
      LoadSubjects();

      // Clear the input fields
      NewSubjectName = "";
      NewSubjectDescription = "";
    }

    [RelayCommand]
    private void DeleteSubject(Subject subject)
    {
      if (subject == null || sharedSubjectManager == null)
        return;

      string subjectName = subject.Name ?? "Unknown subject";
      
      sharedSubjectManager.DeleteSubjectById(subject.Id);

      // Set confirmation message
      DeleteSubjectMessage = $"Successfully deleted subject: {subjectName}!";
      ShowDeleteSubjectMessage = true;

      // Hide message after 3 seconds
      Task.Delay(3000).ContinueWith(_ => ShowDeleteSubjectMessage = false);

      // Refresh the subject list
      LoadSubjects();
    }

    [RelayCommand]
    private void EditSubject(Subject subject)
    {
      if (subject == null)
        return;

      SubjectBeingEdited = subject;
      EditSubjectName = subject.Name ?? "";
      EditSubjectDescription = subject.Description ?? "";
      IsEditingSubject = true;
    }

    [RelayCommand]
    private void SaveSubjectEdit()
    {
      if (SubjectBeingEdited == null ||
          string.IsNullOrWhiteSpace(EditSubjectName) ||
          sharedSubjectManager == null)
      {
        return;
      }

      sharedSubjectManager.EditSubjectById(
        SubjectBeingEdited.Id,
        EditSubjectName,
        EditSubjectDescription
      );

      // Refresh and reset
      LoadSubjects();
      CancelEdit();
    }

    [RelayCommand]
    private void CancelEdit()
    {
      SubjectBeingEdited = null;
      EditSubjectName = "";
      EditSubjectDescription = "";
      IsEditingSubject = false;
    }
  }
}