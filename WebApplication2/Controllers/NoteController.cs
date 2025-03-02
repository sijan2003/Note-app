using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models; // Adjust namespace as needed for MyNotes
using WebApplication2.Data; // Adjust namespace for ApplicationDbContext
using WebApplication2.Models; // Adjust namespace for User
using YourNotes.viewmodel;

namespace WebApplication2.Controllers
{
    [Authorize] // Ensures only authenticated users can access these actions
    public class NoteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public NoteController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Note/Index
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var notes = _context.MyNotes
                .Where(n => n.UserId == userId) // Fetch only notes for the current user
                .Select(n => new NoteViewModel
                {
                    Id = n.Id,
                    Title = n.Title,
                    Description = n.Description,
                    Color = n.Color // Include color if used in the view
                })
                .ToList();

            return View(notes);
        }

        // GET: /Note/Create
        public IActionResult Create()
        {
            return View(new NoteViewModel());
        }

        // POST: /Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Add for security
        public IActionResult Create(NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var note = new MyNotes
                {
                    Title = model.Title,
                    Description = model.Description,
                    Color = model.Color,
                    UserId = userId
                };
                _context.MyNotes.Add(note);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index)); // Redirect back to Index after saving
            }
            return View(model);
        }

        // GET: /Note/NewNote
        public IActionResult NewNote()
        {
            return View("Create", new NoteViewModel()); // Redirect to the Create view with a new NoteViewModel
        }

        // GET: /Note/Edit/5
        public IActionResult Edit(int id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var note = _context.MyNotes
                .FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
            {
                return NotFound(); // Return 404 if the note is not found
            }

            var viewModel = new NoteViewModel
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                Color = note.Color // Include color if used
            };

            return View(viewModel);
        }

        // POST: /Note/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var note = _context.MyNotes
                    .FirstOrDefault(n => n.Id == id && n.UserId == userId);

                if (note == null)
                {
                    return NotFound(); // Return 404 if the note is not found
                }

                // Update the note properties
                note.Title = model.Title;
                note.Description = model.Description;
                note.Color = model.Color;

                _context.MyNotes.Update(note);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index)); // Redirect to Index after successful update
            }

            // If validation fails, return to the Edit view with the model
            return View(model);
        }

        // GET: /Note/Delete/5
        public IActionResult Delete(int id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var note = _context.MyNotes
                .FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
            {
                return NotFound();
            }

            var viewModel = new NoteViewModel
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                Color = note.Color // Include color if used
            };

            return View(viewModel);
        }

        // POST: /Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var note = _context.MyNotes
                .FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
            {
                return NotFound();
            }

            _context.MyNotes.Remove(note);
            _context.SaveChanges();

            // Return to the Index action to refresh the page without the deleted note
            return RedirectToAction(nameof(Index));
        }
    }
}