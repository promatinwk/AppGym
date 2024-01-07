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
        public async Task<IActionResult> Create(TrainingSession trainingSession)
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


        [HttpGet]
        public IActionResult ConfigureSession(int sessionId)
        {
            var session = _context.TraningSessions.FirstOrDefault(ts => ts.Id == sessionId);

            if (session == null)
            {
                return NotFound();
            }
            int trainingId = session.TrainingId;

            // Sprawdź, czy istnieją już rekordy dla danej sesji
            bool recordsExist = _context.WeightRecords.Any(wr => wr.TrainingSessionId == sessionId);

            if (recordsExist)
            {
                TempData["SessionId"] = sessionId;
                return View("Error");
            }

            ViewBag.Exercises = _context.TrainingExercises
                .Include(te => te.Exercise)
                .Where(te => te.TrainingId == _context.TraningSessions.FirstOrDefault(ts => ts.Id == sessionId).TrainingId)
                .ToList();


            ViewBag.SessionId = sessionId;
            //ViewBag.Exercises = exercises;

            return View();
        }

        // Akcja do obsługi przesłanych danych
        [HttpPost]
        public async Task<IActionResult> ConfigureSession(int sessionId, List<WeightRecord> weightRecords)
        {
            foreach (var record in weightRecords)
            {
                record.TrainingSessionId = sessionId;
                // Dodajemy do bazy danych
                _context.WeightRecords.Add(record);

            }
         
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult SessionDetails(int sessionId)
        {
            // Pobierz wszystkie rekordy dla danej sesji
            var weightRecords = _context.WeightRecords
                .Include(wr => wr.Exercise) 
                .Where(wr => wr.TrainingSessionId == sessionId)
                .ToList();

            ViewBag.SessionId = sessionId;
            // Dictionary przechowujące najwyższe wagi dla danego ExerciseId i odpowiadające nazwy użytkowników
      
            return View(weightRecords);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var trainingSession = await _context.TraningSessions
                .FirstOrDefaultAsync(ts => ts.Id == id);

            if (trainingSession == null)
            {
                return NotFound();
            }

            // Usuń powiązane WeightRecord
            var weightRecords = _context.WeightRecords
                .Where(wr => wr.TrainingSessionId == id)
                .ToList();

            _context.WeightRecords.RemoveRange(weightRecords);

            // Usuń TrainingSession
            _context.TraningSessions.Remove(trainingSession);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "TrainingSession");
        }



    }
}

