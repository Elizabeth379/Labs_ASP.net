using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_153503_Kiseleva.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category? Category { get; set; }
        public decimal Price { get; set; }
        public string? Image {  get; set; }
        //public string ImageMimeType { get; set; }
    }
}
