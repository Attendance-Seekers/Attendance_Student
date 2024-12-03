using Attendance_Student.DTOs.Class;
using Attendance_Student.MapperConfig;
using Attendance_Student.Models;
using Attendance_Student.Repositories;
using AutoMapper;
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
        IMapper mapper; 
        public ClassController(AttendanceStudentContext db, GenericRepository<Class> classRepo, IMapper mapper )
        {
            this.db = db;
            this.classRepo = classRepo;
            this.mapper = mapper;
        }
        [HttpGet]
        [SwaggerOperation
            (
            Summary = "Retrieves all Classes",
            Description = "Fetches a list of all Classes in the school"
            )]
        [SwaggerResponse(200, "Successfully retrieved the list of Classes", typeof(List<SelectClassDTO>))]
        [SwaggerResponse(404, "No classes found")]
        [Produces("application/json")]

        public IActionResult selectAllClasses()
        {
            //Console.WriteLine("selectALLLLLLLLLLLLLLLLLLLLLL");
            List<Class> classes = classRepo.selectAll();
           
            if (classes.Count < 0) return NotFound();
            else
            {

                var classDTO = mapper.Map<List<SelectClassDTO>>(classes);

                //foreach (Class _class in classes)
                //{

                //    ClassDTO class1 = new ClassDTO()
                //    {
                //        Class_Id = _class.Class_Id,
                //        Class_Name = _class.Class_Name,
                //        Class_Size = _class.Class_Size


                //    };
                //    classDTO.Add(class1);

                //}
                return Ok(classDTO);
            }
        }

        [HttpGet("/{id:int}")]
        [SwaggerOperation(
         Summary = "Retrieves a class by ID",
         Description = "Fetches a single class details based on its unique ID"
            )]
        [SwaggerResponse(200, "Successfully retrieved the class", typeof(SelectClassDTO))]
        [SwaggerResponse(404, "class not found")]
        [Produces("application/json")]


        public IActionResult selectClassById(int id)
        {

            Class _class = classRepo.selectById(id);


            if (_class == null) return NotFound();
            else
            {
                var classDTO = mapper.Map<SelectClassDTO>(_class);
                //ClassDTO classDTO = new ClassDTO()
                //{
                //    Class_Id = _class.Class_Id,
                //    Class_Name = _class.Class_Name,
                //    Class_Size = _class.Class_Size

                //};
                return Ok(classDTO);
            }

        }
        [HttpPost]
        [SwaggerOperation(
    Summary = "Creates a new Class",
    Description = "Adds a new Class info to the system. Requires admin privileges.")] // didn't do the admins yet
        [SwaggerResponse(201, "The Class was created")]
        [SwaggerResponse(400, "The Class data is invalid")]
        //[Produces("application/json")]
        //[Consumes("application/json")]
        public IActionResult addClass( AddClassDTO _classDTO)
        {

            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            else
            {

                Class newClass = mapper.Map<Class>(_classDTO);


                //Console.WriteLine(  "i'm being addded");
                classRepo.add(newClass);


                //Console.WriteLine( "i have been added sucessfully" );
                classRepo.save();
                return CreatedAtAction("selectClassById", new { id = newClass.Class_Id }, _classDTO);
                //return Ok();

            }
        }
        [HttpPut]
        [SwaggerOperation(Summary = "Edit an existing Class", Description = "Updates an existing Class with new details. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Class updated successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Class data.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Class not found.")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult editClass(EditClassDTO _classDTO) 
        {

            if (ModelState.IsValid) 
            {
                var _class = classRepo.selectById(_classDTO.Class_Id);
                if (_class == null) return NotFound();
                else 
                {
                    mapper.Map(_classDTO,_class);
                    classRepo.update(_class);
                    classRepo.save();
                    return Ok();
                }
            }
            else { return BadRequest(); }

        
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a Class", Description = "Deletes a Class by its ID. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Class deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Class not found.")]
        [Produces("application/json")]
        public IActionResult deleteClassById(int id)
        {
            var _class = classRepo.selectById(id);
            if (_class == null)
            {
                return NotFound();
            }
            else 
            {
                classRepo.remove(_class);
                return Ok();
            }

        }
    }
}
