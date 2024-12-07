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

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly UserManager<Parent> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly AttendanceStudentContext _context;

        public ParentController(
            UserManager<Parent> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            AttendanceStudentContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all Parents", Description = "Fetches a list of all Parents in the system.")]
        [SwaggerResponse(200, "Successfully retrieved the list of Parents", typeof(List<ParentResponseDto>))]
        [SwaggerResponse(404, "No parents found")]
        public async Task<IActionResult> GetAllParents()
        {
            var parents = await _userManager.Users.ToListAsync();

            if (!parents.Any())
                return NotFound();

            var parentDTOs = _mapper.Map<List<ParentResponseDto>>(parents);
            return Ok(parentDTOs);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieves a Parent by ID", Description = "Fetches a single Parent details based on its unique ID.")]
        [SwaggerResponse(200, "Successfully retrieved the Parent", typeof(ParentResponseDto))]
        [SwaggerResponse(404, "Parent not found")]
        public async Task<IActionResult> GetParentById(string id)
        {
            var parent = await _userManager.FindByIdAsync(id);
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
            var createResult = await _userManager.CreateAsync(newParent, parentDTO.Password);

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
            var parent = await _userManager.FindByIdAsync(id);
            if (parent == null)
                return NotFound();

            _mapper.Map(parentDTO, parent);
            var updateResult = await _userManager.UpdateAsync(parent);

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
            var parent = await _userManager.FindByIdAsync(id);
            if (parent == null)
                return NotFound();

            var deleteResult = await _userManager.DeleteAsync(parent);

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
            var student = await _context.Students
                .Include(s => s.viewAttendances)
                    .ThenInclude(va => va.attendance)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
                return NotFound();

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