using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdSystem.MVC.Models;
using AdSystem.Models;
using System.Data.SqlClient;
using System.IO;

namespace AdSystem.MVC.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private AdDbContext ctx = new AdDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(ctx.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = ctx.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] Category category)
        {
            if (ModelState.IsValid)
            {
                ctx.Categories.Add(category);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            Category category = ctx.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ctx.Entry(category).State = EntityState.Modified;
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "ویرایش رکورد با موفقیت انجام شد";

            }
            catch (SqlException ex)
            {

            }
            catch(FormatException ex)
            {

            }
            catch(IOException ex)
            {

            }
            catch (Exception ex)
            {
                TempData["MessageClass"] = "danger";
                TempData["Message"] = "خطایی در ویرایش رکورد به وجود آمده است\n";
                TempData["Message"] += ex.Message + "\n";
                TempData["Message"] += ex.StackTrace;
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = ctx.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = ctx.Categories.Find(id);
            ctx.Categories.Remove(category);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
