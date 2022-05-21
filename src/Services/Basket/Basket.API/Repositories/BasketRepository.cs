using System.Text.Json;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _cache;

    public BasketRepository(IDistributedCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<ShoppingCart?> GetBasket(string username)
    {
        var basketString = await _cache.GetStringAsync(username);
        if (string.IsNullOrEmpty(basketString)) return null;

        return JsonSerializer.Deserialize<ShoppingCart>(basketString);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        await _cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));

        return await GetBasket(basket.UserName) ?? throw new InvalidOperationException($"Basket can't be found for user {basket.UserName}");
    }

    public async Task DeleteBasket(string username)
    {
        await _cache.RemoveAsync(username);
    }
}