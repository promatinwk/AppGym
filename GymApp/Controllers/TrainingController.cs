using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using GymApp.Data;
using GymApp.Models;

namespace GymApp.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public TrainingController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainings = await _context.Trainings
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return View(trainings);
        }
        
        public IActionResult Create()
        { 
            var viewModel = new Training();
            viewModel.TrainingExercises = new List<TrainingExercises>(); // Inicjalizacja listy

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Training training)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            training.UserId = userId;
            training.CreateDate = DateTime.Now;

            _context.Add(training);
            await _context.SaveChangesAsync();

  
            return RedirectToAction(nameof(Index));

              
        }


         public IActionResult Details(int id)
         {
            var trainingExercises = _context.TrainingExercises
                .Include(te => te.Exercise)
                .Where(te => te.TrainingId == id)
                .ToList();

                 ViewBag.TrainingId = id; // Przekazujemy id treningu do widoku, aby można było dodać nowe ćwiczenia

                 return View(trainingExercises);
         }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var training = await _context.Trainings
                        .Include(t => t.TrainingExercises)
                        .FirstOrDefaultAsync(t => t.Id == id);

            if (training == null)
            {
                return NotFound();
            }

            // Usuwanie powiązanych TrainingExercises
            _context.TrainingExercises.RemoveRange(training.TrainingExercises);

            _context.Trainings.Remove(training);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteExercise(int trainingId, int exerciseId)
            {
                var trainingExercise = _context.TrainingExercises
                    .FirstOrDefault(te => te.TrainingId == trainingId && te.ExerciseId == exerciseId);

                if (trainingExercise == null)
                {
                    return NotFound();
                }

                _context.TrainingExercises.Remove(trainingExercise);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id = trainingId });
            }
        

       












    }
}