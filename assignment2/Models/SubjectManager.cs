using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Models;

public class SubjectManager
{
  public List<Subject> subjectList = [];
  // ------------------Teacher methods-----------------------------

  public List<Subject> GetCreatedSubjectsByTeacherId(Guid teacherId)
  {
    return subjectList.FindAll(subject => subject.teacherId != null && subject.teacherId == teacherId);
  }
  
  public void CreateNewSubject(string name, string description, Guid teacherId)
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

  // -------------------Student Methods----------------------------
  public void EnrollStudentInNewSubjectId(Guid studentId, Guid subjectId)
  {
    var subject = subjectList.FirstOrDefault(subject => subject.Id == subjectId);
    if (subject != null && subject.studentsEnrolled != null)
    {
      subject.studentsEnrolled.Add(studentId);
    }
  }

  public void RemoveSubjectFromStudentsRepertoir(Guid studentId, Guid subjectId)
  {
    var subject = subjectList.FirstOrDefault(subject => subject.Id == subjectId);
    if (subject != null && subject.studentsEnrolled != null)
    {
      subject.studentsEnrolled.Remove(studentId);
    }
  }

  // Get all of user's subjects by Id
  public List<Subject> GetEnrolledSubjectsByStudentId(Guid studentId)
  {
    return subjectList.FindAll(subject => subject.studentsEnrolled != null && subject.studentsEnrolled.Contains(studentId));

  }

}
