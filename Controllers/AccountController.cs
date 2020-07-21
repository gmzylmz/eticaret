﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eticaret.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Models.Account.RegisterModels user)
        {
            try
            {
                if (user.rePassword!=user.Member.Password)
                {
                    throw new Exception("Şifreler aynı değildir.");
                }
                user.Member.MemberType = DB.MemberTypes.Customer;
                user.Member.AddedDate = DateTime.Now;
                context.Members.Add(user.Member);
                context.SaveChanges();
                return RedirectToAction("Login","Account");
            }
            catch (Exception ex)
            {
                ViewBag.ReError= ex.Message;
                return View();
            }

            
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Account.LoginModels model)
        {
            try
            {
                var user = context.Members.FirstOrDefault(x => x.Password == model.Member.Password && x.Email == model.Member.Email);
                if (user != null)
                {
                    Session["LogonUser"] = user;
                    return RedirectToAction("index", "i");
                }
                else
                {
                    ViewBag.ReError = "Kullanıcı bilgileriniz yanlış";
                    return View();
                }
            }
            catch (Exception ex)
            {

                ViewBag.ReError = ex.Message;
                return View();
            }
           
        }

        public ActionResult Logout()
        {
            Session["LogonUser"] = null;
            return RedirectToAction("Login","Account");
        }

        public ActionResult Profil()
        {
            return View();
        }
    }
}