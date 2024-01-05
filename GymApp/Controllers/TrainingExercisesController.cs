using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



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
            var training = _context.Trainings.Find(trainingId);

            if (training == null)
            {
                return NotFound();
            }

            ViewBag.Exercises = _context.Exercises.ToList();
            ViewBag.TrainingId = trainingId;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int trainingId, int[] selectedExerciseIds, int[] seriesCounts)
        {
            var training = _context.Trainings.Include(t => t.User).FirstOrDefault(t => t.Id == trainingId);

            if (training == null)
            {
                return NotFound();
            }

            if (selectedExerciseIds != null && selectedExerciseIds.Length > 0)
            {
                for (int i=0; i<selectedExerciseIds.Length; i++)
                {
                    _context.TrainingExercises.Add(new Models.TrainingExercises
                    {
                        TrainingId = trainingId,
                        ExerciseId = selectedExerciseIds[i],
                        SeriesCount = seriesCounts[i]
                    }) ;
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Training");

        }

    }

}

