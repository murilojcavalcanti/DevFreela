using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Models.user;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Queries.QueriesUser.GetByIdUser
{
    public class GetByIdUserHandler : IRequestHandler<GetByIdUserQuery, ResultViewModel<UserViewModel>>
    {
        private readonly DevFreelaDbContext _context;

        public GetByIdUserHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<UserViewModel>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _context.Users
               .Include(u => u.Skills)
               .ThenInclude(u => u.Skill)
               .SingleOrDefaultAsync(u => u.Id == request.IdUser);
            
            if (user is null) return ResultViewModel<UserViewModel>.Error("Usuário não encontrado!");
            
            UserViewModel result = UserViewModel.FromEntity(user);
            
            return ResultViewModel<UserViewModel>.Success(result);
        }
    }
}
