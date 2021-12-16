using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ICT365_Assignment2.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ICT365_Assignment2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment envIn)
        {
            _env = envIn;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult SubmitData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult JoinForm(Models.CollaboratorModel collab)
        {
            string name = collab.Name;
            string age = collab.Age;
            string care = collab.CareLevel;

            JObject newCollab;
            List<JObject> cList;

            cList = JsonConvert.DeserializeObject<List<JObject>>(System.IO.File.ReadAllText(System.IO.Path.Combine(_env.WebRootPath, "data/collaborators.json")));

            newCollab = cList[0].DeepClone().ToObject<JObject>();

            newCollab["Name"] = name;
            newCollab["Age"] = age;
            newCollab["CareLevel"] = care;

            cList.Add(newCollab);

            Console.WriteLine(cList.ToString());

            string newJ = JsonConvert.SerializeObject(cList);

            System.IO.File.WriteAllText(System.IO.Path.Combine(_env.WebRootPath, "data/collaborators.json"), newJ);

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Join()
        {
            Console.WriteLine("derp");
            return View();
        }
        
        public IActionResult Collaborators()
        {
            return View();
        }

        public IActionResult Social()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
