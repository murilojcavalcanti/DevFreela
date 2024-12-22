using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.ProjectRepositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Services.Commands.CommandsProject.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;

        public DeleteProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            Project project =await  _repository.GetById(request.Id);
            if (project is null) return ResultViewModel.Error("Projeto não existe!");
            project.SetAsDeleted();
            await _repository.Update(project);
            return ResultViewModel.Success();
        }
    }
}
