using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication2.Models;

namespace MvcApplication2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        dbEntities db = new dbEntities();

        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<MvcApplication2.Models.Commets> comms = db.Commets;
            IEnumerable<MvcApplication2.Models.Poems> poems = db.Poems;

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ViewBag.Poems = poems;
            ViewBag.Comm = comms;
            
            return View();
        }

        [HttpPost]
        [Authorize(Users = "leksi")]
        public ActionResult CreatePoem(Models.Poems entry)
        {
            entry.data = DateTime.Now;
            db.Poems.Add(entry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Users = "leksi")]
        public ActionResult DeletePoem(int id)
        {
            var poem = db.Poems.Find(id);
            db.Poems.Remove(poem);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult CreateMsg(Models.Commets entry)
        {
            entry.data = DateTime.Now;
            entry.username = User.Identity.Name;
            db.Commets.Add(entry);
            db.SaveChanges();

            return RedirectToAction("ViewPoem", new { poem = entry.poem_id });
        }
        [HttpGet]
        public ActionResult DeleteComment(int id,int poem_id)
        {
            var comment = db.Commets.Find(id);
            db.Commets.Remove(comment);
            db.SaveChanges();

            return RedirectToAction("ViewPoem", new { poem = poem_id });
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ViewPoem(int poem)
        {
            IEnumerable<MvcApplication2.Models.Commets> comms = db.Commets;
            IEnumerable<MvcApplication2.Models.Poems> poems = db.Poems;

            ViewBag.Poems = poems;
            ViewBag.Comm = comms;

            ViewBag.id = poem;

            return View(poem);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Кое что об этом";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Мои контакты";

            return View();
        }
    }
}
