using Attendance_Student.DTOs.StudentDTO;
using Attendance_Student.Models;
using Attendance_Student.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IMapper mapper;
        UnitWork _unit;
        public StudentController(UnitWork unit, IMapper mapper)
        {
            _unit = unit;
            this.mapper = mapper;
        }
        [HttpGet]
        [SwaggerOperation
            (
            Summary = "Retrieves all Students",
            Description = "Fetches a list of all Students in the school"
            )]
        [SwaggerResponse(200, "Successfully retrieved the list of Students", typeof(List<SelectStudentDTO>))]
        [SwaggerResponse(404, "No classes found")]
        [Produces("application/json")]

        public IActionResult selectAllStudents()
        {
            var Students = _unit.UserReps.GetUsersWithRole("Student").Result.OfType<Student>().ToList();

            if (!Students.Any()) return NotFound("There are not Students");

            var StudentDTO = mapper.Map<List<SelectStudentDTO>>(Students);

            return Ok(StudentDTO);
            
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
         Summary = "Retrieves a Student by ID",
         Description = "Fetches a single Student details based on its unique ID"
            )]
        [SwaggerResponse(200, "Successfully retrieved the Student", typeof(SelectStudentDTO))]
        [SwaggerResponse(404, "Student not found")]
        [Produces("application/json")]


        public IActionResult selectStudentById(string id)
        {

            var Student = (Student)_unit.UserReps.GetUsersWithRole("Student").Result.FirstOrDefault(t => t.Id == id);


            if (Student == null) return NotFound($"No Student found with the id: {id}.");
 
            var StudentDTO = mapper.Map<SelectStudentDTO>(Student);

            return Ok(StudentDTO);
            

        }
        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Student",
            Description = "Adds a new Student info to the system. Requires admin privileges.")] // didn't do the admins yet
        [SwaggerResponse(201, "The Student was created", typeof(SelectStudentDTO))]
        [SwaggerResponse(400, "The Student data is invalid")]
        //[Produces("application/json")]
        //[Consumes("application/json")]
        public IActionResult addStudent([FromForm] AddStudentDTO StudentDTO)
        {

            if (!ModelState.IsValid)
                return NotFound("The data provided is not valid. Please check the input values.");        

            Student newStudent = mapper.Map<Student>(StudentDTO);

            var createStudentPass = _unit.UserReps.CreateUser(newStudent, StudentDTO.password).Result;
            if (!createStudentPass.Succeeded) return BadRequest(createStudentPass.Errors);
            
            var addingToStudentResult = _unit.UserReps.AddRole(newStudent, "Student").Result;
            if (addingToStudentResult.Succeeded)  return Ok("The Student was created"); 
            else { return BadRequest(addingToStudentResult.Errors); }

                


            
        }
        [HttpPut]
        [SwaggerOperation(Summary = "Edit an existing Student", Description = "Updates an existing Student with new details. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Student updated successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Student data.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Student not found.")]
        //[Produces("application/json")]
        //[Consumes("application/json")]
        public IActionResult editStudent(EditStudentDTO StudentDTO)
        {

            if (ModelState.IsValid) return BadRequest(ModelState);
            
            var Student = _unit.UserReps.GetUserById(StudentDTO.Id).Result;
            if (Student == null) return NotFound($"No Student found with the id: {StudentDTO.Id}.");
            mapper.Map(StudentDTO, Student);
            var updateStudent = _unit.UserReps.UpdateUser(Student).Result;
            if (updateStudent.Succeeded) { return Ok(); }
            else { return BadRequest(updateStudent.Errors); }

        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a Student", Description = "Deletes a Student by its ID. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Student deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Student not found.")]
        [Produces("application/json")]
        public IActionResult deleteStudentById(string id)
        {
            var Student = _unit.UserReps.GetUserById(id).Result;
            if (Student == null)
                return NotFound($"No Student found with the id: {id}.");

            // Remove roles associated with the user
            var roles = _unit.UserReps.GetRoles(Student).Result;
            foreach (var role in roles)
            {
                _unit.UserReps.RemoveUserFromRole(Student, role).Wait();
            }
            var res = _unit.UserReps.RemoveUser(Student).Result;
            if (!res.Succeeded) return BadRequest(res.Errors);
            
            return Ok("Student deleted successfully with her roles.");

        }
    }
}

