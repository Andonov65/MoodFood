using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domasna.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
