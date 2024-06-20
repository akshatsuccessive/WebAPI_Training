using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumeWebAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; } = null!;

        [DisplayName("Product Price")]
        public double Price { get; set; }
    }
}
