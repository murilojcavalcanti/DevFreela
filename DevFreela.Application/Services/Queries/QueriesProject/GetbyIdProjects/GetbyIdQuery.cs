using DevFreela.Application.Models;
using DevFreela.Application.Models.project;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Queries.QueriesProject.GetbyIdProjects
{
    public class GetbyIdQuery:IRequest<ResultViewModel<ProjectViewModel>>
    {
        public int Id { get; set; }

        public GetbyIdQuery(int id)
        {
            Id = id;
        }
    }
}
