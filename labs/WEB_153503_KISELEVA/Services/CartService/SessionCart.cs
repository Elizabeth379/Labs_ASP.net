using System;
using System.Text.Json.Serialization;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Extensions;

namespace WEB_153503_KISELEVA.Services.CartService
{
	public class SessionCart : Cart
	{
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()
            .HttpContext?.Session;
            SessionCart cart = session?.Get<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession? Session { get; set; }

        public override void AddToCart(Product product)
        {
            base.AddToCart(product);
            Session?.Set("Cart", this);
        }

        public override void RemoveItems(int id)
        {
            base.RemoveItems(id);
            Session?.Set<SessionCart>("Cart", this);
        }

        public override void ClearAll()
        {
            base.ClearAll();
            Session?.Remove("Cart");
        }
    }
}

