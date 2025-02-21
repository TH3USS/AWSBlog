namespace BlogPessoal.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string ImagemUrl { get; set; }
        public DateTime DataPublicacao { get; set; } = DateTime.Now;
    }

}
