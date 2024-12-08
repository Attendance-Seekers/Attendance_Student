using Attendance_Student.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Student.Repositories
{
    public class GenericRepository<genericEntity> where genericEntity : class
    {
        AttendanceStudentContext _context;

        public GenericRepository(AttendanceStudentContext context)
        {
                _context = context; 
        }

        public async Task<List<genericEntity>> selectAll()
        {
            return await _context.Set<genericEntity>().ToListAsync();
        }

        public async Task<genericEntity> selectById(int id)
        {
            return await _context.Set<genericEntity>().FindAsync(id);
        }
        public async Task<genericEntity> selectUserById(string id)
        {
            return await _context.Set<genericEntity>().FindAsync(id);
        }

        public async Task add(genericEntity entity)
        {
           await _context.Set<genericEntity>().AddAsync(entity);
            
        }

        public void update(genericEntity entity)
        {

             _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
             
        }
        public void remove(genericEntity obj)
        {

            //var obj = db.Set<genericEntity>().Find(id);
            _context.Set<genericEntity>().Remove(obj);

        }

        public virtual void save()
        {

            _context.SaveChanges();
        }
    }
}
