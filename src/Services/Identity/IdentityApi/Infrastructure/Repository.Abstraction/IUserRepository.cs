using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace Infrastructure.Repository.Abstraction
{
    public interface IUserRepository
    {
        public Task<Result<User>> GetUserAsync(string email);

        public Task<Result> CreateUserAsync(User user);

        public Task<Result> UpdateUserAsync(User user);

        public Task<Result> DeleteUserAsync(string email);
    }
}
