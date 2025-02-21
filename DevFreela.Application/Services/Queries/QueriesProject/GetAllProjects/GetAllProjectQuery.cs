﻿using DevFreela.Application.Models;
using DevFreela.Application.Models.project;
using DevFreela.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Queries.QueriesProject.GetAllProjects
{
    public class GetAllProjectQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
    {
        public int UserId { get; set; }

        public GetAllProjectQuery(int userId)
        {
            UserId = userId;
        }
    }
}
