using System.Net.Http.Json;
using ERPWMS.Domain.Entities;

namespace ERPWMS.Client.Services;

public class WorkOrderService
{
    private readonly HttpClient _httpClient;

    public WorkOrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<WorkOrder>> GetAllAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<WorkOrder>>("api/workorder") ?? new List<WorkOrder>();
        }
        catch
        {
            return new List<WorkOrder>();
        }
    }

    public async Task<WorkOrder?> CreateAsync(WorkOrder workOrder)
    {
        var response = await _httpClient.PostAsJsonAsync("api/workorder", workOrder);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<WorkOrder>();
        }
        return null;
    }

    public async Task<bool> ReportProgressAsync(Guid id)
    {
        var response = await _httpClient.PostAsync($"api/workorder/{id}/report", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RevertProgressAsync(Guid id)
    {
        var response = await _httpClient.PostAsync($"api/workorder/{id}/revert", null);
        return response.IsSuccessStatusCode;
    }
}
