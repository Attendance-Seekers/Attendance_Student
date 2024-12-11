using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Attendance_Student.Models;
using Swashbuckle.AspNetCore.Annotations;
using Attendance_Student.DTOs.ParentDTOs;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Attendance_Student.UnitOfWorks;
using Attendance_Student.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ParentController : ControllerBase
    {
        UnitWork _unit;
        private readonly IMapper _mapper;

        public ParentController(
            UnitWork unit,
            IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retrieves all Parents with pagination",
            Description = "Fetches a paginated list of all Parents in the school"
        )]
        [SwaggerResponse(200, "Successfully retrieved the paginated list of Parents", typeof(PaginatedResponse<ParentResponseDto>))]
        [SwaggerResponse(404, "No parents found")]
        [Produces("application/json")]
        public IActionResult GetAllParents([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            
            var parents = _unit.UserReps.GetUsersWithRole("Parent").Result.OfType<Parent>().ToList();

            if (!parents.Any())
                return NotFound("There are no Parents");

           
            int totalCount = parents.Count;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var paginatedParents = parents
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

          
            var parentDTOs = _mapper.Map<List<ParentResponseDto>>(paginatedParents);

            var response = new PaginatedResponse<ParentResponseDto>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Data = parentDTOs
            };

            return Ok(response);
        }

        [Authorize(Roles ="Parent")]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieves a Parent by ID", Description = "Fetches a single Parent details based on its unique ID.")]
        [SwaggerResponse(200, "Successfully retrieved the Parent", typeof(ParentResponseDto))]
        [SwaggerResponse(404, "Parent not found")]
        public async Task<IActionResult> GetParentById(string id)
        {
            var parent = await _unit.UserReps.GetUserById(id);
            if (parent == null)
                return NotFound();

            var parentDTO = _mapper.Map<ParentResponseDto>(parent);
            return Ok(parentDTO);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new Parent", Description = "Adds a new Parent info to the system.")]
        [SwaggerResponse(201, "The Parent was created", typeof(ParentResponseDto))]
        [SwaggerResponse(400, "The Parent data is invalid")]
        public async Task<IActionResult> CreateParent([FromForm] ParentCreateDto parentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newParent = _mapper.Map<Parent>(parentDTO);
            var createResult = await _unit.UserReps.CreateUser(newParent, parentDTO.Password);

            if (!createResult.Succeeded)
                return BadRequest(createResult.Errors);

            var createdParent = _mapper.Map<ParentResponseDto>(newParent);
            return CreatedAtAction(nameof(GetParentById), new { id = newParent.Id }, createdParent);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates an existing Parent", Description = "Edits a Parent's information by ID.")]
        [SwaggerResponse(200, "The Parent was updated successfully", typeof(ParentResponseDto))]
        [SwaggerResponse(404, "Parent not found")]
        public async Task<IActionResult> UpdateParent(string id, [FromForm] ParentUpdateDto parentDTO)
        {
            var parent = await _unit.UserReps.GetUserById(id);
            if (parent == null)
                return NotFound($"No Parent found with the id: {id}.");

            _mapper.Map(parentDTO, parent);
            var updateResult = await _unit.UserReps.UpdateUser(parent);

            if (!updateResult.Succeeded)
                return BadRequest(updateResult.Errors);

            return Ok(_mapper.Map<ParentResponseDto>(parent));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a Parent", Description = "Removes a Parent from the system.")]
        [SwaggerResponse(200, "Parent deleted successfully")]
        [SwaggerResponse(404, "Parent not found")]
        public async Task<IActionResult> DeleteParent(string id)
        {
            var parent = await _unit.UserReps.GetUserById(id);
            if (parent == null)
                return NotFound($"No Parent found with the id: {id}.");

            var deleteResult = await _unit.UserReps.RemoveUser(parent);

            if (!deleteResult.Succeeded)
                return BadRequest(deleteResult.Errors);

            return Ok();
        }

        [HttpGet("attendance/{studentId}")]
        [SwaggerOperation(
            Summary = "View Attendance Report",
            Description = "Fetches the attendance report of a specific student."
        )]
        [SwaggerResponse(200, "Attendance report retrieved successfully", typeof(ParentAttendanceReportDto))]
        [SwaggerResponse(404, "Student not found")]
        public async Task<ActionResult<ParentAttendanceReportDto>> ViewAttendanceReport(
            string studentId,
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate)
        {
            var student = await _unit.StudentRepo.selectUserById(studentId);

            if (student == null)
                return NotFound($"No Student found with the id: {studentId}.");

            var attendanceRecords = student.viewAttendances
                .Where(va => va.attendance.dateAttendance >= startDate &&
                             va.attendance.dateAttendance <= endDate)
                .Select(va => new AttendanceRecordDto
                {
                    Date = va.attendance.dateAttendance,
                    Status = va.Status
                }).ToList();

            var totalDays = (endDate.DayNumber - startDate.DayNumber) + 1;
            var presentDays = attendanceRecords.Count(r => r.Status == "Present");
            var attendancePercentage = (decimal)presentDays / totalDays * 100;

            return new ParentAttendanceReportDto
            {
                StudentId = studentId,
                StudentName = student.Student_fullname,
                StartDate = startDate,
                EndDate = endDate,
                AttendanceRecords = attendanceRecords,
                AttendancePercentage = attendancePercentage
            };
        }
    }
}