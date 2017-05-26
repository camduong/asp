using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vacation.Models;

namespace Vacation.Controllers
{
	public class ToursController : Controller
	{
		private VacationEntities db = new VacationEntities();

		// GET: Tours
		public ActionResult Index()
		{
			var tours = db.Tours.Include(t => t.Location);
			return View(tours.ToList());
		}

		// GET: Tours/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Tour tour = db.Tours.Find(id);
			if (tour == null)
			{
				return HttpNotFound();
			}
			return View(tour);
		}

		// GET: Tours/Create
		public ActionResult Create()
		{
			ViewBag.Location_ID = new SelectList(db.Locations, "Id", "Name");
			return View();
		}

		// POST: Tours/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost, ValidateInput(false)]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Name,Slug,Location_ID,NumberTicket,Depart_Date,Return_Date,Day,Price,Schedule")] Tour tour, HttpPostedFileBase images)
		{
			if (ModelState.IsValid)
			{
				db.Tours.Add(tour);
				db.SaveChanges();
				var id = tour.Id;
				var filePath = Path.Combine(HttpContext.Server.MapPath("~/Content/img"), Path.GetFileName(images.FileName));
				string fileName = Path.GetFileName(images.FileName);
				images.SaveAs(filePath);
				var image = new Image();
				image.Tour_Id = id;
				image.Img_Url = fileName;
				db.Images.Add(image);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.Location_ID = new SelectList(db.Locations, "Id", "Name", tour.Location_ID);
			return View(tour);
		}

		// GET: Tours/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Tour tour = db.Tours.Find(id);
			if (tour == null)
			{
				return HttpNotFound();
			}
			ViewBag.Location_ID = new SelectList(db.Locations, "Id", "Name", tour.Location_ID);
			return View(tour);
		}

		// POST: Tours/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost, ValidateInput(false)]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Name,Slug,Location_ID,NumberTicket,Depart_Date,Return_Date,Day,Price,Schedule")] Tour tour)
		{
			if (ModelState.IsValid)
			{
				db.Entry(tour).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.Location_ID = new SelectList(db.Locations, "Id", "Name", tour.Location_ID);
			return View(tour);
		}

		// GET: Tours/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Tour tour = db.Tours.Find(id);
			if (tour == null)
			{
				return HttpNotFound();
			}
			return View(tour);
		}

		// POST: Tours/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Tour tour = db.Tours.Find(id);
			db.Tours.Remove(tour);
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
