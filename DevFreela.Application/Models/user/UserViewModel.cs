using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Models.user
{
    public class UserViewModel
    {
        public UserViewModel(string fullName, string email, DateTime birthDate, List<string> skills, string role)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Skills = skills;
            Role = role;
        }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Role { get; set; }
        public List<string> Skills { get;private set; }
        public static UserViewModel FromEntity(User user)
        {
            var skills = user.Skills.Select(s => s.Skill.Description).ToList();
            return new UserViewModel(user.FullName, user.Email, user.BirthDate,skills,user.Role );
        }
    }
}
