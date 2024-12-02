﻿namespace DevFreela.API.Models.project
{
    public class ProjectItemViewModel
    {
        public ProjectItemViewModel(int id, string title, string clientName, string freelancerName, decimal totalCost)
        {
            Id = id;
            Title = title;
            ClientName = clientName;
            FreelancerName = freelancerName;
            TotalCost = totalCost;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ClientName { get; set; }
        public string FreelancerName { get; set; }
        public decimal TotalCost { get; set; }
    }
}
