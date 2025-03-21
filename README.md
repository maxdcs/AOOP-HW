## This is the second assignment for the Advanced Object Oriented Programming class.

Nedas Bartkus and Matej Tribula developed the backend, Maksym Drzyzgiewicz developed the frontend

# University Management System

A comprehensive application for managing university subjects with separate interfaces for students and teachers.

## Overview

This application is developed using Avalonia UI and .NET, implementing an MVVM architecture. It provides:

- User authentication (login) with role-based access
- Two separate interfaces for students and teachers
- Subject management capabilities for teachers (creation, deletion, editing)
- Subject enrollment functionality for students (enrollment, dropping)
- Data persistence using JSON files

## Running the Application

1. Clone the repository
2. Open the solution in Visual Studio or your preferred IDE
3. Build and run the application, for VSCode, the user needs to go to the `assignment2/` folder, open the terminal and run the command:

```
dotnet run
```

4. Login with provided credentials (Also provided in the login page for easy reference):
   - Teacher: username: `janedoe123`, password: `123`
   - Student: username: `johnsmith`, password: `123`
# Testing
## Automated Unit Testing

Automated tests can be run using by navigating to the `assignment2/` folder and running the command:
```
dotnet test
```
### UserManager Tests

- **Test_GetAllTeachers_ReturnsTeachers**  
  Verifies that the `GetAllTeachers()` method returns only users with a "Teacher" role.

- **Test_GetAllStudents_ReturnsStudents**  
  Verifies that the `GetAllStudents()` method returns only users with a "Student" role.

- **Test_AuthenticateUser**  
  Tests user authentication with:
  - Valid teacher credentials (e.g., Username: `janedoe123`, Password: `123`)
  - Valid student credentials (e.g., Username: `johnsmith`, Password: `123`)
  - Invalid credentials and empty inputs.

- **Test_GetTeacherById_ReturnsCorrectTeacher**  
  Checks that given a teacher's ID, the correct teacher object is returned.

- **Test_GetStudentById_ReturnsCorrectStudent**  
  Checks that given a student's ID, the correct student object is returned.

### SubjectManager Tests

- **Test_CreateAndAddNewSubject_AddsSubject**  
  Validates that a new subject is created and added to the subject list when invoking `CreateAndAddNewSubject`.

- **Test_EditSubjectById_UpdatesSubject**  
  Verifies that the subject data is updated correctly via the `EditSubjectById` method.

- **Test_DeleteSubjectById_RemovesSubject**  
  Ensures that a subject is removed from the list when `DeleteSubjectById` is called.

- **Test_EnrollStudentInSubjectId_AddsStudentToSubject**  
  Confirms that enrolling a student in a subject (using `EnrollStudentInSubjectId`) correctly adds the student's ID to the subject's enrolled list.

- **Test_RemoveSubjectFromStudent_RemovesStudentFromSubject**  
  Checks that a student is removed from the subject’s enrollment list when `RemoveSubjectFromStudent` is called.

- **Test_GetCreatedSubjectsByTeacherId_ReturnsTeacherSubjects**  
  Validates that `GetCreatedSubjectsByTeacherId` returns only the subjects created by a specific teacher.

- **Test_GetEnrolledSubjectsByStudentId_ReturnsEnrolledSubjects**  
  Verifies that `GetEnrolledSubjectsByStudentId` returns the correct subjects in which a student is enrolled.

## Functional Testing

### Authentication System

- **Login Test**: The system successfully validates credentials and directs users to the appropriate interface based on the role of the specified user
  - Teachers see only the Teacher Dashboard
  - Students see only the Student Dashboard

### Student Functionality

- **Enrollment Test**: Students can successfully enroll in available subjects.
  - Test: Click "Enroll" button on an available subject
  - Verification: Subject moves from the Available Subjects list to Enrolled Subjects
  - Result: A green confirmation notification appears confirming enrollment

- **Drop Subject Test**: Students can drop subjects they've previously enrolled in.
  - Test: Click "Drop" button on an enrolled subject
  - Verification: Subject moves from the Enrolled Subjects list to Available Subjects
  - Result: A red confirmation notification appears confirming the drop

- **View Subject Details**: Students can view detailed information about enrolled subjects.
  - Test: Click "View Details" button on an enrolled subject
  - Verification: Subject details panel appears with name, description, and teacher information

- **Search Functionality**: Students can search for subjects by name.
  - Test: Enter text in the search bar
  - Verification: Both Available and Enrolled subjects lists filter in real-time

### Teacher Functionality

- **Create Subject Test**: Teachers can create new subjects.
  - Test: Fill in name and description fields and click "Add Subject"
  - Verification: New subject appears in the "Your Subjects" list
  - Result: A green confirmation notification appears confirming creation

- **Edit Subject Test**: Teachers can edit their created subjects.
  - Test: Click "Edit" on a subject, modify details, and save
  - Verification: Subject information updates accordingly

- **Delete Subject Test**: Teachers can delete subjects they've created.
  - Test: Click "Delete" button on a created subject
  - Verification: Subject is removed from the list
  - Result: A red confirmation notification appears confirming deletion

- **Search Functionality**: Teachers can search through their created subjects.
  - Test: Enter text in the search bar
  - Verification: Subject list filters in real-time

### System-Level Features

- **Login Test**: Login validation works for both Student and Teacher roles.
  - Test: Run the app, login with wrong data, or correct data, for both provided accounts
  - Verification: Wrong credentials cause an unauthorized error, correct ones progress into either the appropriate Student or Teacher view.

- **Data Persistence**: All changes are properly saved to JSON files.
  - Test: Make changes, close and reopen application
  - Verification: All changes persist between application sessions


