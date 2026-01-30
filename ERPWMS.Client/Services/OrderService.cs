using System.Net.Http.Json;
using ERPWMS.Domain.Entities;

namespace ERPWMS.Client.Services;

public class OrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Order>> GetAllAsync(string type = "Sales")
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Order>>($"api/order?type={type}") ?? new List<Order>();
        }
        catch
        {
            return new List<Order>();
        }
    }

    public async Task<Order?> CreateAsync(Order order)
    {
        var response = await _httpClient.PostAsJsonAsync("api/order", order);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Order>();
        }
        return null;
    }

    public async Task<bool> ProcessOrderAsync(Guid id)
    {
        var response = await _httpClient.PostAsync($"api/order/{id}/process", null);
        return response.IsSuccessStatusCode;
    }

    public async Task UpdateAsync(Order order)
    {
        await _httpClient.PutAsJsonAsync($"api/order/{order.Id}", order);
    }
}
