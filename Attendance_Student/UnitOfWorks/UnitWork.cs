using Attendance_Student.Models;
using Attendance_Student.Repositories;

namespace Attendance_Student.UnitOfWorks
{
    public class UnitWork
    {
        
        GenericRepository<Class> classRepo;
        GenericRepository<Department> departmentRepo;
        GenericRepository<Subject> subjectRepo;
        GenericRepository<TimeTable>timeTableRepo;

        AttendanceStudentContext db;
        public UnitWork(AttendanceStudentContext db)
        {

            this.db = db;
        }

        public GenericRepository<Class> ClassRepo
        {
            get
            {
                if (classRepo == null)
                {

                    classRepo = new GenericRepository<Class>(db);
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
                     departmentRepo = new GenericRepository<Department> (db);
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

                    subjectRepo = new GenericRepository<Subject>(db);
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

                    timeTableRepo = new GenericRepository<TimeTable>(db);
                }
                return timeTableRepo;
            }
        }
    }
}
