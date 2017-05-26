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
					FormsAuthentication.SetAuthCookie(login.Email, false);
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
		[ValidateAntiForgeryToken]
		public ActionResult Register(User u)
		{
			if (ModelState.IsValid)
			{
				db.Users.Add(u);
				db.SaveChanges();
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
	}
}