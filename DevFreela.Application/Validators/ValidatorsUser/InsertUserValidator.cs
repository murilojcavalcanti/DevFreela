using DevFreela.Application.Services.Commands.CommandUser.InsertUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators.ValidatorsUser
{
    public class InsertUserValidator : AbstractValidator<InsertUserCommand>
    {
        public InsertUserValidator()
        {
            RuleFor(U => U.FullName).MinimumLength(3).WithMessage("Seu nome deve conter no minimo 3 caracteres");

            RuleFor(u => u.Email).EmailAddress().WithMessage("Deve ser um email válido");

            RuleFor(u => u.BirthDate).Must(b=>b<DateTime.Now.AddYears(-16)).WithMessage("você deve ser maior de 16 anos");
        }
    }
}
