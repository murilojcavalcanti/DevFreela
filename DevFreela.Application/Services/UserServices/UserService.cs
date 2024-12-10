using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Models.Skills;
using DevFreela.Infrastructure.Models.user;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _context;

        public UserService(DevFreelaDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<List<UserViewModel>> GetAll(string search = "", int size = 5, int page = 0)
        {
            var users = _context.Users
             .Include(u => u.Skills)
             .ThenInclude(u => u.Skill)
             .Skip(page*size)
             .Take(size)
             .ToList();
            if (users is null) return ResultViewModel<List<UserViewModel>>.Error("Não existem projetos");
            List<UserViewModel> result = users.Select(u => UserViewModel.FromEntity(u)).ToList() ;
            return ResultViewModel<List<UserViewModel>>.Success(result);
        }

        public ResultViewModel<UserViewModel> GetById(int id)
        {
            User? user = _context.Users
                .Include(u => u.Skills)
                .ThenInclude(u => u.Skill)
                .SingleOrDefault(u => u.Id == id);
            if (user is null) return ResultViewModel<UserViewModel>.Error("Usuário não encontrado!");
            UserViewModel result = UserViewModel.FromEntity(user);
            return ResultViewModel<UserViewModel>.Success(result);
        }

        public ResultViewModel<int> Insert(CreateUserInputModel model)
        {
            User user = model.ToEntity();
            _context.Users.Add(user);
            _context.SaveChanges();
            return ResultViewModel<int>.Success(user.Id);
        }

        public ResultViewModel InsertSkills(int id, UserSkillsInputModel model)
        {
            var UserSkills = model.SkillIds.Select(s => new UserSkill(id, s)).ToList();
            _context.UserSkills.AddRange(UserSkills);
            return ResultViewModel.Success();
        }
    }
}
