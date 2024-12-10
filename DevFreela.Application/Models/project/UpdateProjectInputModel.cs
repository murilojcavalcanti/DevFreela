namespace DevFreela.Application.Models.project
{
    public class UpdateProjectInputModel
    {

        public int IdProject { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
    }
}
