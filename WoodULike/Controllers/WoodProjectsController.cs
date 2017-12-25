using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WoodULike.Models;

namespace WoodULike.Controllers
{
    public class WoodProjectsController : Controller
    {
        private WoodProjectDBContext db = new WoodProjectDBContext();

        // GET: WoodProjects
        public ActionResult Index()
        {
            return View(db.WoodProjects.ToList());
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
        public ActionResult Create([Bind(Include = "ID,ProjectTitle,ImageURL,Description,PublishDate,ProjectType")] WoodProject woodProject)
        {
            if (ModelState.IsValid)
            {
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
