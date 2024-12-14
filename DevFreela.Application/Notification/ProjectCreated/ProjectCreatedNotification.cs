using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Notification.ProjectNotification
{
    internal class ProjectCreatedNotification:INotification
    {
        public ProjectCreatedNotification(int idProject, string title, decimal totalCost)
        {
            IdProject = idProject;
            Title = title;
            TotalCost = totalCost;
        }

        public int IdProject { get; set; }
        public string Title { get; set; }
        public decimal TotalCost { get; set; }
    }
}
