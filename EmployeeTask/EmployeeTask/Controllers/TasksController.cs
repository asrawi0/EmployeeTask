using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeTask.Models;

namespace EmployeeTask.Controllers
{
    public class TasksController : Controller
    {
        private EmployeeTaskEntities db   = new EmployeeTaskEntities();

        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = db.Tasks.Include(t => t.Employee);
            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {


            var items = new SelectList(db.Employees, "Id", "Name").ToList();
            items.Insert(0, (new SelectListItem { Text = "[None]" ,Value = string.Empty}));
            ViewBag.EmployeeId = items;
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Descraption,IsComplate,EmployeeId,AssignDate,ComplatedDate,comment")] Task task)
        {
            if (ModelState.IsValid)
            {
                if(!task.AssignDate.HasValue && task.EmployeeId.HasValue)
                {
                    task.AssignDate= DateTime.Now;
                }
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            BindEmployee(task);
            return View(task);
        }

        private void BindEmployee(Task task)
        {
            if (task.EmployeeId.HasValue)
            {
                ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", task?.EmployeeId);
            }

            else
            {
                var items = new SelectList(db.Employees, "Id", "Name").ToList();
                items.Insert(0, (new SelectListItem {Text = "[None]", Value = string.Empty}));
                ViewBag.EmployeeId = items;
            }
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }


            BindEmployee(task);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Descraption,IsComplate,EmployeeId,AssignDate,ComplatedDate,comment")] Task task)
        {
            if (ModelState.IsValid)
            {
                if (!task.AssignDate.HasValue && task.EmployeeId.HasValue)
                {
                    task.AssignDate = DateTime.Now;
                }

                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            BindEmployee(task);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
          


            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
           
            db.Tasks.Remove(task);
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
