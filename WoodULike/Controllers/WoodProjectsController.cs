using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WoodULike.DAL;
using WoodULike.Models;
using WoodULike.ViewModels;
using WoodULike.Extensions;
using PagedList;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;

namespace WoodULike.Controllers
{
    public class WoodProjectsController : Controller
    {
        private WoodULikeDbContext db = new WoodULikeDbContext();

        // GET: WoodProjects
        public ActionResult Index(string searchString, string searchProjectType, int? page)
        {
            int pageSize = 5;
            int pageIndex = 1;
            IQueryable<WoodProject> result;
            SelectList pTypes = new SelectList(new WoodProject().ProjectTypes);

            ViewBag.ProjectTypes = pTypes;
            try
            {
                result = db.WoodProjects.OrderByDescending(x => x.PublishDate).Include(x => x.ApplicationUser);
                if (searchString != null)
                {
                    result = db.WoodProjects.Where(x => x.ProjectTitle.Contains(searchString));
                }
            }
            catch (RetryLimitExceededException dex)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return View("Error", new HandleErrorInfo(dex, "WoodProject", "Index"));
            }
            return View(result.ToPagedList(pageIndex, pageSize));

            //int pageSize = 5;
            //int pageIndex = 1;
            ////var viewModel = null;
            //IQueryable<WoodProject> result;

            //try
            //{
                

            //    pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            //    var viewModel = db.WoodProjects.OrderByDescending(x => x.PublishDate).Include(x => x.ApplicationUser);

            //    SelectList pTypes = new SelectList(new WoodProject().ProjectTypes);

            //    ViewBag.ProjectTypes = pTypes;

            //    if (!String.IsNullOrEmpty(searchString))
            //    {
            //        viewModel = viewModel.Where(s =>
            //        s.ProjectTitle.ToLower().Contains(searchString.ToLower()) ||
            //        s.ProjectType.ToLower().Contains(searchString.ToLower()) ||
            //        s.Description.ToLower().Contains(searchString.ToLower())
            //        );
            //    }

            //    if (!String.IsNullOrEmpty(searchProjectType))
            //    {
            //        viewModel = viewModel.Where(s => s.ProjectType.Contains(searchProjectType));
            //    }

            //    result = viewModel;
                

            //}
            //catch (RetryLimitExceededException dex)
            //{
            //    //Can log error from dex here
            //    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            //    return View("Error", new HandleErrorInfo(dex, "WoodProject", "Index"));
            //}
            //return View(result.ToPagedList(pageIndex, pageSize));
        }

        public ActionResult MyWoodProjects(string searchString)
        {
            var user = User.Identity.GetUserId();
            var woodProjects = from m in db.WoodProjects
                               where m.UserId == user
                               select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                woodProjects = woodProjects.Where(s => s.ProjectTitle.Contains(searchString) || s.ProjectType.ToString().Contains(searchString) || s.Description.Contains(searchString));
            }

            return View(woodProjects);
        }

        // GET: WoodProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WoodProject woodProject = db.WoodProjects.Find(id);
            if (woodProject == null)
            {
                return HttpNotFound();
            }
            return View(woodProject);
        }

        // GET: WoodProjects/Create
        public ActionResult Create()
        { 
            bool isUserLoggedIn = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (isUserLoggedIn)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: WoodProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProjectTitle,ImageURL,Description,ProjectType")] WoodProject woodProject, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Wood_Project_Images"), fileName);
                        woodProject.ImageURL = path.Substring(path.IndexOf("Content"));

                        file.SaveAs(path);
                    }

                    var user = User.Identity.GetUserId();
                    woodProject.UserId = user;
                    woodProject.PublishDate = DateTime.Now;
                    db.WoodProjects.Add(woodProject);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            } catch (RetryLimitExceededException dex)
            {
                //Can log dex exception here
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return View("Error", new HandleErrorInfo(dex, "WoodProject", "Index"));
            }


            return View(woodProject);
        }

        // GET: WoodProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WoodProject woodProject = db.WoodProjects.Find(id);
            if (woodProject == null)
            {
                return HttpNotFound();
            }
            return View(woodProject);
        }

        // POST: WoodProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProjectTitle,ImageURL,Description,PublishDate,ProjectType")] WoodProject woodProject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(woodProject).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(woodProject);
            }
            catch (RetryLimitExceededException dex)
            {
                //Can log dex exception here
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return View("Error", new HandleErrorInfo(dex, "WoodProject", "Index"));
            }
        }

        // GET: WoodProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WoodProject woodProject = db.WoodProjects.Find(id);
            if (woodProject == null)
            {
                return HttpNotFound();
            }
            return View(woodProject);
        }

        // POST: WoodProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WoodProject woodProject = db.WoodProjects.Find(id);
            db.WoodProjects.Remove(woodProject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
