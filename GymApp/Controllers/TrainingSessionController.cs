using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymApp.Data;
using GymApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Controllers
{
    public class TrainingSessionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingSessionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string currentUserId = User.Identity.Name;

            var userSessions = _context.TraningSessions
                .Where(s => s.UserId == currentUserId)
                .Include(s => s.Training)  // Dodaj Include, aby załączyć dane treningów do sesji
                .ToList();

            return View(userSessions);
        }


        public IActionResult Create()
        {
            string currentUserId = User.Identity.Name;

            var userTrainings = _context.Trainings
                .Where(t => t.User.UserName == currentUserId)
                .ToList();

            ViewBag.TrainingId = new SelectList(userTrainings, "Id", "Name");

            var defaultSessionDate = DateTime.Now;
            var model = new TrainingSession { SessionDate = defaultSessionDate };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(TrainingSession trainingSession)
        {
            trainingSession.UserId = User.Identity.Name;

            if (trainingSession.SessionDate < DateTime.Now)
            {
                ModelState.AddModelError(nameof(trainingSession.SessionDate), "Data sesji musi być z przyszłości.");
            }

            _context.Add(trainingSession);
            await _context.SaveChangesAsync();

            var userTrainings = _context.Trainings
            .Where(t => t.User.UserName == trainingSession.UserId)
            .ToList();
            ViewBag.TrainingId = new SelectList(userTrainings, "Id", "Name", trainingSession.TrainingId);




            return RedirectToAction("Index", "TrainingSession");
            
        }



    }
}

