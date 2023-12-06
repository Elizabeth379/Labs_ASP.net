using System;
namespace WEB_153503_KISELEVA.Domain.Entities
{
	public class Category
	{
		public int Id { get; set; }
		public required string Name {get; set;}
		public required string NormalizedName { get; set; }
	}
}

