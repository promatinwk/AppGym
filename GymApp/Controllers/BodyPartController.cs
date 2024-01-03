using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymApp.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymApp.Controllers
{
    public class BodyPartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BodyPartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bodyParts = _context.BodyParts.ToList();
            return View(bodyParts);
        }
    }
}

