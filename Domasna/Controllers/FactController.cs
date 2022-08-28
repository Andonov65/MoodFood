using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domasna.Controllers
{
    public class FactController : Controller
    {
        public IActionResult Facts()
        {
            return View();
        }
    }
}
