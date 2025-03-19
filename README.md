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

4. Login with provided credentials:
   - Teacher: username: `janedoe123`, password: `123`
   - Student: username: `johnsmith`, password: `123`

## Features & Functional Testing

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

- **Data Persistence**: All changes are properly saved to JSON files.
  - Test: Make changes, close and reopen application
  - Verification: All changes persist between application sessions

## Testing the Application

Automated tests can be run using the test project included in the solution. The tests cover:
- Constructor tests for all main classes
- Subject management operations
- Student enrollment operations
- Teacher subject creation/editing/deletion
