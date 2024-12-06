using Attendance_Student.DTOs.SubjectDTO;
using Attendance_Student.Models;
using Attendance_Student.Repositories;
using Attendance_Student.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        //AttendanceStudentContext db;
        //GenericRepository<Subject> subjectRepo;
        UnitWork unit;
        IMapper mapper;
        public SubjectController(UnitWork unit, IMapper mapper)
        {
          this.unit = unit;
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

        public IActionResult selectAllSubjectss()
        {
            //Console.WriteLine("selectALLLLLLLLLLLLLLLLLLLLLL");
            List<Subject> subjects = unit.SubjectRepo.selectAll();

            if (subjects.Count < 0) return NotFound();
            else
            {

                var subjectDTO = mapper.Map<List<SelectSubjectDTO>>(subjects);

                return Ok(subjectDTO);
            }
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(
         Summary = "Retrieves a Subject by ID",
         Description = "Fetches a single Subject details based on its unique ID"
            )]
        [SwaggerResponse(200, "Successfully retrieved the Subject", typeof(SelectSubjectDTO))]
        [SwaggerResponse(404, "Subject not found")]
        [Produces("application/json")]


        public IActionResult selectSubjectById(int id)
        {

            Subject subject = unit.SubjectRepo.selectById(id);


            if (subject == null) return NotFound();
            else
            {
                var subjectDTO = mapper.Map<SelectSubjectDTO>(subject);

              
                return Ok(subjectDTO);
            }

        }
        [HttpPost]
        [SwaggerOperation(
    Summary = "Creates a new Subject",
    Description = "Adds a new Subject info to the system. Requires admin privileges.")] // didn't do the admins yet
        [SwaggerResponse(201, "The Subject was created", typeof(SelectSubjectDTO))]
        [SwaggerResponse(400, "The Subject data is invalid")]
        //[Produces("application/json")]
        //[Consumes("application/json")]
        public IActionResult addSubject([FromForm] AddSubjectDTO subjectDTO)
        {

            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            else
            {

                Subject newSubject = mapper.Map<Subject>(subjectDTO);



                unit.SubjectRepo.add(newSubject);
                unit.SubjectRepo.save();
                return CreatedAtAction("selectSubjectById", new { id = newSubject.subject_Id }, subjectDTO);
               

            }
        }
        [HttpPut]
        [SwaggerOperation(Summary = "Edit an existing Subject", Description = "Updates an existing Subject with new details. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Subject updated successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Subject data.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Subject not found.")]
        //[Produces("application/json")]
        //[Consumes("application/json")]
        public IActionResult editSubject(EditSubjectDTO subjectDTO)
        {

            if (ModelState.IsValid)
            {
                var subjcet = unit.SubjectRepo.selectById(subjectDTO.subject_Id);
                if (subjcet == null) return NotFound();
                else
                {
                    mapper.Map(subjectDTO, subjcet);
                    unit.SubjectRepo.update(subjcet);
                    unit.SubjectRepo.save();
                    return Ok();
                }
            }
            else { return BadRequest(); }


        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a Subject", Description = "Deletes a Subject by its ID. Requires admin privileges.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Subject deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Subject not found.")]
        [Produces("application/json")]
        public IActionResult deleteSubjectById(int id)
        {
            var subjcet = unit.SubjectRepo.selectById(id);
            if (subjcet == null)
            {
                return NotFound();
            }
            else
            {
                unit.SubjectRepo.remove(subjcet);
                unit.SubjectRepo.save();
                return Ok();
            }

        }
    }
}
