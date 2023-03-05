using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Models;
using Psinder.Services;


namespace Psinder.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly IMapper _mapper;

        public PostsController(ApplicationDbContext context,IFileStorage fileStorage ,IMapper mapper)
        {
            _context = context;
            _fileStorage = fileStorage;
            _mapper = mapper;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var posts = _context.Posts;

            var postsMappedAndOrdered = _mapper.Map<IEnumerable<PostDTO>>(posts).OrderByDescending(p=> p.Id).ToList();
            foreach(var post in postsMappedAndOrdered)
            {
                var imageFile = _fileStorage.GetImageFile(post.Id.ToString());
                post.Image = imageFile;
            }

            return View(postsMappedAndOrdered);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Name, Age, Size, Breed, Difficulty,Location,Description, ImagePath, ContactPhone, ContactEmail")] PostDTO postDto, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var post = _mapper.Map<Post>(postDto);
                _context.Add(post);
                await _context.SaveChangesAsync();

                var stream = image.OpenReadStream();
                byte[] buffer = new byte[stream.Length];
                stream.ReadAsync(buffer, 0, buffer.Length);
                _fileStorage.SaveFile(buffer, post.Id.ToString());

                return RedirectToAction(nameof(Index));
            }
            return View(postDto);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Name, Age, Size, Breed, Difficulty,Location,Description, ImagePath, ContactPhone, ContactEmail")] PostDTO post, IFormFile image)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return _context.Posts.Any(e => e.Id == id);
        }

        public FileContentResult GetImage(int postId)
        {
            byte[]? imageData = _fileStorage.GetImageFile(postId.ToString());

            if (imageData != null)
            {
                return File(imageData, "image/jpeg");
            }
            else
            {
                return null;
            }
        }
    }
}
