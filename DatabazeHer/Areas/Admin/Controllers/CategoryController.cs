using DataHer.Dao;
using DataHer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatabazeHer.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "spravce")]
        public ActionResult CreateCategories()
        {
            GameCategoryDao gameCategoryDao = new GameCategoryDao();
            IList<GameCategory> categories = gameCategoryDao.GetAll();
            ViewBag.Categories = categories;

            return View("CreateCategories");
        }
        [HttpPost]
        [Authorize(Roles = "spravce")]
        public ActionResult Add(GameCategory gameCategory)
        {
            if (ModelState.IsValid)
            {

                GameCategoryDao gameCategoryDao = new GameCategoryDao();
       
                gameCategoryDao.Create(gameCategory);

                TempData["message-success"] = "Žánr byl úspěšně vytvořen";

            }
            else
            {
                return View("Create", gameCategory);
            }



            return RedirectToAction("Index", "Games");
        }

        [Authorize(Roles = "spravce")]
           public ActionResult EditCategory()
           {
               GameCategoryDao gameCategoryDao = new GameCategoryDao();
         
               return View("EditCategory");
           }
        [Authorize(Roles = "spravce")]
        [HttpPost]
        public ActionResult Update(GameCategory gameCategory)
        {
            try
            {
                GameCategoryDao gameCategoryDao = new GameCategoryDao();
         

                gameCategoryDao.Update(gameCategory);

                TempData["message-success"] = "Žánr" + gameCategory.Name + "byl upraven";

            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Index", "Games");
        }

    /*    [Authorize(Roles = "spravce")]
        public ActionResult Delete(int id)
        {
            try
            {
                GameCategoryDao gameDao = new GameCategoryDao();
                GameCategory gameCategory = gameCategoryDao.GetById(id);


                gameCategoryDao.Delete(gameCategory);

                TempData["message-success"] = "Hra " + gameCategory.Name + " byla smazána";

            }
            catch (Exception exception)
            {
                throw;
            }

            return RedirectToAction("Index");

        } */
    }
}