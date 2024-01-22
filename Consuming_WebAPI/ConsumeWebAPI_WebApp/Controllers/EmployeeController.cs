using ConsumeWebAPI_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumeWebAPI_WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7150/api");
        
        // HttpClient
        private readonly HttpClient _httpClient;
        public EmployeeController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employeesList = new List<Employee>();
            HttpResponseMessage respone = _httpClient.GetAsync(_httpClient.BaseAddress + "/Employee/GetAllEmployees").Result;  // controllerName/Action method

            if(respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;

                // deserialize the string data to json
                employeesList = JsonConvert.DeserializeObject<List<Employee>>(data);
            }
            return View(employeesList);
        }
    }
}
