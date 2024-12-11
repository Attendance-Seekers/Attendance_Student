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
        UnitWork _unit;

        IMapper mapper; 
        public ClassController(UnitWork unit, IMapper mapper)
        {
            _unit = unit;
            this.mapper = mapper;
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves Paginated Classes", Description = "Fetches paginated list of Classes")]
        [SwaggerResponse(200, "Successfully retrieved the list of Classes")]
        [SwaggerResponse(404, "No classes found")]
        public async Task<IActionResult> selectAllClasses(
       [FromQuery] int page = 1,
       [FromQuery] int pageSize = 10)
        {
            List<Class> classes = await _unit.ClassRepo.selectAll();

            if (classes.Count == 0) return NotFound("No classes found.");

            var totalCount = classes.Count;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var paginatedClasses = classes
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var classDTO = mapper.Map<List<SelectClassesDTO>>(paginatedClasses);

            return Ok(new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Classes = classDTO
            });
        }
        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Retrieves a class by ID", Description = "Fetches a single class details based on its unique ID")]
        [SwaggerResponse(200, "Successfully retrieved the class", typeof(SelectClassDTO))]
        [SwaggerResponse(404, "Class not found")]
        [Produces("application/json")]

        public async Task<IActionResult> selectClassById(int id)
        {

            Class _class = await _unit.ClassRepo.selectById(id);

            if (_class == null) return NotFound("Class not found.");

            var classDTO = mapper.Map<SelectClassDTO>(_class);
            return Ok(classDTO);
            

        }
        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Class",
            Description = "Adds a new Class info to the system. Requires admin privileges.")] // didn't do the admins yet
        [SwaggerResponse(201, "The Class was created")]
        [SwaggerResponse(400, "The Class data is invalid")]

        public async Task<IActionResult> addClass( AddClassDTO _classDTO)
        {

            if (!ModelState.IsValid)
                return NotFound("Invalid data provided.");
            
            Class newClass = mapper.Map<Class>(_classDTO);
            List < Student > students = new List<Student>();

            foreach (var studentId in _classDTO.studentsIDs) 
            {
                var student = (Student)_unit.UserReps.GetUsersWithRole("Student").Result.FirstOrDefault(t => t.Id == studentId);
                students.Add(student);

            }
            newClass.students = students;

              
            await _unit.ClassRepo.add(newClass);
            await _unit.Save();
            return CreatedAtAction("selectClassById", new { id = newClass.Class_Id }, _classDTO);
                
            
        }
        [HttpPut]
        [SwaggerOperation(Summary = "Edit an existing Class", Description = "Updates an existing Class with new details. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Class updated successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Class data.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Class not found.")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> editClass(EditClassDTO _classDTO) 
        {

            if (!ModelState.IsValid) return BadRequest("Invalid data provided.");

            
            var _class = await _unit.ClassRepo.selectById(_classDTO.Class_Id);
            if (_class == null) return NotFound("Class not found.");

            mapper.Map(_classDTO,_class);

            if (_classDTO.flagAddOrOverwrite)  // if true , clear all the students within the current class
            {
                _class.students.Clear();
                List<Student> students = new List<Student>();
                foreach (var studentId in _classDTO.studentsIDs)
                {
                    var student = (Student) _unit.UserReps.GetUsersWithRole("Student").Result.FirstOrDefault(t => t.Id == studentId);
                    if (student != null) students.Add(student);

                }
                _class.students = students;

            }
            else
            {
                        
                foreach (var studentId in _classDTO.studentsIDs)
                {
                    var student = (Student)_unit.UserReps.GetUsersWithRole("Student").Result.FirstOrDefault(t => t.Id == studentId);
                    if (student != null) _class.students.Add(student);

                }
                        
            }
            _unit.ClassRepo.update(_class);
            await _unit.Save();
            return Ok("Class updated successfully.");
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a Class", Description = "Deletes a Class by its ID. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Class deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Class not found.")]
        [Produces("application/json")]
        public async Task<IActionResult> deleteClassById(int id)
        {
            var _class = await _unit.ClassRepo.selectById(id);
            if (_class == null) return NotFound("Class not found.");

            _unit.ClassRepo.remove(_class);
            await _unit.Save();
            return Ok("Class deleted successfully.");
            
        }
    }
}
