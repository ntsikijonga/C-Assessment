using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Tangent.Models;
using System.Web.Script.Serialization;

namespace Tangent.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }
        //AuthenticateUser", "Account
        [HttpPost]
        public ActionResult AuthenticateUser(LoginViewModel loginViewModel)
        {
           
            if(!ModelState.IsValid)
            {
                return View("~/Views/Home/Index.cshtml", loginViewModel);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://userservice.staging.tangentmicroservices.com/");
                var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", loginViewModel.username),
                    new KeyValuePair<string, string>("password", loginViewModel.password)
                };

                var content = new FormUrlEncodedContent(pairs);
               
                var clientRespond = client.PostAsync("api-token-auth/", content).Result;
                if (!clientRespond.IsSuccessStatusCode)
                    return View(new LoginViewModel());
                
                var rep = clientRespond.Content.ReadAsStringAsync().Result;
                var jsonRep = System.Web.Helpers.Json.Decode(rep);
                Session.Add("token", jsonRep.token);
                return RedirectToAction("Home", "Home");
                   
            }
        }
    }
}