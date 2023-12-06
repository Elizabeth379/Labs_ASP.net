using System;
namespace WEB_153503_KISELEVA.Domain.Entities
{
	public class Cart
	{
        public Dictionary<int, CartItem> CartItems { get; set; } = new();

        public virtual void AddToCart(Product product)
        {
            if (CartItems.ContainsKey(product.Id))
            {
                CartItems[product.Id].Count += 1;
            } else
            {
                CartItems.Add(product.Id, new CartItem { Product = product, Count = 1});
            }
        }
        
        public virtual void RemoveItems(int id)
        {
            if (CartItems.ContainsKey(id))
            {
                CartItems.Remove(id);
            }
        }
        
        public virtual void ClearAll()
        {
            CartItems.Clear();
        }
        
        public int Count { get => CartItems.Sum(item => item.Value.Count); }
        
        public double TotalPrice
        {
            get => CartItems.Sum(item => item.Value.Product.Price * item.Value.Count);
        }
    }
}

