using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BatchMVCClient.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace MVCClient.Controllers
{
    
    public class BatchController : Controller
    {
        string url = " https://localhost:7218/api/batch";
        HttpClient client = null;
        public BatchController()
        {
             client = new HttpClient();
             client.BaseAddress = new Uri(url);  

        }



        // GET: StudentController
        public async Task<ActionResult> Index()
        {
            List<Batch> batches = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            { 
                var jsonString = responseMessage.Content.ReadAsStringAsync();
            jsonString.Wait();

             batches = JsonConvert.DeserializeObject<List<Batch>>(jsonString.Result);
        }
            else
            {
                ViewBag.msg = responseMessage.ReasonPhrase;
                return View();
            }


            return View(batches);
        }

        // GET: StudentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Batch batch = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseMessage = await client.GetAsync(url+"/"+id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonString = responseMessage.Content.ReadAsStringAsync();
                jsonString.Wait();

                batch = JsonConvert.DeserializeObject<Batch>(jsonString.Result);
            }
            else
            {
                ViewBag.msg = responseMessage.ReasonPhrase;
                return View();
            }


            return View(batch);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Batch Batch)
        {
            try

            {

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Batch.StartDate = DateTime.Now;
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, Batch);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.msg = responseMessage.ReasonPhrase;
                    return View();
                }
               
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Batch batch = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonString = responseMessage.Content.ReadAsStringAsync();
                jsonString.Wait();

                batch = JsonConvert.DeserializeObject<Batch>(jsonString.Result);
            }
            else
            {
                ViewBag.msg = responseMessage.ReasonPhrase;
                return View();
            }


            return View(batch);

        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Batch Batch)
        {
            try

            {

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url+"/"+id, Batch);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.msg = responseMessage.ReasonPhrase;
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Batch Batch)
        {
            try

            {

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.msg = responseMessage.ReasonPhrase;
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
