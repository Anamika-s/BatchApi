using Microsoft.AspNetCore.Mvc;
using BatchMVCClient.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BatchMVCClient.Controllers
{
    public class LoginController : Controller
    {
        string url = " https://localhost:7218/api/login";
        HttpClient client = null;
        public LoginController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);

        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var token = string.Empty;
            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            
            client.BaseAddress = new Uri(url);
            var contentType = new MediaTypeWithQualityHeaderValue
("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            var Response = client.PostAsync(url, content);
            var result = Response.Result;

            if (result.IsSuccessStatusCode)
            {
                var stringJWT = result.Content.ReadAsStringAsync().Result;
                JwtToken jwt = JsonConvert.DeserializeObject
  <JwtToken>(stringJWT);
                //HttpContext.Session.SetString("token", jwt.Token);

                ViewBag.Message = "User logged in successfully!";
                //HttpContext.Session["token"] = token.ToString();
                //return View();

                return RedirectToAction("Index", "Student");

            }
            else
                return View();


        }
    }
}
