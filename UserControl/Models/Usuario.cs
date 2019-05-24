namespace UserControl.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public Perfil Perfil { get; set; }
        public bool Estado { get; set; }

        public Usuario (string login, string senha)
        {
            Login = login;
            Senha = senha;
        }

        public bool ValidaLogin()
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Senha))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
