using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Attendance_Student.Models;
using Swashbuckle.AspNetCore.Annotations;
using Attendance_Student.DTOs.AdminDTOs;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all Admins", Description = "Fetches a list of all Admins in the system.")]
        [SwaggerResponse(200, "Successfully retrieved the list of Admins", typeof(List<AdminDTO>))]
        [SwaggerResponse(404, "No admins found")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");

            if (!admins.Any())
                return NotFound();

            var adminDTOs = _mapper.Map<List<AdminDTO>>(admins);
            return Ok(adminDTOs);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieves an Admin by ID", Description = "Fetches a single Admin details based on its unique ID.")]
        [SwaggerResponse(200, "Successfully retrieved the Admin", typeof(AdminDTO))]
        [SwaggerResponse(404, "Admin not found")]
        public async Task<IActionResult> GetAdminById(string id)
        {
            var admin = await _userManager.FindByIdAsync(id);
            if (admin == null || !await _userManager.IsInRoleAsync(admin, "Admin"))
                return NotFound();

            var adminDTO = _mapper.Map<AdminDTO>(admin);
            return Ok(adminDTO);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new Admin", Description = "Adds a new Admin info to the system.")]
        [SwaggerResponse(201, "The Admin was created", typeof(AdminDTO))]
        [SwaggerResponse(400, "The Admin data is invalid")]
        public async Task<IActionResult> CreateAdmin([FromForm] AddAdminDTO adminDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newAdmin = _mapper.Map<IdentityUser>(adminDTO);
            var createResult = await _userManager.CreateAsync(newAdmin, adminDTO.Password);

            if (!createResult.Succeeded)
                return BadRequest(createResult.Errors);

            var roleResult = await _userManager.AddToRoleAsync(newAdmin, "Admin");
            if (!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            var createdAdmin = _mapper.Map<AdminDTO>(newAdmin);
            return CreatedAtAction(nameof(GetAdminById), new { id = newAdmin.Id }, createdAdmin);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates an existing Admin", Description = "Edits an Admin's information by ID.")]
        [SwaggerResponse(200, "The Admin was updated successfully", typeof(AdminDTO))]
        [SwaggerResponse(404, "Admin not found")]
        public async Task<IActionResult> UpdateAdmin(string id, [FromForm] EditAdminDTO adminDTO)
        {
            var admin = await _userManager.FindByIdAsync(id);
            if (admin == null || !await _userManager.IsInRoleAsync(admin, "Admin"))
                return NotFound();

            _mapper.Map(adminDTO, admin);
            var updateResult = await _userManager.UpdateAsync(admin);

            if (!updateResult.Succeeded)
                return BadRequest(updateResult.Errors);

            return Ok(_mapper.Map<AdminDTO>(admin));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes an Admin", Description = "Removes an Admin from the system.")]
        [SwaggerResponse(200, "Admin deleted successfully")]
        [SwaggerResponse(404, "Admin not found")]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            var admin = await _userManager.FindByIdAsync(id);
            if (admin == null || !await _userManager.IsInRoleAsync(admin, "Admin"))
                return NotFound();

            var deleteResult = await _userManager.DeleteAsync(admin);

            if (!deleteResult.Succeeded)
                return BadRequest(deleteResult.Errors);

            return Ok();
        }
    }
}
