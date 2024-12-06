using Attendance_Student.DTOs.DepartmentDTO;
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
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        
            //AttendanceStudentContext db;
            //GenericRepository<Department> DepartmentRepo;
            //GenericRepository<Subject> subjectRepo;
            UnitWork unit;

            UserManager<IdentityUser> userManager;
            RoleManager<IdentityRole> roleManager;
            IMapper mapper;
            public DepartmentController(UnitWork unit, IMapper mapper, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                this.unit = unit;
                this.mapper = mapper;
                this.userManager = userManager;
                this.roleManager = roleManager;
               
            }
        [HttpGet]
        [SwaggerOperation
            (
            Summary = "Retrieves all Departmentes",
            Description = "Fetches a list of all Departmentes in the school"
            )]
        [SwaggerResponse(200, "Successfully retrieved the list of Departmentes", typeof(List<SelectDepartmentDTO>))]
        [SwaggerResponse(404, "No Departmentes found")]
        [Produces("application/json")]

        public IActionResult selectAllDepartmentes()
        {
            List<Department> Departmentes = unit.DepartmentRepo.selectAll();

            if (Departmentes.Count < 0) return NotFound();
            else
            {

                var DepartmentDTO = mapper.Map<List<SelectDepartmentDTO>>(Departmentes);


                return Ok(DepartmentDTO);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
         Summary = "Retrieves a Department by ID",
         Description = "Fetches a single Department details based on its unique ID"
            )]
        [SwaggerResponse(200, "Successfully retrieved the Department", typeof(SelectDepartmentDTO))]
        [SwaggerResponse(404, "Department not found")]
        [Produces("application/json")]


        public IActionResult selectDepartmentById(int id)
        {

            Department _Department = unit.DepartmentRepo.selectById(id);


            if (_Department == null) return NotFound();
            else
            {
                var DepartmentDTO = mapper.Map<SelectDepartmentDTO>(_Department);

                return Ok(DepartmentDTO);
            }

        }
        [HttpPost]
        [SwaggerOperation(
    Summary = "Creates a new Department",
    Description = "Adds a new Department info to the system. Requires admin privileges.")] // didn't do the admins yet
        [SwaggerResponse(201, "The Department was created")]
        [SwaggerResponse(400, "The Department data is invalid")]
        //[Produces("application/json")]
        //[Consumes("application/json")]
        public IActionResult addDepartment(AddDepartmentDTO _DepartmentDTO)
        {

            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            else
            {

                Department newDepartment = mapper.Map<Department>(_DepartmentDTO);
                List<Teacher> teachers = new List<Teacher>();
                foreach (var teacherId in _DepartmentDTO.TeachersIDs)
                {
                    var teacher = (Teacher)userManager.GetUsersInRoleAsync("Teacher").Result.FirstOrDefault(t => t.Id == teacherId);
                    teachers.Add(teacher);

                }

                List<Subject> subjects = new List<Subject>();
                foreach (var subjectId in _DepartmentDTO.SubjectsIDs)
                {
                    var subject = unit.SubjectRepo.selectById(subjectId);

                    subjects.Add(subject);

                }

                newDepartment.Teachers = teachers;
                newDepartment.Subjects = subjects;


                unit.DepartmentRepo.add(newDepartment);
                unit.DepartmentRepo.save();
                return CreatedAtAction("selectDepartmentById", new { id = newDepartment.Id }, _DepartmentDTO);

            }
        }
        [HttpPut]
        [SwaggerOperation(Summary = "Edit an existing Department", Description = "Updates an existing Department with new details. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Department updated successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Department data.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Department not found.")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult editDepartment(EditDepartmentDTO _DepartmentDTO)
        {

            if (ModelState.IsValid)
            {
                var _Department = unit.DepartmentRepo.selectById(_DepartmentDTO.Id);
                if (_Department == null) return NotFound();
                else
                {
                    mapper.Map(_DepartmentDTO, _Department);

                    if (_DepartmentDTO.flagAddOrOverwrite)  // if true , clear all the students within the current Department
                    {
                        _Department.Subjects.Clear();
                        _Department.Teachers.Clear();
                        List<Teacher> teachers = new List<Teacher>();
                        foreach (var teacherId in _DepartmentDTO.TeachersIDs)
                        {
                            var teacher = (Teacher)userManager.GetUsersInRoleAsync("Teacher").Result.FirstOrDefault(t => t.Id == teacherId);
                            teachers.Add(teacher);

                        }

                        List<Subject> subjects = new List<Subject>();
                        foreach (var subjectId in _DepartmentDTO.SubjectsIDs)
                        {
                            var subject = unit.SubjectRepo.selectById(subjectId);

                            subjects.Add(subject);

                        }
                        _Department.Subjects = subjects;
                        _Department.Teachers = teachers;


                    }
                    else
                    {

                        foreach (var teacherId in _DepartmentDTO.TeachersIDs)
                        {
                            var teacher = (Teacher)userManager.GetUsersInRoleAsync("Teacher").Result.FirstOrDefault(t => t.Id == teacherId);
                            _Department.Teachers.Add(teacher);

                        }


                        foreach (var subjectId in _DepartmentDTO.SubjectsIDs)
                        {
                            var subject = unit.SubjectRepo.selectById(subjectId);

                            _Department.Subjects.Add(subject);

                        }

                    }
                    unit.DepartmentRepo.update(_Department);
                     unit.DepartmentRepo.save();
                    return Ok();
                }
            }
            else { return BadRequest(); }


        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a Department", Description = "Deletes a Department by its ID. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Department deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Department not found.")]
        [Produces("application/json")]
        public IActionResult deleteDepartmentById(int id)
        {
            var _Department = unit.DepartmentRepo.selectById(id);
            if (_Department == null)
            {
                return NotFound();
            }
            else
            {
                unit.DepartmentRepo.remove(_Department);
                  unit.DepartmentRepo.save();
                return Ok();
            }

        }
    }
}

