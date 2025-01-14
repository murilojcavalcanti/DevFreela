using Azure;
using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Models.user;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Queries.QueriesUser.GetAllUsers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ResultViewModel<List<UserViewModel>>>
    {
        private readonly DevFreelaDbContext _context;

        public GetAllUsersHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<UserViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await  _context.Users
            .Include(u => u.Skills)
            .ThenInclude(u => u.Skill)
            .Where(u=>u.IsDeleted==false)
            .ToListAsync();
            List<UserViewModel> result = users.Select(u => UserViewModel.FromEntity(u)).ToList();
            return ResultViewModel<List<UserViewModel>>.Success(result);
        }
    }
}
