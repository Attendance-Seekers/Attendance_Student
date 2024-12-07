using Attendance_Student.DTOs.StudentDTO;
using Attendance_Student.Models;
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
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        public StudentController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
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
            var Students = userManager.GetUsersInRoleAsync("Student").Result.OfType<Student>().ToList();

            if (!Students.Any()) return NotFound();
            else
            {

                var StudentDTO = mapper.Map<List<SelectStudentDTO>>(Students);

                return Ok(StudentDTO);
            }
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

            //Student Student = StudentRepo.selectById(id);
            var Student = (Student)userManager.GetUsersInRoleAsync("Student").Result.FirstOrDefault(t => t.Id == id);


            if (Student == null) return NotFound();
            else
            {
                var StudentDTO = mapper.Map<SelectStudentDTO>(Student);

                return Ok(StudentDTO);
            }

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
            {
                return NotFound();
            }
            else
            {

                Student newStudent = mapper.Map<Student>(StudentDTO);


                var createStudentPass = userManager.CreateAsync(newStudent, StudentDTO.password).Result;
                if (createStudentPass.Succeeded)
                {
                    var addingToStudentResult = userManager.AddToRoleAsync(newStudent, "Student").Result;
                    if (addingToStudentResult.Succeeded) { return Ok(); }
                    else { return BadRequest(addingToStudentResult.Errors); }

                }
                else { return BadRequest(createStudentPass.Errors); }

            }
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

            if (ModelState.IsValid)
            {
                //var StudentName= User.Identity.Name;
                var Student = userManager.FindByIdAsync(StudentDTO.Id).Result;
                if (Student == null) return NotFound();
                else
                {
                    mapper.Map(StudentDTO, Student);
                    var updateStudent = userManager.UpdateAsync(Student).Result;
                    if (updateStudent.Succeeded) { return Ok(); }
                    else { return BadRequest(updateStudent.Errors); }

                }
            }
            else { return BadRequest(ModelState); }


        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a Student", Description = "Deletes a Student by its ID. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Student deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Student not found.")]
        [Produces("application/json")]
        public IActionResult deleteStudentById(string id)
        {
            var Student = userManager.FindByIdAsync(id).Result;
            if (Student == null)
            {
                return NotFound();
            }
            else
            {
                // Remove roles associated with the user
                var roles = userManager.GetRolesAsync(Student).Result;
                foreach (var role in roles)
                {
                    userManager.RemoveFromRoleAsync(Student, role).Wait();
                }
                var res = userManager.DeleteAsync(Student).Result;
                if (res.Succeeded)
                {
                    return Ok();
                }
                else
                { return BadRequest(res.Errors); }
            }

        }
    }
}

