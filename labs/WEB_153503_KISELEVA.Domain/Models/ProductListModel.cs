using System;
namespace WEB_153503_KISELEVA.Domain.Models
{
	public class ProductListModel<T>
	{
        public List<T> Items { get; set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
    }
}

