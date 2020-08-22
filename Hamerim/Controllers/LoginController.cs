﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hamerim.Data;
using Hamerim.Models;
using Hamerim.Services;

namespace Hamerim.Controllers
{
    public class LoginController : Controller
    {
        readonly IPermissionsService permissionsService;

        public LoginController(IPermissionsService permissionsService)
        {
            this.permissionsService = permissionsService;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            if (permissionsService.ValidateUser(username, password))
            {
                using (var ctx = new HamerimDbContext())
                {
                    Session["User"] = ctx.Users.First(user => user.Username == username &&
                                                              user.Password == password);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Credentials";
                return View();
            }
        }
    }
}