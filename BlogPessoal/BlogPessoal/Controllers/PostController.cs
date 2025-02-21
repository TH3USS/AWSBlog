using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var posts = _context.Posts.OrderByDescending(p => p.DataPublicacao).ToList();
            return View(posts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post post, IFormFile imagem)
        {
            if (ModelState.IsValid)
            {
                // Upload da Imagem (Azure Blob Storage ou AWS S3)
                if (imagem != null)
                {
                    var fileName = Path.GetFileName(imagem.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imagem.CopyTo(stream);
                    }
                    post.ImagemUrl = "/uploads/" + fileName;
                }

                _context.Posts.Add(post);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }
    }

}
