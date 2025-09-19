using KafeApı.Aplication.DTOS.UserDtos;
using KafeApı.Aplication.Interfaces;
using KafeApı.Persistance.Context.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Persistance.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly RoleManager<AppIdentityRole> _roleManager;

        public UserRepository(UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager, RoleManager<AppIdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<bool> AddRoleToUserAsync(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                return false; // ✅ Rol yoksa false döndür
            }

            // ✅ Kullanıcı zaten bu role sahip mi kontrolü
            var hasRole = await _userManager.IsInRoleAsync(user, roleName);
            if (hasRole)
            {
                return true; // Zaten rol atanmış
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<UserDto> CheckUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return new UserDto
                {
                    Email = user.Email,
                    Id = user.Id,
                    Roles = userRoles.Any() ? userRoles.FirstOrDefault() : "User"
                };
            }
            return new UserDto();
        }

        public async Task<SignInResult> CheckUserWithPassword(LoginDto dto)
        {
            // ✅ Input validation
            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                return SignInResult.Failed;
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) // ✅ Null kontrolü
            {
                return SignInResult.Failed;
            }

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);
            return result;
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return false;
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (roleExist)
            {
                return false; // Rol zaten var
            }

            var result = await _roleManager.CreateAsync(new AppIdentityRole { Name = roleName });
            return result.Succeeded;
        }

        public async Task<SignInResult> LoginAsync(LoginDto dto)
        {
            // ✅ Input validation
            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                return SignInResult.Failed;
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) // ✅ Null kontrolü
            {
                return SignInResult.Failed;
            }

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, true, false);
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
        {
            // ✅ Input validation
            if (dto == null || string.IsNullOrEmpty(dto.Email))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidInput",
                    Description = "Geçersiz giriş bilgileri"
                });
            }

            // ✅ Email benzersizlik kontrolü
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "DuplicateEmail",
                    Description = "Bu email adresi zaten kullanılıyor"
                });
            }

            var user = new AppIdentityUser
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                PhoneNumber = dto.Phone,
                UserName = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            return result;
        }
    }
}