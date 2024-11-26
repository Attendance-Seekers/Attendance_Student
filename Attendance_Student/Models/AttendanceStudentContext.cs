using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Student.Models
{
    public class AttendanceStudentContext:IdentityDbContext
    {
        public AttendanceStudentContext(DbContextOptions<AttendanceStudentContext> options):base(options)
        {
                
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Department> Departments { get; set; } 
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TimeTable> TimeTables { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Computer Science" },
                new Department { Id = 2, Name = "Mechanical Engineering" },
                new Department { Id = 3, Name = "Electrical Engineering" }
            );

            // Subjects
            modelBuilder.Entity<Subject>().HasData(
                new Subject { subject_Id = 1, subject_Name = "Mathematics", subject_Duration = 60 },
                new Subject { subject_Id = 2, subject_Name = "Physics", subject_Duration = 60 },
                new Subject { subject_Id = 3, subject_Name = "Chemistry", subject_Duration = 60 }
            );

            // Teachers
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, Name = "John Smith", Address = "123 Street, City", SubjectId = 1, DeptId = 1 },
                new Teacher { Id = 2, Name = "Alice Johnson", Address = "456 Avenue, City", SubjectId = 2, DeptId = 2 },
                new Teacher { Id = 3, Name = "Robert Brown", Address = "789 Boulevard, City", SubjectId = 3, DeptId = 3 }
            );

            // Classes
            modelBuilder.Entity<Class>().HasData(
                new Class { Class_Id = 1, Class_Name = "Class A", Class_Size = 30 },
                new Class { Class_Id = 2, Class_Name = "Class B", Class_Size = 25 },
                new Class { Class_Id = 3, Class_Name = "Class C", Class_Size = 20 }
            );

            // TimeTables
            modelBuilder.Entity<TimeTable>().HasData(
                new TimeTable
                {
                    timeTable_Id = 1,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow
                },
                new TimeTable
                {
                    timeTable_Id = 2,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow
                },
                new TimeTable
                {
                    timeTable_Id = 3,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow
                }
            );

            // Students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = "1", UserName = "student1", Status = "Active", Class_Id = 1 },
                new Student {Id = "2", UserName = "student2", Status = "Active", Class_Id = 2 },
                new Student {Id = "3", UserName = "student3", Status = "Inactive", Class_Id = 3 }
            );
        }


    }
}
