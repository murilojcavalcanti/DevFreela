using DevFreela.Application.Models;
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
        public GetAllProjectQuery(string search, int size, int page)
        {
            Search = search;
            Size = size;
            Page = page;
        }

        public string Search { get; set; }
        public int Size { get; set; }
        public int Page { get; set; }

    }
}
