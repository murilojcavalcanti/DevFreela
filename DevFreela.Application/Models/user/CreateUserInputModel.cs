﻿using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Models.user
{
    public class CreateUserInputModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public User ToEntity()=>new User(FullName,Email,BirthDate);
    }
}
