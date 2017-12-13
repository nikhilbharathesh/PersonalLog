using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Nikhil.PersonalLog.Web.Controllers
{
    public class NikhilController : Controller
    { 
        IConfiguration _config;
        ILogger<NikhilController> _logger;
        public NikhilController(IConfiguration configuration, ILogger<NikhilController> logger)
        {
            _config = configuration;
            _logger = logger;
        }
        public IActionResult Index()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var apilink = _config["APIURL"];

                try
                {
                    client.BaseAddress = new Uri(apilink);
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    var response = client.GetAsync("/api/values").Result;
                    string stringData = response.Content.
                    ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Customer>(stringData);
                    return View(data);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.StackTrace);
                    var newvar = new Customer
                    {
                        Designation = ex.Message,
                        Name = apilink,
                        EmailServer = ex.InnerException.ToString()
                    };
                    return View(newvar);
                }
            }
        }
    }
}