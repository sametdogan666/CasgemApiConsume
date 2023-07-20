using Casgem.ApiConsume.DTOs.CategoryDtos;
using Casgem.ApiConsume.DTOs.CustomerDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Casgem.ApiConsume.Controllers;

public class CustomerController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CustomerController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("https://localhost:44370/api/Customer/getlistcustomer");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCustomerDto>>(jsonData);

            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult AddCustomer()
    {
        return View();
    }

    public async Task<IActionResult> AddCustomer(CreateCustomerDto createCustomerDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createCustomerDto);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("https://localhost:44370/api/Customer/addcustomer", content);

        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return View();
    }

    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync($"https://localhost:44370/api/Customer/{id}");

        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateCustomer(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"https://localhost:44370/api/Customer/getcustomerbyid/{id}");

        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateCustomerDto>(jsonData);

            return View(value);
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateCustomerDto);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PutAsync($"https://localhost:44370/api/Customer", content);

        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return View();
    }
}