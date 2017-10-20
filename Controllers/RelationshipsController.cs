using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ExcitedEmu.Models;
using ExcitedEmu.Factories;
namespace ExcitedEmu.Controllers
{
    public class RelationshipController : Controller
    {
        private readonly RelationshipFactory RelationshipFactory;
        public RelationshipController(RelationshipFactory connect)
        {
            RelationshipFactory = connect;
        }
        // Load Relationships Page
        [HttpGet]
        [Route("/Relationships")]
        public IActionResult Relationships()
        {
            if (HttpContext.Session.GetString("loggedIn")=="true")
            {
                ViewBag.loggedIn = true;
                ViewBag.userID = HttpContext.Session.GetInt32("userID");
                ViewBag.AllRelationships = RelationshipFactory.GetRelationships();
                ViewBag.errors = "";
                return View();
            }
            return RedirectToAction("Index","Home");
        }
        // Add New Product
        [HttpPost]
        [Route("/addorder")]
        public IActionResult AddOrder(Order Order)
        {   
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("loggedIn")=="true")
                {
                    ViewBag.loggedIn = true;
                    RelationshipFactory.AddOrder(Order);
                }
                else
                {
                    ViewBag.loggedIn = false;
                    ModelState.AddModelError("noLogin","You must be logged in to create an error");
                }
            }
            ViewBag.AllRelationships = RelationshipFactory.GetRelationships();
            ViewBag.errors = ModelState.Values;
            return View("Relationships");
        }
    }
}
