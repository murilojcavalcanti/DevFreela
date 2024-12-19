using DevFreela.Application.Models;
using DevFreela.Infrastructure.Models.user;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Queries.QueriesUser.GetByIdUser
{
    public class GetByIdUserQuery : IRequest<ResultViewModel<UserViewModel>>
    {

        public int IdUser { get; set; }

        public GetByIdUserQuery(int idUser)
        {
            IdUser = idUser;
        }
    }
}
