using Contracts.DTO;
using Contracts.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Abstraction
{
    public interface IUserService
    {
        public Task<Result<AuthDTO>> RegistrationAsync(RegistrationDTO registrationDTO);

        public Task<Result> UpdatePasswordAsync(string password);

        public Task<Result> UpdateUserAsync(UserDTO userDTO);

        public Task<Result<AuthDTO>> LoginAsync(LoginDTO loginDTO);

        public Task<Result> DeleteUserAsync();

        public Task<Result<AuthDTO>> UpdateEmailAsync(string email);
    }
}
