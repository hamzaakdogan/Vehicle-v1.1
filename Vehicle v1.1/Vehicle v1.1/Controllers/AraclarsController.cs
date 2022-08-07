using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vehicle_v1._1.Models;

namespace Vehicle_v1._1.Controllers
{
    public class AraclarsController : Controller
    {
        private AracEntities1 db = new AracEntities1();

        // GET: Araclars


        //public ActionResult Index()
        //{
        //    return View(db.Araclars.ToList());
        //}

        // GET: Araclars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Araclar araclar = db.Araclars.Find(id);
            if (araclar == null)
            {
                return HttpNotFound();
            }
            return View(araclar);
        }

        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var search = from s in db.Araclars
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                search = search.Where(s => s.AracPlaka.Contains(searchString)
                                       || s.AracMarka.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    search = search.OrderByDescending(s => s.AracPlaka);
                    break;
                case "Date":
                    search = search.OrderBy(s => s.AracMarka);
                    break;
                case "date_desc":
                    search = search.OrderByDescending(s => s.AracModel);
                    break;
                default:
                    search = search.OrderBy(s => s.AracTipiID);
                    break;
            }
            return View(search.ToList());
        }


        // GET: Araclars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Araclars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AracTipiID,AracTipi,AracMarka,AracModel,AracPlaka,AracModelYil,AracRenk,Kapasite_KG,Kapasite_m3")] Araclar araclar)
        {
            if (ModelState.IsValid)
            {
                db.Araclars.Add(araclar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(araclar);
        }

        // GET: Araclars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Araclar araclar = db.Araclars.Find(id);
            if (araclar == null)
            {
                return HttpNotFound();
            }
            return View(araclar);
        }

        // POST: Araclars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AracTipiID,AracTipi,AracMarka,AracModel,AracPlaka,AracModelYil,AracRenk,Kapasite_KG,Kapasite_m3")] Araclar araclar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(araclar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(araclar);
        }

        // GET: Araclars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Araclar araclar = db.Araclars.Find(id);
            if (araclar == null)
            {
                return HttpNotFound();
            }
            return View(araclar);
        }

        // POST: Araclars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Araclar araclar = db.Araclars.Find(id);
            db.Araclars.Remove(araclar);
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
