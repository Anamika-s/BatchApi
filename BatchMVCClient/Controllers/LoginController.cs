using Microsoft.AspNetCore.Mvc;
using BatchMVCClient.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BatchMVCClient.Controllers
{
    public class LoginController : Controller
    {
        string url = "https://localhost:7218/api/login";
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
        public async Task<ActionResult> Login(User user)
        {
            var token = string.Empty;
            //StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            //client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            user.Id = 0;
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, user);
            if (responseMessage.IsSuccessStatusCode)
            {
                var stringJWT = responseMessage.Content.ReadAsStringAsync().Result;
                JwtToken jwt = JsonConvert.DeserializeObject
  <JwtToken>(stringJWT);
                HttpContext.Session.SetString("token", jwt.Token);

                TempData["Message"] = "User logged in successfully!";
                

                return RedirectToAction("Index", "Batch");

            }
            else
                return View();


        }
    }
}
