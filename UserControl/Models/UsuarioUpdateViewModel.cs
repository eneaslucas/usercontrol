namespace UserControl.Controllers
{
    public class UsuarioUpdateViewModel
    {
        public int id { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public int perfilId { get; set; }
        public string perfilNome { get; set; }
    }
}