using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandUser.UpdateUser
{
    public class UpdateUserCommand : IRequest<ResultViewModel>
    {
        public UpdateUserCommand(int userId, string fullName, string email, DateTime birthDate)
        {
            UserId = userId;
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public User ToEntity() => new User(FullName, Email, BirthDate);

    }
}
