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
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<DaySchedule> DaySchedules { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<StudentAttendance> StudentAttendances { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<StudentAttendance>().HasKey(s => new { s.StudentId, s.AttendanceId });
            //modelBuilder.Entity<TeacherAttendance>().HasKey(t => new { t.TeacherId, t.AttendanceId });
            modelBuilder.Entity<SubjectDaySchedule>().HasKey(d => new { d.SubjectId, d.DayScheduleId });
            // Disable cascading delete for all relationships
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

        }


    }
}
