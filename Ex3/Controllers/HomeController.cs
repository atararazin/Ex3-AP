using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ex3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult display(string ip, string port)
        {
            HomeModel.Instance.Ip = ip;
            HomeModel.Instance.Port = port;
            //HomeModel.Instance.Connect(); remove this comment
            return View();
        }
    }
}