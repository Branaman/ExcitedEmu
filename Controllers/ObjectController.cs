using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ExcitedEmu.Models;
using ExcitedEmu.Factories;
namespace ExcitedEmu.Controllers
{
    public class ObjectsController : Controller
    {
        private readonly ObjectFactory ObjectFactory;
        public ObjectsController(ObjectFactory connect)
        {
            ObjectFactory = connect;
        }
        // Load Objects Page
        [HttpGet]
        [Route("/Objects")]
        public IActionResult Objects()
        {
            if (HttpContext.Session.GetString("loggedIn")=="true")
            {
                ViewBag.loggedIn = true;
                ViewBag.userID = HttpContext.Session.GetInt32("userID");
                ViewBag.AllObjects = ObjectFactory.GetAllObjects();
                ViewBag.errors = "";
                return View();
            }
            return RedirectToAction("Index","Home");
        }
        // Add New Object
        [HttpPost]
        [Route("/addObject")]
        public IActionResult AddObject(Object Object)
        {   
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("loggedIn")=="true")
                {
                    ObjectFactory.AddObject(Object);
                }
            }
            ViewBag.errors = ModelState.Values;
            return RedirectToAction("Objects");
        }
    }
}
