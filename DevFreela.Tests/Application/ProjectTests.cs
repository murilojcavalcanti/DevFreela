using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Tests.Application
{
    public class ProjectTests
    {
        [Fact]
        public void ProjectIsCreated_Start_Sucess()
        {
            //Arrange
            Project project = new("Projeto","Descrição",1,2,1500);

            //Act
            project.Start();

            //Assert
            Assert.Equal(ProjectStatusEnum.InProgress,project.Status);
            Assert.NotNull(project.StartedAt);
        }

        [Fact]
        public void ProjectIsCreated_PedingPayment_Sucess()
        {
            //Assert
            Project project = new("Projeto", "Descrição", 1, 2, 1500);
            project.Start();

            //Act
            project.SetPaymentPending();

            //Assert
            Assert.Equal(ProjectStatusEnum.peymentPending, project.Status);
        }

        [Fact]
        public void ProjectIsCreated_Deleted_Sucess()
        {
            //Assert
            Project project = new("Projeto", "Descrição", 1, 2, 1500);

            //Act
            project.SetAsDeleted();

            //Assert
            Assert.True(project.IsDeleted);
        }

        [Fact]
        public void ProjectIsCreated_Complete_Sucess()
        {
            //Assert
            Project project = new("Projeto", "Descrição", 1, 2, 1500);
            project.Start();

            //Act
            project.Complete();

            //Assert
            Assert.Equal(ProjectStatusEnum.Completed,project.Status);
            Assert.NotNull(project.CompletedAt);
        }
        [Fact]
        public void ProjectIsCreated_Canceled_Sucess()
        {
            //Assert
            Project project = new("Projeto", "Descrição", 1, 2, 1500);
            project.Start();
            //Act
            project.Cancel();

            //Assert
            Assert.Equal(ProjectStatusEnum.cancelled, project.Status);
        }

        [Fact]
        public void ProjectIsInvalidState_Start_Exception()
        {
            //Assert
            Project project = new("Projeto", "Descrição", 1, 2, 1500);
            project.Start();

            //Act
            Action? start = project.Start;

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(start);
            Assert.Equal(Project.INVALID_STATE_MESSAGE, ex.Message);

        }
    }
}
