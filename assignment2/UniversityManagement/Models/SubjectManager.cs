using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityManagement.Models;

public class SubjectManager
{
  public List<Subject> subjectList = [];
  // ------------------Teacher methods-----------------------------



  public void CreateAndAddNewSubject(string name, string description, Guid teacherId)
  {
    Subject newSubject = new(name, description, teacherId);
    subjectList.Add(newSubject);
  }

  public void EditSubjectById(Guid subjectId, string newName, string newDescription)
  {
    var subject = subjectList.FirstOrDefault(subject => subject.Id == subjectId);
    if (subject != null)
    {
      subject.Name = newName;
      subject.Description = newDescription;
    }
  }

  public void DeleteSubjectById(Guid subjectId)
  {
    // delete subject from list
    subjectList.RemoveAll(subject => subject.Id == subjectId);

  }

  public List<Subject> GetCreatedSubjectsByTeacherId(Guid teacherId)
  {
    return subjectList.FindAll(subject => subject.TeacherId != null && subject.TeacherId == teacherId);
  }




  // -------------------Student Methods----------------------------
  public void EnrollStudentInSubjectId(Guid studentId, Guid subjectId)
  {
    var subject = subjectList.FirstOrDefault(subject => subject.Id == subjectId);
    if (subject != null && subject.StudentsEnrolled != null)
    {
      subject.StudentsEnrolled.Add(studentId);
    }
  }

  public void RemoveSubjectFromStudent(Guid studentId, Guid subjectId)
  {
    var subject = subjectList.FirstOrDefault(subject => subject.Id == subjectId);
    if (subject != null && subject.StudentsEnrolled != null)
    {
      subject.StudentsEnrolled.Remove(studentId);
    }
  }

  // Get all of user's subjects by Id
  public List<Subject> GetEnrolledSubjectsByStudentId(Guid studentId)
  {
    return subjectList.FindAll(subject => subject.StudentsEnrolled != null && subject.StudentsEnrolled.Contains(studentId));
  }

}
