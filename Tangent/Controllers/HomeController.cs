using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using Newtonsoft.Json;
using Tangent.Models;

namespace Tangent.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Home()
        {
            if (Session["token"] == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            ViewBag.Message = "Home page.";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://projectservice.staging.tangentmicroservices.com:80/");
                string token = (string)(Session["token"]);
               client.DefaultRequestHeaders.Add("ContentType", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", token);
                var clientRespond = client.GetAsync("api/v1/projects/").Result;

                var viewModel = new HomeViewModel();
                viewModel.ProjectList = new List<Project>();
                
                if (!clientRespond.IsSuccessStatusCode)
                    return View("Error");
                var rep = clientRespond.Content.ReadAsStringAsync();
                viewModel.ProjectList = clientRespond.Content.ReadAsAsync<IList<Project>>().Result;
                return View(viewModel);
            }
        } 
    } 
}

    