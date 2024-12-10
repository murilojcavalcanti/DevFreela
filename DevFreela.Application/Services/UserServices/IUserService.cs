using DevFreela.Application.Models;
using DevFreela.Infrastructure.Models.Skills;
using DevFreela.Infrastructure.Models.user;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.UserServices
{
    public interface IUserService
    {
        ResultViewModel<int> Insert(CreateUserInputModel model);
        ResultViewModel<List<UserViewModel>> GetAll(string search = "",int size = 5,int page = 0);
        ResultViewModel<UserViewModel> GetById(int model);
        ResultViewModel InsertSkills(int id, UserSkillsInputModel model);
    }
}
