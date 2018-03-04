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
            ViewBag.ProjectTypes = new SelectList(new WoodProject().ProjectTypes);

            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            try
            {
                result = db.WoodProjects.OrderByDescending(x => x.PublishDate).Include(x => x.ApplicationUser);
                if (!String.IsNullOrEmpty(searchString))
                {
                    result = db.WoodProjects.Where(x => x.ProjectTitle.ToLower().Contains(searchString.ToLower())
                        || x.ProjectType.ToLower().Contains(searchString.ToLower()) 
                        || x.Description.ToLower().Contains(searchString.ToLower()
                        ));
                }

                if (!String.IsNullOrEmpty(searchProjectType))
                {
                    result = result.Where(x => x.ProjectType.Equals(searchProjectType));
                }

                result = result.OrderByDescending(x => x.PublishDate);
            }
            catch (RetryLimitExceededException dex)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return View("Error", new HandleErrorInfo(dex, "WoodProject", "Index"));
            }
            return View(result.ToPagedList(pageIndex, pageSize));
        }

        public ActionResult MyWoodProjects(string searchString, string searchProjectType)
        {
            var user = User.Identity.GetUserId();
            
            var userWoodProjects = db.WoodProjects.Where(x => x.UserId == user);

            ViewBag.SearchProjectType = new SelectList(new WoodProject().ProjectTypes);

            if (!String.IsNullOrEmpty(searchString))
            {
                userWoodProjects = userWoodProjects.Where(s => s.ProjectTitle.Contains(searchString) || s.ProjectType.ToString().Contains(searchString) || s.Description.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(searchProjectType))
            {
                userWoodProjects = userWoodProjects.Where(x => x.ProjectType.Equals(searchProjectType));
            }

            userWoodProjects = userWoodProjects.OrderByDescending(x => x.PublishDate);
            return View(userWoodProjects);
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
                ViewBag.ProjectTypes = new SelectList(new WoodProject().ProjectTypes);
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
        public ActionResult Create([Bind(Include = "ID,ProjectTitle,ImageURL,Description,ProjectType")] WoodProject woodProject, string projectType, HttpPostedFileBase[] files)
        {
            try
            {
                ViewBag.ProjectTypes = new SelectList(new WoodProject().ProjectTypes);

                if (ModelState.IsValid)
                {

                    woodProject.ProjectType = projectType;
                    foreach (HttpPostedFileBase file in files)
                    {

                        var imageFile = new ImageFile();
                        if (file.ContentLength > 0)
                        {

                            //TODO: Check for same filename and change file path if needed
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/Wood_Project_Images"), fileName);

                            //REMOVE THIS
                            woodProject.ImageURL = path.Substring(path.IndexOf("Content"));

                            imageFile.WoodProject = woodProject;
                            imageFile.Directory = path.Substring(path.IndexOf("Content"));

                            db.ImageFiles.Add(imageFile);
                            file.SaveAs(path);
                        }
                        
                    }

                   
                    var user = User.Identity.GetUserId();
                    woodProject.UserId = user;
                    woodProject.PublishDate = DateTime.Now;
                    db.WoodProjects.Add(woodProject);
                    
                    db.SaveChanges();
                    
                }
            } catch (RetryLimitExceededException dex)
            {
                //Can log dex exception here
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return View("Error", new HandleErrorInfo(dex, "WoodProject", "Index"));
            }

            return RedirectToAction("Index");
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
            var result = db.ImageFiles.Where(x => x.WoodProject.Id == id);
            db.WoodProjects.Remove(woodProject);

            foreach (var image in result)
            {
                db.ImageFiles.Remove(image);
            }
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
