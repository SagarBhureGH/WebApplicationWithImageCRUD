using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using WebApplication_ConsumeAPI.Models;


namespace WebApplication_ConsumeAPI.Controllers
{
    public class RegistrationCAPIController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44324/api");
        private readonly HttpClient _httpClient;

        public RegistrationCAPIController(HttpClient  httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            List<RegistrationViewModel> list =new List<RegistrationViewModel>();
            HttpResponseMessage response= _httpClient.GetAsync(baseAddress + "/RegistrationModals/GetregistrationModals").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<RegistrationViewModel>>(data);

            }
            return View(list);
        }

        public IActionResult Details(int Id)
        {
            RegistrationViewModel viewModel = new RegistrationViewModel();  
            HttpResponseMessage response = _httpClient.GetAsync(baseAddress + "/RegistrationModals/GetRegistrationModal/"+Id).Result;
            if (response.IsSuccessStatusCode)
            {
                var data= response.Content.ReadAsStringAsync().Result;
                viewModel=JsonConvert.DeserializeObject<RegistrationViewModel>(data);
            }

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RegistrationViewModel modal)
        {
            try
            {
                string data = JsonConvert.SerializeObject(modal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responce =
                    _httpClient.PostAsync(baseAddress + ("/RegistrationModals/PostRegistrationModal"), content).Result;
                if (responce.IsSuccessStatusCode)
                {
                    TempData["SuccessMSG"] = "Registration Complete......";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMSG"] = ex.Message;
                return View();
            }
            return View();
        }


        // Update: Display edit form for a specific registration
        public IActionResult Edit(int id)
        {
            RegistrationViewModel model = new RegistrationViewModel();
            HttpResponseMessage response =
                _httpClient.GetAsync(baseAddress + "/RegistrationModals/GetRegistrationModal/"+id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<RegistrationViewModel>(data);
            }

            return View(model);
        }

        // Update: Handle form submission to update a registration
        [HttpPost]
        public IActionResult Edit(RegistrationViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    _httpClient.PutAsync(baseAddress + "/RegistrationModals/PutRegistrationModal/"+model.Id , content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMSG"] = "Update Complete......";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMSG"] = ex.Message;
                return View(model);
            }

            return View();
        }

        // Delete: Display confirmation page for deletion
        public IActionResult Delete(int id)
        {
            RegistrationViewModel model = null;
            HttpResponseMessage response = _httpClient.GetAsync(baseAddress + "/RegistrationModals/GetRegistrationModal/"+id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<RegistrationViewModel>(data);
            }

            return View(model);
        }

        // Delete: Handle the actual deletion of a registration
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = 
                    _httpClient.DeleteAsync(baseAddress + $"/RegistrationModals/DeleteRegistrationModal/"+id).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMSG"] = "Deletion Complete......";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMSG"] = ex.Message;
            }

            return RedirectToAction("Index");
        }



    }
}
