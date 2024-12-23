using DevFreela.Application.Services.Commands.CommandsProject.InsertProject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators.ValidatorsProject
{
    public class InsertProjectValidator:AbstractValidator<InsertProjectCommand>
    {
        public InsertProjectValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("Não pode ser Vazio")
                .MaximumLength(50)
                .WithMessage("Tamanho Maximo é 50.");

            RuleFor(p => p.TotalCost)
            .GreaterThan(1000)
            .WithMessage("O custo deve ser maior que R$1000,00") ;
        }
    }
}
