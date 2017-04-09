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
using System.Threading.Tasks;

namespace Tangent.Controllers
{   
        
    
    public class ProjectController : Controller
    {
        
               
        
        public ActionResult Index(Project project)
        {
            return View("~/Views/Project/Index.cshtml", project);
        }

        public ActionResult Update(Project project)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Project/Index.cshtml", project);
            }

            using (var client = new HttpClient())
            {
                string token = (string)(Session["token"]);
                client.BaseAddress = new Uri("http://projectservice.staging.tangentmicroservices.com:80/");
                var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("pk", project.pk),
                    new KeyValuePair<string, string>("title", project.title),
                    new KeyValuePair<string, string>("description", project.description),
                    new KeyValuePair<string, string>("start_date", project.start_date),
                    new KeyValuePair<string, string>("end_date", project.end_date),
                    new KeyValuePair<string, string>("is_billable", project.is_billable.ToString()),
                    new KeyValuePair<string, string>("is_active", project.is_active.ToString()),
                    new KeyValuePair<string, string>("task_set", "[]"),
                    new KeyValuePair<string, string>("resource_set", "[]")
                };


               
               var content = new FormUrlEncodedContent(pairs);
                client.DefaultRequestHeaders.Add("ContentType", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", token);
               
                string requestURI = "api/v1/projects/" + project.pk + "/";
                var clientRespond = PatchAsync(client, requestURI, content).Result;
                if (!clientRespond.IsSuccessStatusCode)
                   return View("Error");

                
                return RedirectToAction("Home", "Home");
            }
        }

        public ActionResult Delete(Project project)
        {
            string token = (string)(Session["token"]);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://projectservice.staging.tangentmicroservices.com:80/");
                client.DefaultRequestHeaders.Add("ContentType", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", token);
                var clientRespond = client.DeleteAsync("api/v1/projects/"+project.pk+"/").Result;
                if (!clientRespond.IsSuccessStatusCode)
                    return View("Error");

                
                return RedirectToAction("Home", "Home");
            }
            
        }

        public ActionResult Save(HomeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Index.cshtml", model);
            }

            string token = (string)(Session["token"]);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://projectservice.staging.tangentmicroservices.com:80/");
                var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("title", model.Project.title),
                    new KeyValuePair<string, string>("description", model.Project.description),
                    new KeyValuePair<string, string>("start_date", model.Project.start_date),
                    new KeyValuePair<string, string>("end_date", model.Project.end_date),
                    new KeyValuePair<string, string>("is_billable", model.Project.is_billable.ToString()),
                    new KeyValuePair<string, string>("is_active", model.Project.is_active.ToString())
                };

                var content = new FormUrlEncodedContent(pairs);
                client.DefaultRequestHeaders.Add("ContentType", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", token);
                var clientRespond = client.PostAsync("api/v1/projects/", content).Result;
                if (!clientRespond.IsSuccessStatusCode)
                    return View(new HomeViewModel());
               
                return RedirectToAction("Home", "Home");
            }
        }

        public static Task<HttpResponseMessage> PatchAsync(HttpClient client, string requestURI, FormUrlEncodedContent content )
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(client.BaseAddress + requestURI),
                Content = content,
            };

            return client.SendAsync(request);
        }


    }

    
}

    