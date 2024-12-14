using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Notification.ProjectNotification
{
    public class FreelanceNotificationHandler : INotificationHandler<ProjectCreatedNotification>
    {
        Task INotificationHandler<ProjectCreatedNotification>.Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Noticando frelancers: Projeto criado ");
            return Task.CompletedTask;
        }
    }
}
