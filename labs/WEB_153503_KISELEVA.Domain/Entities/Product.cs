using System;
namespace WEB_153503_KISELEVA.Domain.Entities
{
	public class Product
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
		public double Price { get; set; }
		public string? Image { get; set; }
        public string? CategoryNormalizedName { get; set; }
	}
}

