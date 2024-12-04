using Attendance_Student.DTOs.TeacherDTO;
using Attendance_Student.Models;
using Attendance_Student.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        //AttendanceStudentContext db;
        //GenericRepository<Teacher> teacherRepo;
        IMapper mapper;
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
            public TeacherController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
            {
                this.roleManager = roleManager;
                this.userManager = userManager;
                this.mapper = mapper;
            }
            [HttpGet]
            [SwaggerOperation
                (
                Summary = "Retrieves all Teachers",
                Description = "Fetches a list of all Teachers in the school"
                )]
            [SwaggerResponse(200, "Successfully retrieved the list of Teachers", typeof(List<SelectTeacherDTO>))]
            [SwaggerResponse(404, "No classes found")]
            [Produces("application/json")]

            public IActionResult selectAllTeachers()
            {
            //Console.WriteLine("selectALLLLLLLLLLLLLLLLLLLLLL");
            //List<Teacher> Teachers = teacherRepo.selectAll();
            var teachers = userManager.GetUsersInRoleAsync("Teacher").Result.OfType<Teacher>().ToList();

                if (!teachers.Any()) return NotFound();
                else
                {

                    var teacherDTO = mapper.Map<List<SelectTeacherDTO>>(teachers);

                    return Ok(teacherDTO);
                }
            }

            [HttpGet("{id}")]
            [SwaggerOperation(
             Summary = "Retrieves a Teacher by ID",
             Description = "Fetches a single Teacher details based on its unique ID"
                )]
            [SwaggerResponse(200, "Successfully retrieved the Teacher", typeof(SelectTeacherDTO))]
            [SwaggerResponse(404, "Teacher not found")]
            [Produces("application/json")]


            public IActionResult selectTeacherById(string id)
            {

            //Teacher teacher = teacherRepo.selectById(id);
            var teacher = (Teacher)userManager.GetUsersInRoleAsync("Teacher").Result.FirstOrDefault(t => t.Id == id);


                if (teacher == null) return NotFound();
                else
                {
                    var teacherDTO = mapper.Map<SelectTeacherDTO>(teacher);

                    return Ok(teacherDTO);
                }

            }
            [HttpPost]
            [SwaggerOperation(
        Summary = "Creates a new Teacher",
        Description = "Adds a new Teacher info to the system. Requires admin privileges.")] // didn't do the admins yet
            [SwaggerResponse(201, "The Teacher was created", typeof(SelectTeacherDTO))]
            [SwaggerResponse(400, "The Teacher data is invalid")]
            //[Produces("application/json")]
            //[Consumes("application/json")]
            public IActionResult addTeacher([FromForm] AddTeacherDTO teacherDTO)
            {

                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                else
                {

                    Teacher newTeacher = mapper.Map<Teacher>(teacherDTO);


                var createTeacherPass = userManager.CreateAsync(newTeacher, teacherDTO.password).Result;
                if (createTeacherPass.Succeeded) 
                {
                    var addingToTeacherResult = userManager.AddToRoleAsync(newTeacher, "Teacher").Result;
                    if (addingToTeacherResult.Succeeded) { return Ok(); }
                    else { return BadRequest(addingToTeacherResult.Errors); }
                
                }
                else { return BadRequest(createTeacherPass.Errors); }
                    
                }
            }
            [HttpPut]
            [SwaggerOperation(Summary = "Edit an existing Teacher", Description = "Updates an existing Teacher with new details. Requires admin privileges.")]
            [SwaggerResponse(StatusCodes.Status200OK, "Teacher updated successfully.")]
            [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Teacher data.")]
            [SwaggerResponse(StatusCodes.Status404NotFound, "Teacher not found.")]
            //[Produces("application/json")]
            //[Consumes("application/json")]
            public IActionResult editTeacher(EditTeacherDTO teacherDTO)
            {

                if (ModelState.IsValid)
                {
                    var teacherName= User.Identity.Name;
                    var teacher = userManager.FindByNameAsync(teacherName).Result;
                if (teacher == null) return NotFound();
                else
                {
                    mapper.Map(teacherDTO, teacher);
                    var updateTeacher = userManager.UpdateAsync(teacher).Result;
                    if (updateTeacher.Succeeded) { return Ok(); }
                    else { return BadRequest(updateTeacher.Errors); }

                    }
                }
                else { return BadRequest(ModelState); }


            }


            [HttpDelete("{id}")]
            [SwaggerOperation(Summary = "Delete a Teacher", Description = "Deletes a Teacher by its ID. Requires admin privileges.")]
            [SwaggerResponse(StatusCodes.Status200OK, "Teacher deleted successfully.")]
            [SwaggerResponse(StatusCodes.Status404NotFound, "Teacher not found.")]
            [Produces("application/json")]
            public IActionResult deleteTeacherById(string id)
            {
                var teacher = userManager.FindByIdAsync(id).Result;
                if (teacher == null)
                {
                    return NotFound();
                }
                else
                {
                    userManager.DeleteAsync(teacher);
                    return Ok();
                }

            }
        }
}

