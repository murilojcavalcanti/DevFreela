﻿namespace DevFreela.Infrastructure.Models.projectComments
{
    public class CreateProjectCommentInputModel
    {
        public string Content { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }
    }
}
