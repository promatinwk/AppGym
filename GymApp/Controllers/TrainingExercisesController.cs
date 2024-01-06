using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymApp.Data;
using GymApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymApp.Controllers
{
    public class TrainingExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create(int trainingId)
        {
            ViewBag.Exercises = _context.Exercises.ToList();
            ViewBag.TrainingId = trainingId;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainingId,ExerciseId,SeriesCount")] TrainingExercises trainingExercises)
        {
            bool exerciseExists = _context.TrainingExercises
                .Any(te => te.TrainingId == trainingExercises.TrainingId && te.ExerciseId == trainingExercises.ExerciseId);
            if (!exerciseExists)
            {
                // Ćwiczenie nie istnieje, można dodać nowy wpis
                _context.TrainingExercises.Add(trainingExercises);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Ćwiczenie już istnieje, wykonaj odpowiednie kroki (np. wyświetl komunikat)
                ModelState.AddModelError("ExerciseId", "To ćwiczenie już istnieje w tym treningu.");
                // Możesz również ustawić ViewBag lub ViewData, aby przekazać komunikat do widoku
            }


            return RedirectToAction("Create", "TrainingExercises",new { trainingId = trainingExercises.TrainingId });

        }


        public IActionResult Edit(int id)
        {
            var trainingExercise = _context.TrainingExercises.Find(id);

            if (trainingExercise == null)
            {
                return NotFound();
            }

            return View(trainingExercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SeriesCount")] TrainingExercises trainingExercise)
        {
            if (id != trainingExercise.Id)
            {
                return NotFound();
            }
      
             // Update only the SeriesCount property
              var existingTrainingExercise = _context.TrainingExercises.Find(id);
                  existingTrainingExercise.SeriesCount = trainingExercise.SeriesCount;

                  _context.Update(existingTrainingExercise);
                  await _context.SaveChangesAsync();
               
              
                 return RedirectToAction("Details","Training", new { id = trainingExercise.TrainingId });
            

          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int trainingExerciseId)
        {
            var trainingExercise = await _context.TrainingExercises.FindAsync(trainingExerciseId);

            if (trainingExercise == null)
            {
                return NotFound();
            }

            _context.TrainingExercises.Remove(trainingExercise);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Training", new { id = trainingExercise.TrainingId });
        }



    }

}

