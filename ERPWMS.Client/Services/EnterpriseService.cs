using System.Net.Http.Json;
using ERPWMS.Domain.Entities;

namespace ERPWMS.Client.Services;

public class EnterpriseService
{
    private readonly HttpClient _httpClient;

    public EnterpriseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>> GetProductsAsync() => await _httpClient.GetFromJsonAsync<List<Product>>("api/enterprise/products") ?? new();

    public async Task<List<Supplier>> GetSuppliersAsync() => await _httpClient.GetFromJsonAsync<List<Supplier>>("api/enterprise/suppliers") ?? new();
    public async Task<Supplier?> GetSupplierByIdAsync(Guid id) => await _httpClient.GetFromJsonAsync<Supplier>($"api/enterprise/suppliers/{id}");
    public async Task<SupplierStats?> GetSupplierStatsAsync(Guid id) => await _httpClient.GetFromJsonAsync<SupplierStats>($"api/enterprise/suppliers/{id}/stats");
    public async Task AddSupplierAsync(Supplier supplier) => await _httpClient.PostAsJsonAsync("api/enterprise/suppliers", supplier);
    public async Task UpdateSupplierAsync(Supplier supplier) => await _httpClient.PutAsJsonAsync($"api/enterprise/suppliers/{supplier.Id}", supplier);
    public async Task DeleteSupplierAsync(Guid id) => await _httpClient.DeleteAsync($"api/enterprise/suppliers/{id}");

    public async Task<List<Shipment>> GetShipmentsAsync() => await _httpClient.GetFromJsonAsync<List<Shipment>>("api/enterprise/shipments") ?? new();
    public async Task AddShipmentAsync(Shipment shipment) => await _httpClient.PostAsJsonAsync("api/enterprise/shipments", shipment);
    public async Task DeleteShipmentAsync(Guid id) => await _httpClient.DeleteAsync($"api/enterprise/shipments/{id}");
    public async Task UpdateShipmentAsync(Shipment shipment) => await _httpClient.PutAsJsonAsync($"api/enterprise/shipments/{shipment.Id}", shipment);

    public async Task<List<QualityCheck>> GetQualityChecksAsync() => await _httpClient.GetFromJsonAsync<List<QualityCheck>>("api/enterprise/qualitychecks") ?? new();
    public async Task AddQualityCheckAsync(QualityCheck check) => await _httpClient.PostAsJsonAsync("api/enterprise/qualitychecks", check);
    public async Task UpdateQualityCheckAsync(QualityCheck check) => await _httpClient.PutAsJsonAsync($"api/enterprise/qualitychecks/{check.Id}", check);
    public async Task DeleteQualityCheckAsync(Guid id) => await _httpClient.DeleteAsync($"api/enterprise/qualitychecks/{id}");

    public async Task<List<ProductionPlan>> GetPlansAsync() => await _httpClient.GetFromJsonAsync<List<ProductionPlan>>("api/enterprise/plans") ?? new();
    public async Task AddPlanAsync(ProductionPlan plan) => await _httpClient.PostAsJsonAsync("api/enterprise/plans", plan);
    public async Task UpdatePlanAsync(ProductionPlan plan) => await _httpClient.PutAsJsonAsync($"api/enterprise/plans/{plan.Id}", plan);
    public async Task DeletePlanAsync(Guid id) => await _httpClient.DeleteAsync($"api/enterprise/plans/{id}");

    public async Task<List<Asset>> GetAssetsAsync() => await _httpClient.GetFromJsonAsync<List<Asset>>("api/enterprise/assets") ?? new();
    public async Task AddAssetAsync(Asset asset) => await _httpClient.PostAsJsonAsync("api/enterprise/assets", asset);
    public async Task UpdateAssetAsync(Asset asset) => await _httpClient.PutAsJsonAsync($"api/enterprise/assets/{asset.Id}", asset);
    public async Task DeleteAssetAsync(Guid id) => await _httpClient.DeleteAsync($"api/enterprise/assets/{id}");

    public async Task<List<Operation>> GetOperationsAsync() => await _httpClient.GetFromJsonAsync<List<Operation>>("api/enterprise/operations") ?? new();
    public async Task AddOperationAsync(Operation op) => await _httpClient.PostAsJsonAsync("api/enterprise/operations", op);
    public async Task UpdateOperationAsync(Operation op) => await _httpClient.PutAsJsonAsync($"api/enterprise/operations/{op.Id}", op);
    public async Task DeleteOperationAsync(Guid id) => await _httpClient.DeleteAsync($"api/enterprise/operations/{id}");
    public async Task<List<BomItem>> GetBomItemsAsync() => await _httpClient.GetFromJsonAsync<List<BomItem>>("api/enterprise/bom") ?? new();
}

public class SupplierStats
{
    public List<string> Months { get; set; } = new();
    public List<int> OnTimeRates { get; set; } = new();
    public List<int> QualityRates { get; set; } = new();
}
