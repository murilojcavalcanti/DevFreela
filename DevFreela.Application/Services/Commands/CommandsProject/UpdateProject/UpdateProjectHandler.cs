using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.ProjectRepositories;
using MediatR;

namespace DevFreela.Application.Services.Commands.CommandsProject.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;

        public UpdateProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _repository.GetById(request.IdProject);
            if (project is null) return ResultViewModel.Error("Projeto não existe");
            project.Update(request.Title, request.Description, request.TotalCost);
            await _repository.Update(project);
            return ResultViewModel.Success();
        }
    }
}
