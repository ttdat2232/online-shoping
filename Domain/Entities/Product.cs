using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        [Column(TypeName = "nvarchar(200)")]
        public string? Name { get; set; }
        [Column(TypeName = "nvarchar(2000)")]
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public float? Price { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }

    public enum ProductStatus
    {
        UNAVAILABLE = 0,
        AVAILABLE = 1,
        OUT_OF_STOCK = 2,
    }
}