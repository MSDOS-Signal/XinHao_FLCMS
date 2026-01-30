using System.Net.Http.Json;
using ERPWMS.Domain.Entities;

namespace ERPWMS.Client.Services;

public class InventoryService
{
    private readonly HttpClient _httpClient;

    public InventoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Inventory>> GetAllAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Inventory>>("api/inventory") ?? new List<Inventory>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching inventory: {ex.Message}");
            return new List<Inventory>();
        }
    }

    public async Task AddAsync(Inventory inventory) => await _httpClient.PostAsJsonAsync("api/inventory", inventory);
    public async Task UpdateAsync(Inventory inventory) => await _httpClient.PutAsJsonAsync($"api/inventory/{inventory.Id}", inventory);
    public async Task DeleteAsync(Guid id) => await _httpClient.DeleteAsync($"api/inventory/{id}");

    public async Task<DashboardStats?> GetDashboardStatsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<DashboardStats>("api/inventory/dashboard");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching stats: {ex.Message}");
            return new DashboardStats { TotalQuantity = 0, TotalValue = 0 };
        }
    }
}

public class DashboardStats
{
    public decimal TotalQuantity { get; set; }
    public decimal TotalValue { get; set; }
    public int PendingOrders { get; set; }
    public int SystemAlerts { get; set; }
    public List<CategoryStat> CategoryDistribution { get; set; } = new();
    public SupplyStat SupplyPerformance { get; set; } = new();
}

public class CategoryStat
{
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
}

public class SupplyStat
{
    public int OnTime { get; set; }
    public int Total { get; set; }
}
