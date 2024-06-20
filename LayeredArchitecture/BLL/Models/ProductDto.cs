using System.Text.Json.Serialization;

namespace BLL.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
    }
}
