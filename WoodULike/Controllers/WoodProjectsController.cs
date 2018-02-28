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

namespace WoodULike.Controllers
{
    public class WoodProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WoodProjects
        public ActionResult Index(string searchString, string searchProjectType)
        {
            var viewModel = new WoodProjectsWithUsername();

            viewModel.WoodProjects = db.WoodProjects.Include(x => x.ApplicationUser).OrderByDescending(x => x.PublishDate);

            WoodProject a = new WoodProject();
            
            SelectList pTypes = new SelectList(a.ProjectTypes);

            ViewBag.ProjectTypes = pTypes;





            //var woodProjects = from m in db.WoodProjects
            //                   select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.WoodProjects = viewModel.WoodProjects.Where(s =>
                                                    s.ProjectTitle.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                                    s.ProjectType.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                                    s.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase));
               
            }
            if (!String.IsNullOrEmpty(searchProjectType))
            {
                viewModel.WoodProjects = viewModel.WoodProjects.Where(s => s.ProjectType.Contains(searchProjectType));
            }
            //woodProjects = woodProjects.OrderByDescending(x => x.PublishDate);

            //return View(woodProjects);

            return View(viewModel);
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
            return View();
        }

        // POST: WoodProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProjectTitle,ImageURL,Description,ProjectType")] WoodProject woodProject, HttpPostedFileBase file)
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
            if (ModelState.IsValid)
            {
                db.Entry(woodProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(woodProject);
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
