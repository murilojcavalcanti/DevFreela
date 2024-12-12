using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandsProject.CompleteProject
{
    public class CompleteProjectCommand: IRequest<ResultViewModel>
    {

        public CompleteProjectCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
