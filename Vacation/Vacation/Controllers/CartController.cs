using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vacation.Models;
using System.Web.Script.Serialization;

namespace Vacation.Controllers
{
	public class CartController : Controller
	{
		private VacationEntities db = new VacationEntities();
		private const string CartSession = "CartSession";
		// GET: CartItem
		public ActionResult Index()
		{
			var cart = Session[CartSession];
			var list = new List<Cart>();
			if (cart != null)
			{
				list = (List<Cart>)cart;
			}
			return View(list);
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
				return RedirectToAction("Index");
			}
		}
	}