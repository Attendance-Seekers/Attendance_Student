using Attendance_Student.DTOs.TeacherDTO;
using Attendance_Student.Models;
using Attendance_Student.Repositories;
using Attendance_Student.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Swashbuckle.AspNetCore.Annotations;

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TeacherController : ControllerBase
    {
        //AttendanceStudentContext db;
        //GenericRepository<Teacher> teacherRepo;
        IMapper mapper;
        UnitWork _unit;
        public TeacherController(UnitWork unit, IMapper mapper)
        {
            _unit = unit;
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
            var teachers = _unit.UserReps.GetUsersWithRole("Teacher").Result.OfType<Teacher>().ToList();

            if (!teachers.Any()) return NotFound();
            else
            {

                var teacherDTO = mapper.Map<List<SelectTeacherDTO>>(teachers);

                return Ok(teacherDTO);
            }
        }
        [Authorize (Roles ="Teacher")]
        [HttpGet("{id}")]
        [SwaggerOperation(
         Summary = "Retrieves a Teacher by ID",
         Description = "Fetches a single Teacher details based on its unique ID"
            )]
        [SwaggerResponse(200, "Successfully retrieved the Teacher", typeof(SelectTeacherDTO))]
        [SwaggerResponse(404, "Teacher not found")]
        [Produces("application/json")]


        public async Task<IActionResult> selectTeacherById(string id)
        {

            //Teacher teacher = teacherRepo.selectById(id);
            var teacher = (Teacher) _unit.UserReps.GetUsersWithRole("Teacher").Result.FirstOrDefault(t => t.Id == id);


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


                var createTeacherPass = _unit.UserReps.CreateUser(newTeacher, teacherDTO.password).Result;
                if (createTeacherPass.Succeeded)
                {
                    var addingToTeacherResult = _unit.UserReps.AddRole(newTeacher, "Teacher").Result;
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
                //var teacherName= User.Identity.Name;
                var teacher = _unit.UserReps.GetUserById(teacherDTO.Id).Result;
                if (teacher == null) return NotFound();
                else
                {
                    mapper.Map(teacherDTO, teacher);
                    var updateTeacher = _unit.UserReps.UpdateUser(teacher).Result;
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
        public Task<IActionResult> deleteTeacherById(string id)
        {
            var teacher = _unit.UserReps.GetUserById(id).Result;
            if (teacher == null)
            {
                return Task.FromResult<IActionResult>(NotFound());
            }
            else
            {
                // Remove roles associated with the user
                var roles = _unit.UserReps.GetRoles(teacher).Result;
                foreach (var role in roles)
                {
                    _unit.UserReps.RemoveUserFromRole(teacher, role).Wait();
                }
                var res = _unit.UserReps.RemoveUser(teacher).Result;
                if (res.Succeeded)
                {
                    return Task.FromResult<IActionResult>(Ok());
                }
                else
                { return Task.FromResult<IActionResult>(BadRequest(res.Errors)); }
            }

        }

    }
}

