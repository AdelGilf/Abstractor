using Contracts.DTO;
using Contracts.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validators
{
    public class RegistrationValidator : AbstractValidator<RegistrationDTO>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("некоректная почта");
            RuleFor(x => x.Password).NotEmpty().WithMessage("пустая почта");
            RuleFor(x => x.NickName).NotEmpty().WithMessage("пустое имя");
        }
    }
}
