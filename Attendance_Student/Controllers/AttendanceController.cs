
using Attendance_Student.DTOs.AttendanceDTOs;
using Attendance_Student.DTOs.ClassDTO;
using Attendance_Student.DTOs.StudentDTO;
using Attendance_Student.Models;
using Attendance_Student.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        //AttendanceStudentContext _context;
        UnitWork unit;
        UserManager<IdentityUser> _userManager;
        IMapper mapper;
        public AttendanceController(UnitWork unit, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            //_context = context;
            this.unit = unit;
            _userManager = userManager;
            this.mapper = mapper;
        }
        //[HttpPost("class/{class_id}")]
        //public async Task<IActionResult> RecordAttendance(int class_id, AttendanceDTO attendanceDTO)
        //{
        //    if (attendanceDTO == null) return BadRequest("Attendance data is required.");

        //    Class _class = await unit.DB.Classes.FindAsync(class_id);
        //    if (_class == null) return BadRequest($"Class with ID {class_id} not found.");

        //    if (_class.timeTable == null)
        //    {
        //        return BadRequest("Class is not associated with any timetable.");
        //    }
        //    var teacher = await unit.Teachers.FindAsync(attendanceDTO.Teacher_id);
        //    if (teacher == null)
        //    {
        //        return BadRequest($"Teacher with ID {attendanceDTO.Teacher_id} not found.");
        //    }
        //    var studentAttendances = mapper.Map<List<StudentAttendance>>(attendanceDTO.Students);
        //    //List<StudentAttendance> studentAttendances = new List<StudentAttendance>();

        //    //Teacher teacher = (Teacher)await _userManager.FindByNameAsync(User.Identity.Name);

        //    //foreach (StudentAttendanceDTO studentAttendanceDTO in attendanceDTO.Students)
        //    //{
        //    //    var st = await unit.Students.FindAsync(studentAttendanceDTO.StudentId);
        //    //    if (st == null)
        //    //        return BadRequest($"Student with ID {studentAttendanceDTO.StudentId} not found.");

        //    //    StudentAttendance student = new StudentAttendance()
        //    //    {
        //    //        Status = studentAttendanceDTO.status,
        //    //        StudentId = studentAttendanceDTO.StudentId,
        //    //        student = st
        //    //    };
        //    //    studentAttendances.Add(student);
        //    //}

        //    var attendance = mapper.Map<Attendance>(attendanceDTO);
        //    //Attendance attendance = _mapper.Map<Attendance>(attendanceDTO);
        //    attendance.TimeTableId = _class.timeTable.TimeTableId;
        //    //attendance.teacher_id = attendanceDTO.Teacher_id;
        //    attendance.StudentsAttendance = studentAttendances;


        //    //Attendance attendance = new Attendance()
        //    //{
        //    //    dateAttendance = attendanceDTO.DateAttendance,
        //    //    Feedback = attendanceDTO.Feedback,
        //    //    TimeTableId = _class.timeTable.TimeTableId,
        //    //    teacher_id = attendanceDTO.Teacher_id,
        //    //    StudentsAttendance = studentAttendances

        //    //};

        //    unit.Attendances.Add(attendance);
        //    await unit.SaveChangesAsync();

        //    //List<SelectStudentDTO> STs = new List<SelectStudentDTO>();
        //    //foreach (var st in attendance.StudentsAttendance)
        //    //{
        //    //    SelectStudentDTO student = new SelectStudentDTO()
        //    //    {
        //    //        username = st.student.UserName,
        //    //        status = st.Status
        //    //    };
        //    //    STs.Add(student);
        //    //}

        //    //SelectAttendanceDTO selectAttendance = new SelectAttendanceDTO()
        //    //{
        //    //    Id = attendance.Id,
        //    //    Feedback = attendance.Feedback,
        //    //    dateAttendance = attendance.dateAttendance,
        //    //    StudentsAttendance = STs,
        //    //    teacher_name = teacher == null ? "" : teacher.Teacher_fullName,
        //    //    subject_name = teacher.Subject == null ? "" : teacher.Subject.subject_Name
        //    //};

        //    SelectAttendanceDTO selectAttendance = mapper.Map<SelectAttendanceDTO>(attendance);
        //    selectAttendance.teacher_name = teacher?.Teacher_fullName ?? "";
        //    selectAttendance.subject_name = teacher?.Subject?.subject_Name ?? "";
        //    return Ok(selectAttendance);
        //}

        [HttpPost("class/{class_id}")]
        public async Task<IActionResult> RecordAttendance(int class_id, AttendanceDTO attendanceDTO)
        {
            if (attendanceDTO == null) return BadRequest("Attendance data is required.");

            Class _class = await unit.DB.Classes
                                          .Include(c => c.timeTable)  
                                          .FirstOrDefaultAsync(c => c.Class_Id == class_id);
            if (_class == null) return BadRequest($"Class with ID {class_id} not found.");

            if (_class.timeTable == null)
            {
                return BadRequest("Class is not associated with any timetable.");
            }

            var teacher = await unit.DB.Teachers
                                        .Include(t => t.Subject) 
                                        .FirstOrDefaultAsync(t => t.Id == attendanceDTO.Teacher_id);
            if (teacher == null)
            {
                return BadRequest($"Teacher with ID {attendanceDTO.Teacher_id} not found.");
            }

            // Mapping student attendances
            var studentAttendances = new List<StudentAttendance>();
            foreach (var studentAttendanceDTO in attendanceDTO.Students)
            {
                var student = await unit.DB.Students
                                            .FirstOrDefaultAsync(s => s.Id == studentAttendanceDTO.StudentId);
                if (student == null)
                {
                    return BadRequest($"Student with ID {studentAttendanceDTO.StudentId} not found.");
                }

                var studentAttendance = new StudentAttendance
                {
                    Status = studentAttendanceDTO.status,
                    StudentId = studentAttendanceDTO.StudentId,
                    student = student 
                };

                studentAttendances.Add(studentAttendance);
            }

            // Mapping Attendance
            var attendance = mapper.Map<Attendance>(attendanceDTO);
            attendance.TimeTableId = _class.timeTable.TimeTableId;
            attendance.StudentsAttendance = studentAttendances;

          
            unit.DB.Attendances.Add(attendance);
            await unit.DB.SaveChangesAsync();

           
            SelectAttendanceDTO selectAttendance = mapper.Map<SelectAttendanceDTO>(attendance);
            selectAttendance.teacher_name = teacher?.Teacher_fullName ?? "";
            selectAttendance.subject_name = teacher?.Subject?.subject_Name ?? "";

            return Ok(selectAttendance);
        }


        [HttpGet("class/{class_id}/date/{date}")]
        public async Task<IActionResult> GetAttendanceDay(int class_id, DateOnly date)
        {
            Class _class = await unit.DB.Classes.FindAsync(class_id);

            if (_class == null) return BadRequest($"Class with ID {class_id} not found.");
            List<Attendance> attendances = await unit.DB.Attendances.Where(a => a.dateAttendance == date && a.timeTable.class_id == class_id).ToListAsync();
            if (!attendances.Any()) return NotFound("No attendance records found for the specified day.");

            var attendanceDTOs = mapper.Map<List<SelectAttendanceDTO>>(attendances);
            //List<SelectAttendanceDTO> attendanceDTOs = new List<SelectAttendanceDTO>();
            //foreach (var attendance in attendances)
            //{
            //    List<SelectStudentDTO> studentDTOs = new List<SelectStudentDTO>();
            //    foreach (var student in attendance.StudentsAttendance)
            //    {
            //        SelectStudentDTO studentDTO = new SelectStudentDTO()
            //        {
            //            id = student.StudentId,
            //            username = student.student.Student_fullname,
            //            status = student.Status
            //        };
            //        studentDTOs.Add(studentDTO);
            //    }
            //    SelectAttendanceDTO attendanceDTO = new SelectAttendanceDTO()
            //    {
            //        Id = attendance.Id,
            //        Feedback = attendance.Feedback,
            //        dateAttendance = attendance.dateAttendance,
            //        teacher_name = attendance.teacher.Teacher_fullName,
            //        subject_name = attendance.teacher.Subject.subject_Name,
            //        StudentsAttendance = studentDTOs
            //    };
            //    attendanceDTOs.Add(attendanceDTO);
            //}
            return Ok(attendanceDTOs);
        }



        [HttpGet("/student/{student_id}")]
        public async Task<IActionResult> GetAttendanceStudent(string student_id)
        {
            Student _student = await unit.DB.Students.FindAsync(student_id);

            if (_student == null) return BadRequest($"Student with ID {student_id} not found.");
            var attendances_Student = await unit.DB.StudentAttendances.Where(s => s.StudentId == student_id).ToListAsync();
            if (!attendances_Student.Any()) return NotFound($"No attendance records found for student ID {student_id}.");

            var attendances_studentDTO = mapper.Map<List<SelectAttendaceStudentDTO>>(attendances_Student);
            //List<SelectAttendaceStudentDTO> attendances_studentDTO = new List<SelectAttendaceStudentDTO>();
            //foreach (var attendanceST in attendances_Student)
            //{
            //    SelectAttendaceStudentDTO selectAttendanceStudent = new SelectAttendaceStudentDTO()
            //    {
            //        Id = attendanceST.AttendanceId,
            //        dateAttendance = attendanceST.attendance.dateAttendance,
            //        teacher_name = attendanceST.attendance.teacher.Teacher_fullName,
            //        subject_name = attendanceST.attendance.teacher.Subject.subject_Name,
            //        status = attendanceST.Status
            //    };
            //    attendances_studentDTO.Add(selectAttendanceStudent);
            //}
            return Ok(attendances_studentDTO);


        }

        [HttpGet("report/class/{class_id}/range/{start_date}/{end_date}")]
        public async Task<IActionResult> GetAttendanceClassReport(int class_id, DateOnly start_date, DateOnly end_date)
        {
            Class _class = await unit.DB.Classes.FindAsync(class_id);

            if (_class == null) return BadRequest($"Class with ID {class_id} not found.");
            List<Attendance> attendances = await unit.DB.Attendances.Where(a => a.dateAttendance >= start_date && a.dateAttendance <= end_date && a.timeTable.class_id == class_id).ToListAsync();
            if (!attendances.Any()) return NotFound("No attendance records found for the specified day.");

            List<SelectAttendanceDTO> attendanceDTOs = mapper.Map<List<SelectAttendanceDTO>>(attendances);

            //List<SelectAttendanceDTO> attendanceDTOs = new List<SelectAttendanceDTO>();
            //foreach (var attendance in attendances)
            //{
            //    List<SelectStudentDTO> studentDTOs = new List<SelectStudentDTO>();
            //    foreach (var student in attendance.StudentsAttendance)
            //    {
            //        SelectStudentDTO studentDTO = new SelectStudentDTO()
            //        {
            //            id = student.StudentId,
            //            username = student.student.Student_fullname,
            //            status = student.Status
            //        };
            //        studentDTOs.Add(studentDTO);
            //    }
            //    SelectAttendanceDTO attendanceDTO = new SelectAttendanceDTO()
            //    {
            //        Id = attendance.Id,
            //        Feedback = attendance.Feedback,
            //        dateAttendance = attendance.dateAttendance,
            //        teacher_name = attendance.teacher.Teacher_fullName,
            //        subject_name = attendance.teacher.Subject.subject_Name,
            //        StudentsAttendance = studentDTOs
            //    };
            //    attendanceDTOs.Add(attendanceDTO);
            //}
            return Ok(attendanceDTOs);
        }



        [HttpGet("report/student/{student_id}/range/{start_date}/{end_date}")]
        public async Task<IActionResult> GetAttendanceStudentReport(string student_id, DateOnly start_date, DateOnly end_date)
        {
            Student _student = await unit.DB.Students.FindAsync(student_id);

            if (_student == null) return BadRequest($"Student with ID {student_id} not found.");
            var attendances_Student = await unit.DB.StudentAttendances.Where(s => s.StudentId == student_id && s.attendance.dateAttendance >= start_date && s.attendance.dateAttendance <= end_date).ToListAsync();
            if (!attendances_Student.Any()) return NotFound($"No attendance records found for student ID {student_id}.");
            //List<SelectAttendaceStudentDTO> attendances_studentDTO = new List<SelectAttendaceStudentDTO>();
            
            var attendances_studentDTO = mapper.Map<List<SelectAttendaceStudentDTO>>(attendances_Student);

            //foreach (var attendanceST in attendances_Student)
            //{
            //    SelectAttendaceStudentDTO selectAttendanceStudent = new SelectAttendaceStudentDTO()
            //    {
            //        Id = attendanceST.AttendanceId,
            //        dateAttendance = attendanceST.attendance.dateAttendance,
            //        teacher_name = attendanceST.attendance.teacher.Teacher_fullName,
            //        subject_name = attendanceST.attendance.teacher.Subject.subject_Name,
            //        status = attendanceST.Status
            //    };
            //    attendances_studentDTO.Add(selectAttendanceStudent);
            //}
            return Ok(attendances_studentDTO);


        }
    } 
}
