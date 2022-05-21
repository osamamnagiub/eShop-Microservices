namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new();

        public ShoppingCart()
        {
            
        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var shoppingCartItem in ShoppingCartItems)
                {
                    totalPrice += shoppingCartItem.Price;
                }
                return totalPrice;
            }
        }
    }
}
