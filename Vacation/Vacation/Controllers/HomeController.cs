using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Vacation.Models;

namespace Vacation.Controllers
{
	public class HomeController : Controller
	{
		private const string CartSession = "CartSession";
		private VacationEntities db = new VacationEntities();
		// GET: Home
		public ActionResult Index()
		{
			return View(db.Locations.ToList());
		}

		public ActionResult Tour(int? page)
		{
			int pageSize = 3;
			int pageNumber = (page ?? 1);
			return View(db.Tours.OrderBy(a => a.Id).ToPagedList(pageNumber, pageSize));
		}

		public ActionResult FindTour(string name, int? page)
		{
			int pageSize = 3;
			int pageNumber = (page ?? 1);
			if (name == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var tour = db.Tours.Where(a => a.Location.Slug == name).OrderBy(a => a.Id).ToPagedList(pageNumber, pageSize);
			if (tour == null)
			{
				return HttpNotFound();
			}
			return View("Tour", tour);
		}

		public ActionResult DetailTour(int? id)
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

		public PartialViewResult Cart()
		{
			var cart = Session[CartSession];
			var list = new List<Cart>();
			if (cart != null)
			{
				list = (List<Cart>)cart;
			}
			return PartialView(list);
		}

		public JsonResult DeleteAll()
		{
			Session[CartSession] = null;
			return Json(new
			{
				status = true
			});
		}

		public JsonResult Delete(int id)
		{
			var sessioncart = (List<Cart>)Session[CartSession];
			sessioncart.RemoveAll(x => x.Tour.Id == id);
			Session[CartSession] = sessioncart;
			return Json(new
			{
				status = true
			});
		}

		public JsonResult Update(string cartModel)
		{
			var jsoncart = new JavaScriptSerializer().Deserialize<List<Cart>>(cartModel);
			var sessioncart = (List<Cart>)Session[CartSession];

			foreach (var item in sessioncart)
			{
				var jsonItem = jsoncart.SingleOrDefault(x => x.Tour.Id == item.Tour.Id);
				if (jsonItem != null)
				{
					item.Quatity = jsonItem.Quatity;
				}
			}
			Session[CartSession] = sessioncart;
			return Json(new
			{
				status = true
			});
		}

		public ActionResult AddItem(int tourid, int quatity)
		{
			var tour = db.Tours.Find(tourid);
			var cart = Session[CartSession];
			if (cart != null)
			{
				var list = (List<Cart>)cart;
				if (list.Exists(x => x.Tour.Id == tourid))
				{
					foreach (var item in list)
					{
						if (item.Tour.Id == tourid)
						{
							item.Quatity += quatity;
						}
					}
				}
				else
				{
					var item = new Cart();
					item.Tour = tour;
					item.Quatity = quatity;
					list.Add(item);
				}
			}
			else
			{
				var item = new Cart();
				item.Tour = tour;
				item.Quatity = quatity;
				var list = new List<Cart>();
				list.Add(item);
				Session[CartSession] = list;
			}
			return RedirectToAction("Tour");
		}

		public ActionResult Checkout()
		{
			var cart = Session[CartSession];
			var list = new List<Cart>();
			if (cart != null)
			{
				list = (List<Cart>)cart;
			}
			return View(list);
		}

		[HttpPost]
		public ActionResult Checkout(string name, string address, string phone)
		{
			var cart = (List<Cart>)Session[CartSession];
			var totalPrice = 0;
			var order = new Order();
			order.Created_at = DateTime.Now;
			order.Name = name;
			order.Address = address;
			order.Phone = phone;
			order.Status = "Chưa xử lý";
			foreach (var item in cart)
			{ 
				totalPrice += (int)(item.Tour.Price * item.Quatity);
			}
			order.Total_Price = totalPrice;
			db.Orders.Add(order);
			db.SaveChanges();
			var id = order.Id;
			foreach (var item in cart)
			{
				var orderDetail = new DetailOrder();
				orderDetail.Tour_Name = item.Tour.Name;
				orderDetail.Order_Id = id;
				orderDetail.Tour_Price = item.Tour.Price;
				orderDetail.Tour_Qty = item.Quatity;
				db.DetailOrders.Add(orderDetail);
				db.SaveChanges();
			}
			order.Status = "Chưa xử lý";
			return Redirect("/");
		}
	}
}