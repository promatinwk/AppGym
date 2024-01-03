using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

            // Dodajemy dostępne ćwiczenia do ViewBag.Exercises
            ViewBag.Exercises = _context.Exercises.ToList();

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Training training)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                training.UserId = userId;
                training.CreateDate = DateTime.Now;

                _context.Add(training);
                await _context.SaveChangesAsync();

                // Sprawdzenie, czy training.TrainingExercises nie jest nullem
                if (training.TrainingExercises != null)
                {
                    // Dodawanie TrainingExercises do bazy danych
                    foreach (var exercise in training.TrainingExercises)
                    {
                        // Sprawdzenie, czy exercise nie jest nullem
                        if (exercise != null)
                        {
                            _context.TrainingExercises.Add(new TrainingExercises
                            {
                                TrainingId = training.Id,
                                ExerciseId = exercise.ExerciseId,
                                SeriesCount = exercise.SeriesCount
                            });
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Exercises = ViewBag.Exercises ?? new List<Exercise>();
            //ViewBag.Exercises = await _context.Exercises.ToListAsync();
            return View(training);
        }

    }
}

