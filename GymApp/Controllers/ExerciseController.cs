using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymApp.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ApplicationDbContext _context;
        // GET: /<controller>/

        public ExerciseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? bodyPartId)
        {

            var exercises = await _context.Exercises
                .Include(e => e.BodyPart)
                .Include(e => e.RecordHolder)
                .ToListAsync();

            if (bodyPartId.HasValue)
            {
                exercises = exercises.Where(e => e.BodyPartId == bodyPartId).ToList();
            }

            return View(exercises);
        }
    }
}

