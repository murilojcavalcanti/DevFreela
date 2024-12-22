using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.ProjectRepositories;
using MediatR;

namespace DevFreela.Application.Services.Commands.CommandsProject.CompleteProject
{
    public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
    {

        private readonly IProjectRepository _repository;

        public CompleteProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _repository.GetById(request.Id);
            
            if (project is null) return ResultViewModel.Error("Projeto não existe!");
            
            project.Complete();
            await _repository.Update(project);
            return ResultViewModel.Success();
        }
    }
}
