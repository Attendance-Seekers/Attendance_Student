using Attendance_Student.DTOs.SubjectDTO;
using Attendance_Student.Models;
using Attendance_Student.Repositories;
using Attendance_Student.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SubjectController : ControllerBase
    {
        UnitWork _unit;
        IMapper mapper;
        public SubjectController(UnitWork unit, IMapper mapper)
        {
          _unit = unit;
            this.mapper = mapper;
        }
        [HttpGet]
        [SwaggerOperation
            (
            Summary = "Retrieves all Subjects",
            Description = "Fetches a list of all Subjects in the school"
            )]
        [SwaggerResponse(200, "Successfully retrieved the list of subjects", typeof(List<SelectSubjectDTO>))]
        [SwaggerResponse(404, "No classes found")]
        [Produces("application/json")]

        public async Task<IActionResult> selectAllSubjects()
        {
            List<Subject> subjects = await _unit.SubjectRepo.selectAll();

            if (subjects.Count < 0) return NotFound("No subjects were found in the system. Please check again later.");

            var subjectDTO = mapper.Map<List<SelectSubjectDTO>>(subjects);

            return Ok(subjectDTO);
            
        }
        [Authorize(Roles = "Admin , Teacher")]
        [HttpGet("{id:int}")]
        [SwaggerOperation(
         Summary = "Retrieves a Subject by ID",
         Description = "Fetches a single Subject details based on its unique ID"
            )]
        [SwaggerResponse(200, "Successfully retrieved the Subject", typeof(SelectSubjectDTO))]
        [SwaggerResponse(404, "Subject not found")]
        [Produces("application/json")]

        public async Task<IActionResult> selectSubjectById(int id)
        {

            Subject subject = await _unit.SubjectRepo.selectById(id);

            if (subject == null) return NotFound("No subjects were found in the system. Please check again later.");
  
            var subjectDTO = mapper.Map<SelectSubjectDTO>(subject);
 
            return Ok(subjectDTO);            

        }
        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Subject",
            Description = "Adds a new Subject info to the system. Requires admin privileges.")] // didn't do the admins yet
        [SwaggerResponse(201, "The Subject was created", typeof(SelectSubjectDTO))]
        [SwaggerResponse(400, "The Subject data is invalid")]
        //[Produces("application/json")]
        //[Consumes("application/json")]
        public async Task<IActionResult> addSubject([FromForm] AddSubjectDTO subjectDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            Subject newSubject = mapper.Map<Subject>(subjectDTO);

            await _unit.SubjectRepo.add(newSubject);
            await _unit.Save();
            return CreatedAtAction("selectSubjectById", new { id = newSubject.subject_Id }, subjectDTO);


               

            
        }
        [HttpPut]
        [SwaggerOperation(Summary = "Edit an existing Subject", Description = "Updates an existing Subject with new details. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Subject updated successfully." , typeof(SelectSubjectDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Subject data.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Subject not found.")]
        //[Produces("application/json")]
        //[Consumes("application/json")]
        public async Task<IActionResult> editSubjectAsync(EditSubjectDTO subjectDTO)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var subject = await _unit.SubjectRepo.selectById(subjectDTO.subject_Id);
            if (subject == null) return NotFound("No subjects were found in the system. Please check again later.");
            else
            {
                mapper.Map(subjectDTO, subject);
                _unit.SubjectRepo.update(subject);
                _unit.SubjectRepo.save();
                var subjectShow = mapper.Map<SelectSubjectDTO>(subject);
                return Ok(subjectShow);
            }
   


        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a Subject", Description = "Deletes a Subject by its ID. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Subject deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Subject not found.")]
        [Produces("application/json")]
        public async Task<IActionResult> deleteSubjectById(int id)
        {
            var subject = await _unit.SubjectRepo.selectById(id);
            if (subject == null)
                return NotFound("No subjects were found in the system. Please check again later.");
  
            _unit.SubjectRepo.remove(subject);
            await _unit.Save();
            return Ok();
            

        }
    }
}
