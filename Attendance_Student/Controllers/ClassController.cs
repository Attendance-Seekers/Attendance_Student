using Attendance_Student.DTOs.Class;
using Attendance_Student.Models;
using Attendance_Student.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Attendance_Student.Controllers
{
    [Route("api/classes")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        AttendanceStudentContext db;
        GenericRepository<Class> classRepo;
        public ClassController(AttendanceStudentContext db, GenericRepository<Class> classRepo)
        {
            this.db = db;
            this.classRepo = classRepo;
        }
        [HttpGet]
        [SwaggerOperation
            (
            Summary = "Retrieves all Classes",
            Description = "Fetches a list of all Classes in the school"
            )]
        [SwaggerResponse(200, "Successfully retrieved the list of Classes", typeof(List<ClassDTO>))]
        [SwaggerResponse(404, "No classes found")]
        [Produces("application/json")]

        public IActionResult selectAllClasses()
        {
            //Console.WriteLine("selectALLLLLLLLLLLLLLLLLLLLLL");
            List<Class> classes = classRepo.selectAll();
            List<ClassDTO> classDTO = new List<ClassDTO>();
            if (classes.Count < 0) return NotFound();
            else
            {
                foreach (Class _class in classes)
                {

                    ClassDTO class1 = new ClassDTO()
                    {
                        Class_Id = _class.Class_Id,
                        Class_Name = _class.Class_Name,
                        Class_Size = _class.Class_Size


                    };
                    classDTO.Add(class1);

                }
                return Ok(classDTO);
            }
        }

        [HttpGet("/{id:int}")]
        [SwaggerOperation(
         Summary = "Retrieves a class by ID",
         Description = "Fetches a single class details based on its unique ID"
            )]
        [SwaggerResponse(200, "Successfully retrieved the class", typeof(ClassDTO))]
        [SwaggerResponse(404, "class not found")]
        [Produces("application/json")]


        public IActionResult selectClassById(int id)
        {

            Class _class = classRepo.selectById(id);


            if (_class == null) return NotFound();
            else
            {
                ClassDTO classDTO = new ClassDTO()
                {
                    Class_Id = _class.Class_Id,
                    Class_Name = _class.Class_Name,
                    Class_Size = _class.Class_Size

                };
                return Ok(classDTO);
            }

        }
        [HttpPost]
        [SwaggerOperation(
    Summary = "Creates a new Class",
    Description = "Adds a new Class info to the system. Requires admin privileges."
)] // didn't do the admins yet
        [SwaggerResponse(201, "The Class was created", typeof(ClassDTO))]
        [SwaggerResponse(400, "The Class data is invalid")]
        [Produces("application/json")]
        //[Consumes("application/json")]
        public IActionResult addClass([FromForm] ClassDTO _class)
        {

            if (_class == null)
            {
                //Console.WriteLine( "class is null" );
                return NotFound();
            }
            else
            {

                Class newClass = new Class()
                {
                    Class_Name = _class.Class_Name,
                    Class_Size = _class.Class_Size

                };


                //Console.WriteLine(  "i'm being addded");
                classRepo.add(newClass);


                //Console.WriteLine( "i have been added sucessfully" );
                classRepo.save();
                return CreatedAtAction("selectClassById", new { id = newClass.Class_Id }, _class);
                //return Ok();

            }
        }

    }
}
