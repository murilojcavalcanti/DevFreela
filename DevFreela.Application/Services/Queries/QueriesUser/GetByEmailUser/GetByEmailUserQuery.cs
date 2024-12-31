using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Services.Queries.QueriesUser.GetByEmailUser
{
    public class GetByEmailUserQuery : IRequest<ResultViewModel<User>>
    {

        public string EmailUser { get; set; }

        public GetByEmailUserQuery( string emailUser)
        {
            EmailUser = emailUser;
        }
    }
}
