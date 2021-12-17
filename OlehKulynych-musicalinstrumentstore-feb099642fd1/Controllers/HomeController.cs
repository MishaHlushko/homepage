using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Musical_Instrument_Store.Data.Models;

namespace Musical_Instrument_Store.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return RedirectToAction("Index", "MusicalInstrument");
        }
    }
}
