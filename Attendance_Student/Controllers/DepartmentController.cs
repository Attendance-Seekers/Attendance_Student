using Attendance_Student.DTOs.ClassDTO;
using Attendance_Student.DTOs.DepartmentDTO;
using Attendance_Student.DTOs;
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
        
            UnitWork _unit;

            IMapper mapper;
            public DepartmentController(UnitWork unit, IMapper mapper)
            {
                _unit = unit;
                this.mapper = mapper;
               
            }
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all Departments with pagination", Description = "Fetches a paginated list of all Departments in the school")]
        [SwaggerResponse(200, "Successfully retrieved the paginated list of Departments", typeof(PaginatedResponse<SelectDepartmentDTO>))]
        [SwaggerResponse(404, "No Departments found")]
        [Produces("application/json")]
        public async Task<IActionResult> SelectAllDepartments([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            
            List<Department> departments = await _unit.DepartmentRepo.selectAll();

          
            if (departments == null || departments.Count == 0)
            {
                return NotFound("No Departments found.");
            }

           
            int totalCount = departments.Count;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

           
            var paginatedDepartments = departments
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

           
            var departmentDTOs = mapper.Map<List<SelectDepartmentDTO>>(paginatedDepartments);

           
            var response = new PaginatedResponse<SelectDepartmentDTO>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Data = departmentDTOs
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
            [SwaggerOperation(Summary = "Retrieves a Department by ID", Description = "Fetches a single Department details based on its unique ID")]
            [SwaggerResponse(200, "Successfully retrieved the Department", typeof(SelectDepartmentDTO))]
            [SwaggerResponse(404, "Department not found")]
            [Produces("application/json")]


            public async Task<IActionResult> selectDepartmentById(int id)
            {
                if (id <= 0)
                    return BadRequest("The Department ID is invalid");
                

                Department _Department = await _unit.DepartmentRepo.selectById(id);


                if (_Department == null) return NotFound("Department not found.");

                var DepartmentDTO = mapper.Map<SelectDepartmentDTO>(_Department);

                return Ok(DepartmentDTO);
                

            }
            [HttpPost]
            [SwaggerOperation(
                Summary = "Creates a new Department",
                Description = "Adds a new Department info to the system. Requires admin privileges.")] // didn't do the admins yet
            [SwaggerResponse(201, "The Department was created")]
            [SwaggerResponse(400, "The Department data is invalid")]

            public async Task<IActionResult> addDepartment(AddDepartmentDTO _DepartmentDTO)
                {

                if (!ModelState.IsValid)
                    return NotFound("Invalid data provided for the department.");
 

                    Department newDepartment = mapper.Map<Department>(_DepartmentDTO);
                    List<Teacher> teachers = new List<Teacher>();
                    foreach (var teacherId in _DepartmentDTO.TeachersIDs)
                    {
                        var teacher = (Teacher)_unit.UserReps.GetUsersWithRole("Teacher").Result.FirstOrDefault(t => t.Id == teacherId);
                        teachers.Add(teacher);

                    }

                    List<Subject> subjects = new List<Subject>();
                    foreach (var subjectId in _DepartmentDTO.SubjectsIDs)
                    {
                        var subject = await _unit.SubjectRepo.selectById(subjectId);

                        subjects.Add(subject);

                    }

                    newDepartment.Teachers = teachers;
                    newDepartment.Subjects = subjects;


                    await _unit.DepartmentRepo.add(newDepartment);
                    await _unit.Save();
                    return CreatedAtAction("selectDepartmentById", new { id = newDepartment.Id }, _DepartmentDTO);

                
                }
        [HttpPut]
        [SwaggerOperation(Summary = "Edit an existing Department", Description = "Updates an existing Department with new details. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Department updated successfully." , typeof(List<SelectDepartmentDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Department data.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Department not found.")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> editDepartment(EditDepartmentDTO _DepartmentDTO)
        {

            if (!ModelState.IsValid) return BadRequest("The department data provided is invalid.");
            
            var _Department = await _unit.DepartmentRepo.selectById(_DepartmentDTO.Id);
            if (_Department == null) return NotFound("Department not found.");

            mapper.Map(_DepartmentDTO, _Department);

            if (_DepartmentDTO.flagAddOrOverwrite)  // if true , clear all the students within the current Department
            {
                _Department.Subjects.Clear();
                _Department.Teachers.Clear();
                List<Teacher> teachers = new List<Teacher>();
                foreach (var teacherId in _DepartmentDTO.TeachersIDs)
                {
                    var teacher = (Teacher) _unit.UserReps.GetUsersWithRole("Teacher").Result.FirstOrDefault(t => t.Id == teacherId);
                    teachers.Add(teacher);

                }

                List<Subject> subjects = new List<Subject>();
                foreach (var subjectId in _DepartmentDTO.SubjectsIDs)
                {
                    var subject = await _unit.SubjectRepo.selectById(subjectId);

                    subjects.Add(subject);

                }
                _Department.Subjects = subjects;
                _Department.Teachers = teachers;


            }
            else
            {

                foreach (var teacherId in _DepartmentDTO.TeachersIDs)
                {
                    var teacher = (Teacher)_unit.UserReps.GetUsersWithRole("Teacher").Result.FirstOrDefault(t => t.Id == teacherId);
                    _Department.Teachers.Add(teacher);

                }


                foreach (var subjectId in _DepartmentDTO.SubjectsIDs)
                {
                    var subject = await _unit.SubjectRepo.selectById(subjectId);

                    _Department.Subjects.Add(subject);

                }

            }
            _unit.DepartmentRepo.update(_Department);
            await _unit.Save();
            var departmentDTO = mapper.Map<SelectDepartmentDTO>(_Department);

            return Ok(departmentDTO);
                


        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a Department", Description = "Deletes a Department by its ID. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Department deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Department not found.")]
        [Produces("application/json")]
        public async Task<IActionResult> deleteDepartmentById(int id)
        {
            var _Department =await _unit.DepartmentRepo.selectById(id);
            if (_Department == null)
                return NotFound();

            _unit.DepartmentRepo.remove(_Department);
            _unit.DepartmentRepo.save();
            return Ok();
        }
    }
}

