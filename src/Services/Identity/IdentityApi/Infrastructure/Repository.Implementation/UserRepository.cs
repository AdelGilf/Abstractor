using Contracts.Models;
using Infrastructure.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private Context _context = null!;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<Result> CreateUserAsync(User user)
        {

            if (_context.Users.FirstOrDefault(x => user.Email == x.Email) == null)
            {
                await _context.AddAsync<User>(user);
                await _context.SaveChangesAsync();
                return Result.Ok();
            }
            return Result.Fail("почта занята");
        }

        public async Task<Result> DeleteUserAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return Result.Fail("пользователь не найден");
            }
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result<User>> GetUserAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user is null)
            {
                return Result.Fail("пользователь не найден");
            }
            return Result.Ok<User>(user);
        }

        public async Task<Result> UpdateUserAsync(User user)
        {
            _context.Update<User>(user);
            await _context.SaveChangesAsync();
            return Result.Ok();
        }
    }
}
