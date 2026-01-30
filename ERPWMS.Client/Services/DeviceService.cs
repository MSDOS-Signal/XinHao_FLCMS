using System.Net.Http.Json;
using DomainDevice = ERPWMS.Domain.Entities.Device;

namespace ERPWMS.Client.Services;

public class DeviceService
{
    private readonly HttpClient _httpClient;

    public DeviceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DomainDevice>> GetAllAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<DomainDevice>>("api/device") ?? new List<DomainDevice>();
        }
        catch
        {
            return new List<DomainDevice>();
        }
    }

    public async Task<bool> ToggleStatusAsync(Guid id, string status)
    {
        var response = await _httpClient.PostAsync($"api/device/{id}/toggle?status={status}", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ResetAlarmAsync(Guid id)
    {
        var response = await _httpClient.PostAsync($"api/device/{id}/reset", null);
        return response.IsSuccessStatusCode;
    }
}
