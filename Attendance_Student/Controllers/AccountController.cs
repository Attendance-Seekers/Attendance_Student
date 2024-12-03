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

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
[SwaggerOperation(Summary = "Register a new user", Description = "Creates a new user account.")]
[SwaggerResponse(StatusCodes.Status200OK, "Registration successful.")]
[SwaggerResponse(StatusCodes.Status400BadRequest, "Validation errors or registration failure.")]
public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
{
    // Check if the provided role exists
    if (!await _roleManager.RoleExistsAsync(registerDto.Role))
    {
        return BadRequest($"Role '{registerDto.Role}' does not exist.");
    }

    // Create a new user object with the provided details
    var user = new IdentityUser
    {
        UserName = registerDto.Username,
        Email = registerDto.Email,
        PhoneNumber = registerDto.Phone
    };

    // Save the user's name as a claim or in a custom property if extending IdentityUser
    var result = await _userManager.CreateAsync(user, registerDto.Password);

    if (result.Succeeded)
    {
        // Add the user to the specified role
        await _userManager.AddToRoleAsync(user, registerDto.Role);

       

        return Ok(new { Message = "Registration successful", UserId = user.Id });
    }

    return BadRequest(result.Errors);
}



        [HttpPost("login")]
        [SwaggerOperation(Summary = "User login", Description = "Authenticates a user and generates a JWT token.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Login successful. Returns JWT token.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid username or password.")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(
                loginDto.Username,
                loginDto.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginDto.Username);
                var roles = await _userManager.GetRolesAsync(user);

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
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return Unauthorized();

            var result = await _userManager.ChangePasswordAsync(
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
            await _signInManager.SignOutAsync();
            return Ok(new { Message = "Logged out successfully." });
        }
    }
}
