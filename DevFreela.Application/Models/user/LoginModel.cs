namespace DevFreela.Application.Models.user
{
    public class LoginInputModel
    {
        public LoginInputModel(string email, string token)
        {
            Email = email;
            Token = token;
        }

        public string Email { get; set; }
        public string Token { get; set; }
    }

    public class loginViewModel
    {
        public string Token { get; set; }

        public loginViewModel(string token)
        {
            Token = token;
        }
    }

}