using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Psinder.Data;
using Psinder.Models;
using Psinder.Services;
using System.Diagnostics;
using Psinder.Services;

namespace Psinder.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly IMapper _mapper;

        public HomeController(/*Logger<HomeController> logger, */ApplicationDbContext context, IFileStorage fileStorage, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _fileStorage = fileStorage;
            //_logger = logger;
        }

        public IActionResult Index()
        {
            List<PostDTO> random3Posts = new List<PostDTO>();
            var posts = _mapper.Map<IEnumerable<PostDTO>>(_context.Posts).ToList();

            Random r = new Random();
            List<int> idsUsed = new List<int>();
            while (random3Posts.Count() < 3)
            {
                var rId = r.Next(0, posts.Count() - 1);
                if (!idsUsed.Contains(rId))
                {
                    random3Posts.Add(posts[rId]);
                    idsUsed.Add(rId);
                }
            }

            return View(random3Posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}