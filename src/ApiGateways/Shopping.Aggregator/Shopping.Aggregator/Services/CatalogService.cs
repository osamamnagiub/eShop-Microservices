using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    public async Task<IEnumerable<CatalogModel>> GetCatalog()
    {
        var response = await _httpClient.GetAsync("api/v1/Catalog");
        return await response.ReadContentsAs<IEnumerable<CatalogModel>>();
    }

    public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
    {
        var response = await _httpClient.GetAsync($"api/v1/Catalog/GetProductByCategoryName/{category}");
        return await response.ReadContentsAs<IEnumerable<CatalogModel>>();
    }

    public async Task<CatalogModel> GetCatalog(string id)
    {
        var response = await _httpClient.GetAsync($"api/v1/Catalog/{id}");
        return await response.ReadContentsAs<CatalogModel>();
    }
}