using Attendance_Student.Models;

namespace Attendance_Student.Repositories
{
    public class GenericRepository<genericEntity> where genericEntity : class
    {
        protected AttendanceStudentContext db;

        public GenericRepository(AttendanceStudentContext db)
        {
                this.db = db;
        }

        public virtual List<genericEntity> selectAll()
        {
            return db.Set<genericEntity>().ToList();
        }

        public virtual genericEntity selectById(int id)
        {
            return db.Set<genericEntity>().Find(id);
        }

        public virtual void add(genericEntity entity)
        {
            db.Set<genericEntity>().Add(entity);
            
        }

        public virtual void update(genericEntity entity)
        {

            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
             
        }
        public virtual void remove(genericEntity obj)
        {

            //var obj = db.Set<genericEntity>().Find(id);
            db.Set<genericEntity>().Remove(obj);

        }

        public virtual void save()
        {

            db.SaveChanges();
        }
    }
}
