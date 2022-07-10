using AutoMapper;
using Contracts.DTO;
using Contracts.Models;

namespace Core
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<RegistrationDTO,User>();
        }
    }
}
