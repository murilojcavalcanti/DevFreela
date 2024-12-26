﻿using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Commands.CommandSkill.InsertSkill
{
    public class InsertSkillCommand : IRequest<ResultViewModel<int>>
    {
        public InsertSkillCommand(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
        public Skill ToEntity() => new Skill(Description);
    }
}
