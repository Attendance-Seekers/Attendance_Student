using Attendance_Student.Models;
using Attendance_Student.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Attendance_Student.UnitOfWorks
{
    public class UnitWork
    {
        //DataBase
        AttendanceStudentContext _context;
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        SignInManager<IdentityUser> _signInManager;

        GenericRepository<Class> classRepo;
        GenericRepository<Department> departmentRepo;
        GenericRepository<Subject> subjectRepo;
        GenericRepository<TimeTable>timeTableRepo;
        GenericRepository<Attendance> attendanceRepo; 
        GenericRepository<Student> studentRepo;
        GenericRepository<Teacher> teacherRepo;
        GenericRepository<Parent> parentRepo;
        GenericRepository<Admin> adminRepo;
        NotificationFuncRepository notificationFuncRepo;
        UserRepository userReps;
        AttendanceFuncRepository attendanceRepository;

        public UnitWork(AttendanceStudentContext context , UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {

            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;     
        }
        public AttendanceStudentContext Context
        {
            get
            {
                if (_context == null)
                {

                    _context = new AttendanceStudentContext();
                }
                return _context;
            }
        }
        public GenericRepository<Admin> AdminReop
        {
            get
            {
                if(adminRepo == null)
                    adminRepo = new GenericRepository<Admin>(_context);
                return adminRepo;
            }
        }
        public GenericRepository<Parent> ParentRepo
        {
            get
            {
                if (parentRepo == null)
                    parentRepo = new GenericRepository<Parent>(_context);
                return parentRepo;
            }
        }
        public NotificationFuncRepository NotificationFuncRepository
        {
            get
            {
                if (notificationFuncRepo == null)
                    notificationFuncRepo = new NotificationFuncRepository(_context);
                return notificationFuncRepo;
            }
        }
        public AttendanceFuncRepository AttendanceRepository
        {
            get
            {
                if(attendanceRepository == null)
                    attendanceRepository = new AttendanceFuncRepository(_context);
                return attendanceRepository;
            }
        }
        public GenericRepository<Teacher> TeacherRepo
        {
            get
            {
                if(teacherRepo == null)
                    teacherRepo = new GenericRepository<Teacher>(_context);
                return teacherRepo;
            }
        }
        public GenericRepository<Student> StudentRepo
        {
            get
            {
                if (studentRepo == null)
                    studentRepo = new GenericRepository<Student>(_context);
                return studentRepo;
            }
        }
        public GenericRepository<Attendance> AttendanceRepo
        {
            get
            {
                if(attendanceRepo == null)
                    attendanceRepo = new GenericRepository<Attendance>(_context);
                return attendanceRepo;
            }
        }
        public UserRepository UserReps
        {
            get
            {
                if (userReps == null)
                    userReps = new UserRepository(_userManager, _signInManager , _roleManager);
                return userReps;
            }
        }

        public GenericRepository<Class> ClassRepo
        {
            get
            {
                if (classRepo == null)
                {

                    classRepo = new GenericRepository<Class>(_context);
                }
                return classRepo;
            }
        }
        public GenericRepository<Department> DepartmentRepo
        {
            get
            {
                if (departmentRepo == null)
                {
                     departmentRepo = new GenericRepository<Department> (_context);
                }
                return departmentRepo;
            }
        }
        public GenericRepository<Subject> SubjectRepo
        {
            get
            {
                if (subjectRepo == null)
                {

                    subjectRepo = new GenericRepository<Subject>(_context);
                }
                return subjectRepo;
            }
        }
        public GenericRepository<TimeTable> TimeTableRepo
        {
            get
            {
                if (timeTableRepo == null)
                {

                    timeTableRepo = new GenericRepository<TimeTable>(_context);
                }
                return timeTableRepo;
            }
        }
        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
