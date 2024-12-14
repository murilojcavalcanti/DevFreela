using DevFreela.Application.Notification.ProjectNotification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Notification.ProjectCreated
{
    internal class GenerateBoardHandler : INotificationHandler<ProjectCreatedNotification>
    {
        public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Criando painel para projeto {notification.Title}");
            return Task.CompletedTask;
        }
    }
}
