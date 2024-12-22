using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.ProjectRepositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Services.Commands.CommandsProject.StartProject
{
    public class StartProjectHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;

        public StartProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _repository.GetById(request.Id);
            if (project is null) return ResultViewModel.Error("Projeto não existe");

            project.Start();
            await _repository.Update(project);
            
            return ResultViewModel.Success();

        }
    }
}
