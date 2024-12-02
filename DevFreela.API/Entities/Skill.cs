namespace DevFreela.API.Entities
{
    public class Skill
    {
        public Skill(string description)
        {
            Description = description;
        }
        public string Description { get; private set; }


        public int IdFrelancer { get; private set; }

    }
}
