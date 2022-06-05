using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;

        public ShoppingController(ICatalogService catalogService, IOrderService orderService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _orderService = orderService;
            _basketService = basketService;
        }

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string username)
        {
            var basket = await _basketService.GetBasket(username);
            foreach (var basketItemExtendedModel in basket.ShoppingCartItems)
            {
                var product = await _catalogService.GetCatalog(basketItemExtendedModel.ProductId);

                basketItemExtendedModel.ProductName = product.Name;
                basketItemExtendedModel.Category = product.Category;
                basketItemExtendedModel.Summary = product.Summary;
                basketItemExtendedModel.Description = product.Description;
                basketItemExtendedModel.ImageFile = product.ImageFile;

            }

            var orders = await _orderService.GetOrdersByUserName(username);

            return Ok(new ShoppingModel()
            {
                BasketWithProducts = basket,
                Orders = orders,
                UserName = username
            });
        }
    }
}
