# Attendance Seeker

## Overview

Attendance Seeker is a robust and efficient application designed to simplify and enhance attendance management systems for educational institutions. It provides administrators with tools to manage attendance seamlessly, leveraging a secure and scalable backend infrastructure. This project was developed as part of a .NET backend development bootcamp organized by **Career180** and **Learn IT Academy**, in collaboration with **Plan International**.

---

## Project Description

The **Student Attendance API** provides backend services to manage student attendance in a school or educational institution. The system allows teachers and administrators to record attendance, track student absences, generate attendance reports, and send notifications to students and parents regarding attendance issues.

---

## Key Features

- **User Authentication and Authorization**
  - Secure authentication using **JWT Identity**.
  - Role-based access control for administrators, teachers, students, and parents.
- **Attendance Management**
  - Track and manage attendance records efficiently.
  - Generate detailed attendance reports for classes and individual students.
- **Notification System**
  - Send alerts to students and parents regarding absences or patterns of poor attendance.
- **API Endpoints**
  - RESTful API with endpoints for managing students, classes, attendance, teachers, and notifications.
- **Data Handling**
  - Pagination for smooth and efficient data loading.
  - DTO (Data Transfer Object) implementation to streamline data communication.
- **Clean Architecture**
  - Implemented **Repository Pattern** and **Unit of Work Pattern** for maintainable and scalable architecture.
- **Object Mapping**
  - Used **AutoMapper** for efficient object-to-object mapping, reducing boilerplate code.
- **API Documentation**
  - Integrated **Swagger** for comprehensive API documentation, including request/response examples.
- **Database Management**
  - Relational database management using **SQL Server**.
- **Error Handling**
  - Standard HTTP status codes and clear error responses for failed operations.

---

## Technology Stack

### Backend

- **Programming Language**: C#
- **Framework**: .NET Core
- **Database**: SQL Server
- **ORM**: Entity Framework
- **API Architecture**: RESTful Web API
- **Security**: JWT Identity
- **Object Mapping**: AutoMapper
- **Design Patterns**: Repository Pattern, Unit of Work
- **API Documentation**: Swagger

### Frontend

- **Platform**: Windows Forms Application

---

## API Overview

### Purpose
To facilitate the management and tracking of student attendance across various classes and dates.

### Core Functionality

#### Entities:
- **Students**: Record individual attendance for students in each class or session.
- **Classes**: Manage different classes and associated students.
- **Teachers**: Manage attendance records for their classes.
- **Attendance Records**: Track whether students are present or absent on a specific date.
- **Notifications**: Send attendance-related alerts to students and parents (for absences or patterns of absenteeism).

#### Endpoints:
**Students**:
- `POST /students/register`: Register a new student.
- `GET /students/{id}`: Retrieve student details.
- `GET /students/{id}/attendance`: Retrieve attendance record for a specific student.

**Classes**:
- `POST /classes`: Create a new class.
- `GET /classes`: List all classes.
- `GET /classes/{id}`: Retrieve details of a specific class.

**Attendance**:
- `POST /attendance/class/{class_id}`: Record attendance for a class session.
- `GET /attendance/class/{class_id}/date/{date}`: Retrieve attendance for a specific class on a specific date.
- `GET /attendance/student/{student_id}`: Retrieve all attendance records for a specific student.

**Teachers**:
- `GET /teachers/{teacher_id}/classes`: List all classes taught by the teacher.
- `POST /teachers/{teacher_id}/attendance/class/{class_id}`: Record attendance for a class taught by the teacher.

**Notifications**:
- `POST /notifications/send`: Send notifications about student absences or poor attendance patterns.
- `GET /notifications`: Retrieve notifications for the logged-in user.

**Reports**:
- `GET /attendance/report/class/{class_id}/range/{start_date}/{end_date}`: Generate an attendance report for a class.
- `GET /attendance/report/student/{student_id}/range/{start_date}/{end_date}`: Generate an attendance report for a student.

### Example Data
**Attendance Record**:
```json
{
  "date": "2024-11-15",
  "student_id": "stu123",
  "status": "Present"
}
```

**Class Object**:
```json
{
  "id": "class101",
  "name": "Math 101",
  "teacher_id": "teacher123",
  "students": ["stu123", "stu124", "stu125"]
}
```

### Error Handling
- **200 OK**: Successful requests.
- **201 Created**: Resource created successfully.
- **400 Bad Request**: Validation errors.
- **401 Unauthorized**: Authentication failure.
- **403 Forbidden**: Unauthorized resource access.
- **404 Not Found**: Missing resources.

**Example Error Response**:
```json
{
  "error": "Attendance not found",
  "message": "The attendance record for the specified class and date does not exist."
}
```

---

## Development Methodology

The project was developed following **Agile methodologies**, with continuous iterations and team collaboration. **GitHub** was used for version control to ensure smooth collaboration among the team members.

---

## Team Members

- **Ahmed Salah**
- **Yasmin Gamal Ali** 
- **Mohamed Salah**
- **Omar Sayed**
- **Ahmed Elsily**


---

## Learning Outcomes

This project provided the team with hands-on experience in:

- Developing scalable backend systems using .NET Core.
- Implementing secure authentication mechanisms.
- Documenting APIs with Swagger.
- Collaborating effectively in a team using GitHub.
- Applying Agile methodologies to manage and deliver a project efficiently.

---

## How to Run the Application

### Prerequisites

1. **Software**:
   - Visual Studio
   - SQL Server
   - .NET Core SDK
2. **Libraries**:
   - Entity Framework
   - AutoMapper
   - Swagger
   - JWT Authentication

### Steps

1. Clone the repository from GitHub.
2. Configure the connection string in the application settings to connect to your SQL Server instance.
3. Run database migrations to set up the database schema.
4. Launch the backend API.
5. Open and run the Windows Forms application.

---

## Future Enhancements

- Integration with mobile platforms for real-time attendance tracking.
- Adding analytics dashboards for attendance insights.
- Enhancing scalability to support larger institutions.

---

## Contact

For more details, feel free to reach out to Us via LinkedIn 
- https://www.linkedin.com/in/ahmedsalah5
- https://www.linkedin.com/in/yasmin-gamal-ali-3353a6232?utm_source=share&utm_campaign=share_via&utm_content=profile&utm_medium=android_app
- https://www.linkedin.com/in/mohamedsalah2001/
- https://www.linkedin.com/in/omarsayeddev
- https://eg.linkedin.com/in/ahmedelesily99
  
---

**#dotnet #backenddevelopment #AttendanceSeeker #EntityFramework #SQLServer #RESTfulAPI #AutoMapper #Swagger**

