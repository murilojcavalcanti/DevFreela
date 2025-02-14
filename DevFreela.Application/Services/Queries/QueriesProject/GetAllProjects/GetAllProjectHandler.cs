using Azure;
using DevFreela.Application.Models;
using DevFreela.Application.Models.project;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.ProjectRepositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Queries.QueriesProject.GetAllProjects
{
    public class GetAllProjectHandler : IRequestHandler<GetAllProjectQuery, ResultViewModel<List<ProjectItemViewModel>>>
    {
        private readonly IProjectRepository _repository;

        public GetAllProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAll(p => p.IdClient == request.UserId);
            List<ProjectItemViewModel> model = projects.Select(p => ProjectItemViewModel.FromEntity(p)).ToList();
            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }
    }
}
