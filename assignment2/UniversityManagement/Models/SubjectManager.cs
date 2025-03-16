using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json;


namespace UniversityManagement.Models;

public class SubjectManager
{
  // JSON deserilize

  public List<Subject> subjectList = [];

  // user list
  // public List<User> userList = [];
  // 



  // ------------------Teacher methods-----------------------------



  public void CreateAndAddNewSubject(string name, string description, Guid teacherId)
  {
    // maybe we can leaeve out teacherId and just keep current user state in the manager

    // add item to the list
    // serilize to json
    // write
    Subject newSubject = new(name, description, teacherId);
    subjectList.Add(newSubject);
  }

  public void EditSubjectById(Guid subjectId, string newName, string newDescription)
  {
    // edit item
    // serilize to json
    // write
    var subject = subjectList.FirstOrDefault(subject => subject.Id == subjectId);
    if (subject != null)
    {
      subject.Name = newName;
      subject.Description = newDescription;
    }
  }

  public void DeleteSubjectById(Guid subjectId)
  {
    // authenticate if the current user is truly a teacher

    // delete subject from list
    // serilize to json
    // write
    subjectList.RemoveAll(subject => subject.Id == subjectId);

  }

  public List<Subject> GetCreatedSubjectsByTeacherId(Guid teacherId)
  {
    // maybe we can use a currentID and auth

    return subjectList.FindAll(subject => subject.TeacherId != null && subject.TeacherId == teacherId);
  }




  // -------------------Student Methods----------------------------
  public void EnrollStudentInSubjectId(Guid studentId, Guid subjectId)
  {
    // current ID?

    // add student id to the subject
    // serilize to json
    // write
    var subject = subjectList.FirstOrDefault(subject => subject.Id == subjectId);
    if (subject != null && subject.StudentsEnrolled != null)
    {
      subject.StudentsEnrolled.Add(studentId);
    }
  }

  public void RemoveSubjectFromStudent(Guid studentId, Guid subjectId)
  {
    // remove student id to the subject
    // serilize to json
    // write
    var subject = subjectList.FirstOrDefault(subject => subject.Id == subjectId);
    if (subject != null && subject.StudentsEnrolled != null)
    {
      subject.StudentsEnrolled.Remove(studentId);
    }
  }

  // Get all of user's subjects by Id
  public List<Subject> GetEnrolledSubjectsByStudentId(Guid studentId)
  {
    // maybe we can use current user or whatever
    return subjectList.FindAll(subject => subject.StudentsEnrolled != null && subject.StudentsEnrolled.Contains(studentId));
  }

}
