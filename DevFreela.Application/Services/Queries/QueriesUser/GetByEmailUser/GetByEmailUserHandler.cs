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

namespace DevFreela.Application.Services.Queries.QueriesUser.GetByEmailUser
{
    public class GetByEmailUserHandler : IRequestHandler<GetByEmailUserQuery, ResultViewModel<User>>
    {
        private readonly DevFreelaDbContext _context;

        public GetByEmailUserHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<User>> Handle(GetByEmailUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _context.Users
               .Include(u => u.Skills)
               .ThenInclude(u => u.Skill)
               .SingleOrDefaultAsync(u => u.Email == request.EmailUser);
            
            if (user is null) return ResultViewModel<User>.Error("Usuário não encontrado!");
            
            
            return ResultViewModel<User>.Success(user);
        }
    }
}
