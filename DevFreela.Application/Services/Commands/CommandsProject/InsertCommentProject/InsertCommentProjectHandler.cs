using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.ProjectRepositories;
using MediatR;

namespace DevFreela.Application.Services.Commands.CommandsProject.InsertCommentProject
{
    public class InsertCommentProjectHandler : IRequestHandler<InsertCommentProjectCommand, ResultViewModel>
    {

        private readonly IProjectRepository _repository;

        public InsertCommentProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }


        public async Task<ResultViewModel> Handle(InsertCommentProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.Exists(request.IdProject);
            if (project == false) return ResultViewModel.Error("Projeto não existe!");

            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);
            await _repository.AddComment(comment);

            return ResultViewModel.Success();

        }
    }
}
