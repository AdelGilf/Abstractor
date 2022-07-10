using AutoMapper;
using Contracts.DTO;
using Contracts.Models;
using Core.Services.Abstraction;
using FluentResults;
using FluentValidation;
using Infrastructure;
using Infrastructure.Repository.Abstraction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidatorExtension;
using FluentResultExtension;

namespace Core.Services.Implementation
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IValidator<RegistrationDTO> _registrationValidator;
        private IValidator<LoginDTO> _loginValidator;
        private IMapper _mapper;
        private IJwtService _jwtService;
        private ICurrentUserService _currentUserService;
        private IHashingService _hashingService;
        public UserService(IUserRepository userRepository, ICurrentUserService currentUserService, IValidator<RegistrationDTO> registrationValidator, IMapper mapper, IJwtService jwtService, IValidator<LoginDTO> loginValidator, IHashingService hashingService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _registrationValidator = registrationValidator;
            _mapper = mapper;
            _jwtService = jwtService;
            _loginValidator = loginValidator;
            _hashingService = hashingService;
        }

        public async Task<Result> DeleteUserAsync()
        {
            var result = await _userRepository.DeleteUserAsync(_currentUserService.GetUserEmail());
            return result;
        }

        public async Task<Result<AuthDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var succes = _loginValidator.Validate(loginDTO);
            if (succes.IsValid)
            {
                var result = await _userRepository.GetUserAsync(loginDTO.Email);
                if (result.IsFailed)
                {
                    return Result.Fail(result.GetErrors());
                }
                if (result.Value.Password == _hashingService.Hashing(loginDTO.Password))
                {
                    AuthDTO authDTO = new AuthDTO() { Nickname = result.Value.NickName, Token = _jwtService.GetJwt(result.Value.Email), RefreshToken = "refreshtoken" };
                    return Result.Ok(authDTO);
                }
                Result.Fail("неверный пароль");
            }
            return Result.Fail(succes.OutputErrors());
        }

        public async Task<Result<AuthDTO>> RegistrationAsync(RegistrationDTO registrationDTO)
        {
            var succes = _registrationValidator.Validate(registrationDTO);
            if (succes.IsValid)
            {

                var user = _mapper.Map<User>(registrationDTO);
                user.Password = _hashingService.Hashing(user.Password);
                var result = await _userRepository.CreateUserAsync(user);
                if (result.IsFailed)
                {
                    return result;
                }
                AuthDTO authDTO = new AuthDTO() { Nickname = user.NickName, Token = _jwtService.GetJwt(user.Email), RefreshToken = "refreshtoken" };
                return Result.Ok<AuthDTO>(authDTO);
            }
            return Result.Fail(succes.OutputErrors());
        }

        public async Task<Result<AuthDTO>> UpdateEmailAsync(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                return Result.Fail("некорректная почта");
            }
            var result = await _userRepository.GetUserAsync(_currentUserService.GetUserEmail());
            if (result.IsFailed)
            {
                return Result.Fail(result.GetErrors());
            }
            var user = result.Value;
            user.Email = email;
            result = await _userRepository.UpdateUserAsync(user);
            if (result.IsFailed)
            {
                return Result.Fail(result.GetErrors());
            }
            AuthDTO authDTO = new AuthDTO() { Nickname = user.NickName, Token = _jwtService.GetJwt(user.Email), RefreshToken = "refreshtoken" };
            return Result.Ok<AuthDTO>(authDTO);
        }

        public async Task<Result> UpdatePasswordAsync(string password)
        {
            var result = await _userRepository.GetUserAsync(_currentUserService.GetUserEmail());
            if (result.IsFailed)
            {
                return Result.Fail(result.GetErrors());
            }
            var user = result.Value;
            user.Password = _hashingService.Hashing(password);
            result = await _userRepository.UpdateUserAsync(user);
            if (result.IsFailed)
            {
                return Result.Fail(result.GetErrors());
            }
            return Result.Ok();
        }

        public async Task<Result> UpdateUserAsync(UserDTO userDTO)
        {
            {
                var result = await _userRepository.GetUserAsync(_currentUserService.GetUserEmail());
                if (result.IsFailed)
                {
                    return Result.Fail(result.GetErrors());
                }
                var user = result.Value;
                user.NickName = userDTO.NickName;
                result = await _userRepository.UpdateUserAsync(user);
                if (result.IsFailed)
                {
                    return Result.Fail(result.GetErrors());
                }
                return Result.Ok();
            }
        }
    }
}
