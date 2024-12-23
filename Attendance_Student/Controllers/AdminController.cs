﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Attendance_Student.Models;
using Swashbuckle.AspNetCore.Annotations;
using Attendance_Student.DTOs.AdminDTOs;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Attendance_Student.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly UnitWork _unit;
        
        private readonly IMapper _mapper;

        public AdminController(UnitWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all Admins", Description = "Fetches a list of all Admins in the system.")]
        [SwaggerResponse(200, "Successfully retrieved the list of Admins", typeof(List<AdminDTO>))]
        [SwaggerResponse(404, "No admins found")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _unit.UserReps.GetUsersWithRole("Admin");

            if (!admins.Any())
                return NotFound("No admins found in the system.");

            var adminDTOs = _mapper.Map<List<AdminDTO>>(admins);
            return Ok(adminDTOs);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieves an Admin by ID", Description = "Fetches a single Admin details based on its unique ID.")]
        [SwaggerResponse(200, "Successfully retrieved the Admin", typeof(AdminDTO))]
        [SwaggerResponse(404, "Admin not found")]
        public async Task<IActionResult> GetAdminById(string id)
        {
            // Retrieve the user
            var user = await _unit.UserReps.GetUserById(id);

            // Check if user exists and is an admin
            if (user == null)
                return NotFound($"User with ID '{id}' not found.");

            // Additional role validation
            bool isAdmin = await _unit.UserReps.checkUserRole(user, "Admin");
            if (!isAdmin)
                return Forbid(); // or return Unauthorized() depending on your requirements

            // Map to DTO using AutoMapper
            var adminDTO = _mapper.Map<AdminDTO>(user);

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
            var createResult = await _unit.UserReps.CreateUser(newAdmin, adminDTO.Password);

            if (!createResult.Succeeded)
                return BadRequest(createResult.Errors);

            var roleResult = await _unit.UserReps.AddRole(newAdmin, "Admin");
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
            Admin admin = (Admin)await _unit.UserReps.GetUserById(id);
            if (admin == null || !await _unit.UserReps.checkUserRole(admin, "Admin"))
                return NotFound($"Admin with ID '{id}' not found.");

            _mapper.Map(adminDTO, admin);
            var updateResult = await _unit.UserReps.UpdateUser(admin);

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
            var admin = await _unit.UserReps.GetUserById(id);
            if (admin == null || !await _unit.UserReps.checkUserRole(admin, "Admin"))
                return NotFound($"Admin with ID '{id}' not found.");

            var deleteResult = await _unit.UserReps.RemoveUser(admin);

            if (!deleteResult.Succeeded)
                return BadRequest(deleteResult.Errors);

            return Ok("Admin deleted successfully.");
        }
    }
}
