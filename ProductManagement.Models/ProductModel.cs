using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is a required field.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Category is a required field.")]
        public string Category { get; set; }
        [Required(ErrorMessage = "SubCategory is a required field.")]
        public string SubCategory { get; set; }
        [Required(ErrorMessage = "Quantity is a required field.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is a required field.")]
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
    }
}
