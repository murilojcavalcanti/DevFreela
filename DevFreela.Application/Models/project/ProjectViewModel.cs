using DevFreela.Core.Entities;

namespace DevFreela.Application.Models.project
{
    public class ProjectViewModel
    {
        public ProjectViewModel(int Id,string Title,string description, int idClient, int idFreelancer, string clientName, string freelancerName, decimal totalCost,List<ProjectComment> projectComments)
        {
            this.Id = Id;
            this.Title = Title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            ClientName = clientName;
            FreelancerName = freelancerName;
            TotalCost = totalCost;
            Comments = projectComments?.Select(c => c.Content).ToList();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdClient { get; set; }
        public int IdFreelancer { get; set; }
        public string ClientName { get; set; }
        public string FreelancerName { get; set; }
        public decimal TotalCost { get; set; }
        public List<string>? Comments { get; set; }

        public static ProjectViewModel fromEntity(Project entity)
            => new(entity.Id, entity.Title, entity.Description,
                entity.IdClient, entity.IdFreelancer, entity.Client.FullName,
                entity.Freelancer.FullName, entity.TotalCost, entity.Comments);
            
    }
}
