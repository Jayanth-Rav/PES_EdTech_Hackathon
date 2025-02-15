using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;
using PES_EdTech_APP.Models;

namespace PES_EdTech_APP.Controllers
{
    public class EmpController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7047/api");
        private readonly HttpClient _client;
        public EmpController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index()

        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Employee/GetAll/GetAll");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                employees = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(data)!;
            }
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Employee/Insert/Insert", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Employee Added.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                EmployeeViewModel employee = new EmployeeViewModel();
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Employee/GetbyCode/GetbyCode/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<EmployeeViewModel>(data)!;
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress + "/Employee/Update/Update", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Employee details updated.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                EmployeeViewModel employee = new EmployeeViewModel();
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Employee/GetbyCode/GetbyCode/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<EmployeeViewModel>(data)!;
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + "/Employee/Delete/Delete/" + id);
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Employee details deleted.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
    }
}
