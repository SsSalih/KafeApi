using KafeApı.Aplication.DTOS.UserDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Interfaces
{
    public interface IUserRepository
    {
        Task<SignInResult> LoginAsync(LoginDto dto);
        Task LogoutAsync();
        Task<IdentityResult> RegisterAsync(RegisterDto dto);
        Task<UserDto> CheckUser(string email);
        Task<SignInResult> CheckUserWithPassword(LoginDto dto);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AddRoleToUserAsync(string email, string roleName);
    }
}
