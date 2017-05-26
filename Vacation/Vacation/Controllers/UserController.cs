using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Vacation.Models;

namespace Vacation.Controllers
{
	public class UserController : Controller
	{
		VacationEntities db = new VacationEntities();

		// GET: Account
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginViewModel login)
		{
			if (ModelState.IsValid)
			{
				var v = db.Users.Where(a => a.E_mail == login.Email && a.Password == login.Password).FirstOrDefault();
				if (v != null)
				{
					FormsAuthentication.SetAuthCookie(login.Email, login.RememberMe);
					Session["Id"] = v.Id.ToString();
					Session["Email"] = v.E_mail.ToString();
					Session["Name"] = v.Name.ToString();
					Session["Address"] = v.Address.ToString();
					Session["Phone"] = v.Phone.ToString();
					return Redirect("/");

				}
			}
			return Redirect("/");
		}
		[HttpPost]
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			Session.Clear();
			return RedirectToAction("Index", "Home");
		}

		public ActionResult HistoryCart(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var order = db.Orders.Where(x=>x.User_Id == id).ToList();
			foreach(var item in order)
			{
				ViewBag.Detail = db.DetailOrders.Where(a => a.Order_Id == item.Id).ToArray();
			}
			if (order == null)
			{
				return HttpNotFound();
			}
			return View(order);
		}
	}
}