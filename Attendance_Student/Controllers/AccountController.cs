using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Attendance_Student.Models;
using Attendance_Student.DTOs.Account_DTOs;
using Attendance_Student.UnitOfWorks;

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly UnitWork _unit;

        public AccountController(
            IConfiguration configuration,
            UnitWork unit)
        {
            _unit = unit;
            _configuration = configuration;
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register a new user", Description = "Creates a new user account.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Registration successful.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation errors or registration failure.")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
   

            Student user = new Student()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                PhoneNumber = registerDto.Phone
            };

    
            var result = await _unit.UserReps.CreateUser(user, registerDto.Password);

            if (result.Succeeded)
            {
        
                await _unit.UserReps.AddRole(user, registerDto.Role);

       

                return Ok(new { Message = "Registration successful", UserId = user.Id , Username = user.UserName });
            }

            return BadRequest(result.Errors);
        }



        [HttpPost("login")]
        [SwaggerOperation(Summary = "User login", Description = "Authenticates a user and generates a JWT token.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Login successful. Returns JWT token.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid username or password.")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var result = await _unit.UserReps.UserSignIn(loginDto);

            if (result.Succeeded)
            {
                var user = await _unit.UserReps.GetUserByName(loginDto.Username);
                var roles = await _unit.UserReps.GetRoles(user);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
                var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: signingCredentials
                );

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Username = user.UserName,
                    Roles = roles
                });
            }

            return Unauthorized("Invalid login attempt.");
        }

        [HttpPost("change-password")]
        [Authorize]
        [SwaggerOperation(Summary = "Change user password", Description = "Allows an authenticated user to change their password.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Password changed successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed to change password.")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDto)
        {
            var user = await _unit.UserReps.GetUserByName(User.Identity.Name);

            if (user == null)
                return Unauthorized();

            var result = await _unit.UserReps.ChangePassword(
                user,
                changePasswordDto.OldPassword,
                changePasswordDto.NewPassword
            );

            if (result.Succeeded)
                return Ok(new { Message = "Password changed successfully" });

            return BadRequest(result.Errors);
        }

        [HttpPost("logout")]
        [Authorize]
        [SwaggerOperation(Summary = "User logout", Description = "Logs out the authenticated user.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Logged out successfully.")]
        public async Task<IActionResult> Logout()
        {
            await _unit.UserReps.Logout();
            return Ok(new { Message = "Logged out successfully." });
        }
    }
}
