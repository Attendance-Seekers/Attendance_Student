using Attendance_Student.DTOs.ClassDTO;
using Attendance_Student.MapperConfig;
using Attendance_Student.Models;
using Attendance_Student.Repositories;
using Attendance_Student.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Attendance_Student.Controllers
{
    [Route("api/classes")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        //AttendanceStudentContext db;
        //GenericRepository<Class> classRepo;
        UnitWork unit;
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        IMapper mapper; 
        public ClassController(UnitWork unit, IMapper mapper, UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.unit = unit;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
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
            List<Class> classes = unit.ClassRepo.selectAll();
           
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

        [HttpGet("{id:int}")]
        [SwaggerOperation(
         Summary = "Retrieves a class by ID",
         Description = "Fetches a single class details based on its unique ID"
            )]
        [SwaggerResponse(200, "Successfully retrieved the class", typeof(SelectClassDTO))]
        [SwaggerResponse(404, "class not found")]
        [Produces("application/json")]


        public IActionResult selectClassById(int id)
        {

            Class _class = unit.ClassRepo.selectById(id);


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
                List < Student > students = new List<Student>();
                foreach (var studentId in _classDTO.studentsIDs) 
                {
                    var student = (Student)userManager.GetUsersInRoleAsync("Student").Result.FirstOrDefault(t => t.Id == studentId);
                    students.Add(student);

                }
                newClass.students = students;

              
                unit.ClassRepo.add(newClass);
                unit.ClassRepo.save();
                return CreatedAtAction("selectClassById", new { id = newClass.Class_Id }, _classDTO);
                
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
                var _class = unit.ClassRepo.selectById(_classDTO.Class_Id);
                if (_class == null) return NotFound();
                else 
                {
                    mapper.Map(_classDTO,_class);

                    if (_classDTO.flagAddOrOverwrite)  // if true , clear all the students within the current class
                    {
                        _class.students.Clear();
                        List<Student> students = new List<Student>();
                        foreach (var studentId in _classDTO.studentsIDs)
                        {
                            var student = (Student)userManager.GetUsersInRoleAsync("Student").Result.FirstOrDefault(t => t.Id == studentId);
                            students.Add(student);

                        }
                        _class.students = students;


                    }
                    else
                    {
                        
                        foreach (var studentId in _classDTO.studentsIDs)
                        {
                            var student = (Student)userManager.GetUsersInRoleAsync("Student").Result.FirstOrDefault(t => t.Id == studentId);
                            _class.students.Add(student);

                        }
                        
                    }
                    unit.ClassRepo.update(_class);
                    unit.ClassRepo.save();
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
            var _class = unit.ClassRepo.selectById(id);
            if (_class == null)
            {
                return NotFound();
            }
            else 
            {
                unit.ClassRepo.remove(_class);
                unit.ClassRepo.save();
                return Ok();
            }

        }
    }
}
