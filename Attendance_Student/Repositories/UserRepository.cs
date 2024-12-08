using Attendance_Student.DTOs.Account_DTOs;
using Attendance_Student.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Student.Repositories
{
    public class UserRepository
    {
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        SignInManager<IdentityUser> _signInManager;
        public UserRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager , RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUser(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<IdentityResult> AddRole(IdentityUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }
        public async Task<IdentityUser> GetUserByName(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
        public async Task<IdentityUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        public async Task<bool> CheckPassword(IdentityUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
        public async Task<IdentityResult> ChangePassword(IdentityUser user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }
        public async Task<SignInResult> UserSignIn(LoginDTO loginDto)
        {
            return await _signInManager.PasswordSignInAsync(
                loginDto.Username,
                loginDto.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> UpdateUser(IdentityUser user)
        {
            return await _userManager.UpdateAsync(user);
        }
        public async Task<IList<string>> GetRoles(IdentityUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
        public async Task<IList<IdentityUser>> GetUsersWithRole(string role)
        {
            return await _userManager.GetUsersInRoleAsync(role);
        }
        public async Task<bool> checkUserRole(IdentityUser user , string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }
        public async Task<IdentityResult> RemoveUser(IdentityUser user)
        {
            return await _userManager.DeleteAsync(user);
        }
        public async Task<IdentityResult> RemoveUserFromRole(IdentityUser user , string role)
        {
            return await _userManager.RemoveFromRoleAsync(user , role);
        }
        public async Task<List<IdentityUser>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}
