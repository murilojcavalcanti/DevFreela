﻿using DevFreela.Application.Models;
using DevFreela.Application.Models.project;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories.ProjectRepositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Queries.QueriesProject.GetbyIdProjects
{
    public class GetByIdProjectsHandler : IRequestHandler<GetbyIdProjectsQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _context;
        private readonly IProjectRepository _repository;

        public GetByIdProjectsHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetbyIdProjectsQuery request, CancellationToken cancellationToken)
        {

            Project? project = await _repository.GetDetailsById(request.Id,request.Userid);

            if (project == null) return ResultViewModel<ProjectViewModel>.Error("projeto não existe!");

            ProjectViewModel model = ProjectViewModel.fromEntity(project);
            return ResultViewModel<ProjectViewModel>.Success(model);
        }

    }
}
