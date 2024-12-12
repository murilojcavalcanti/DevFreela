using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Services.Commands.CommandsProject.DeleteProject
{
    public class DeleteProjectCommand:IRequest<ResultViewModel>
    {
        public DeleteProjectCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
