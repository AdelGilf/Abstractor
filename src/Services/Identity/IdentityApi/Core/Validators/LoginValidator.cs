using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DTO;
using FluentValidation;
namespace Core.Validators
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("некоректная почта");
            RuleFor(x => x.Password).NotEmpty().WithMessage("пустая почта");
        }
    }
}
