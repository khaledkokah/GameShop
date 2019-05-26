using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameShop.Models;
using GameShop.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;
using GameShop.Extra;

namespace Vidly.Controllers
{
    public class GamesController : Controller
    {
        ApplicationDbContext _context;

        public GamesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Games
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.Admin))
                return View("List");

            return View("ReadOnlyList");
        }

        //  [Authorize(Roles=RoleName.Admin)]
        [CustomAuthorize(Roles = RoleName.Admin)]
        public ActionResult New()
        {
            var categories = _context.Categories.ToList();

            var viewModel = new GameFormViewModel
            {
                Categories = categories
            };

            return View("GameForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var game = _context.Games.SingleOrDefault(c => c.Id == id);

            if (game == null)
                return HttpNotFound();

            var viewModel = new GameFormViewModel(game)
            {
                Categories = _context.Categories.ToList()
            };

            return View("GameForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Game game)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new GameFormViewModel(game)
                {
                    Categories = _context.Categories.ToList()
                };

                return View("GameForm", viewModel);
            }

            if (game.Id == 0)
            {
                //Insert
                game.ReleaseDate = DateTime.Now;
                game.AddDate = DateTime.Now;
                _context.Games.Add(game);
            }
            else
            {
                //Update
                var gameInDb = _context.Games.Single(m => m.Id == game.Id);
                gameInDb.Name = game.Name;
                gameInDb.ReleaseDate = game.ReleaseDate;
                gameInDb.CategoryId = game.CategoryId;
                gameInDb.NumberInStock = game.NumberInStock;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Console.WriteLine(ex);
            }


            return RedirectToAction("Index", "Games");
        }

        public ActionResult Details(int id)
        {
            var game = _context.Games.Include(m => m.Category).SingleOrDefault(m => m.Id == id);
            return View(game);
        }

        [Route("games/released/{year}/{month:regex(\\d{4}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}