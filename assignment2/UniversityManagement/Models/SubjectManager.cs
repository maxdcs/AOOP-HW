using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace UniversityManagement.Models
{
  public class SubjectManager
  {
    private const string SubjectsFilePath = "Data/subjects.json";
    public List<Subject> subjectList = [];

    public SubjectManager()
    {
      LoadSubjectsFromFile();
    }

    private void LoadSubjectsFromFile()
    {
      try
      {
        if (File.Exists(SubjectsFilePath))
        {
          string jsonString = File.ReadAllText(SubjectsFilePath);
          var subjects = JsonSerializer.Deserialize<List<Subject>>(jsonString);
          if (subjects != null)
          {
            subjectList = subjects;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error loading subjects: {ex.Message}");
      }
    }

    private void SaveSubjectsToFile()
    {
      try
      {
        string jsonString = JsonSerializer.Serialize(subjectList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SubjectsFilePath, jsonString);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error saving subjects: {ex.Message}");
      }
    }

    public void CreateAndAddNewSubject(string name, string description, Guid teacherId)
    {
      Subject newSubject = new(name, description, teacherId);
      subjectList.Add(newSubject);
      SaveSubjectsToFile();
    }

    public void EditSubjectById(Guid subjectId, string newName, string newDescription)
    {
      var subject = subjectList.FirstOrDefault(subject => subject.Id == subjectId);
      if (subject != null)
      {
        subject.Name = newName;
        subject.Description = newDescription;
        SaveSubjectsToFile();
      }
    }

    public void DeleteSubjectById(Guid subjectId)
    {
      subjectList.RemoveAll(subject => subject.Id == subjectId);
      SaveSubjectsToFile();
    }

    public void EnrollStudentInSubjectId(Guid studentId, Guid subjectId)
    {
      var subject = subjectList.FirstOrDefault(subject => subject.Id == subjectId);
      if (subject != null && subject.StudentsEnrolled != null)
      {
        subject.StudentsEnrolled.Add(studentId);
        SaveSubjectsToFile();
      }
    }

    public void RemoveSubjectFromStudent(Guid studentId, Guid subjectId)
    {
      var subject = subjectList.FirstOrDefault(subject => subject.Id == subjectId);
      if (subject != null && subject.StudentsEnrolled != null)
      {
        subject.StudentsEnrolled.Remove(studentId);
        SaveSubjectsToFile();
      }
    }

    public List<Subject> GetCreatedSubjectsByTeacherId(Guid teacherId)
    {
      return subjectList.FindAll(subject => subject.TeacherId == teacherId);
    }

    public List<Subject> GetEnrolledSubjectsByStudentId(Guid studentId)
    {
      return subjectList.FindAll(subject => subject.StudentsEnrolled != null &&
                                           subject.StudentsEnrolled.Contains(studentId));
    }
  }
}