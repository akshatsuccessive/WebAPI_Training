using ConsumeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ConsumeWebAPI.Controllers
{
    public class ProductController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7172");
        private readonly HttpClient _client;
        public ProductController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = new List<Product>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Product/GetAllProducts").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                // deseralize the string data in json format
                products = JsonConvert.DeserializeObject<List<Product>>(data);    // json -> model 
            }
            return View(products);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product request)
        {
            string data = JsonConvert.SerializeObject(request);   // model -> json
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            // response from the API
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "Product/AddProduct", content).Result;
            if( response.IsSuccessStatusCode )
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = new Product();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Product/GetProduct/" + id).Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<Product>(data);
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "Product/EditProduct/" + model.ProductId, content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = new Product();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Product/GetProduct/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<Product>(data);
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "Product/DeleteProduct/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
