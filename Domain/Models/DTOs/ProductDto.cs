using Domain.Entities;
using Domain.Models.DTOs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTOs
{
    public class ProductDto : BaseDto
    {
        [MinLength(1)]
        [MaxLength(200)]
        public string? Name { get; set; }
        [MinLength(1)]
        [MaxLength(2000)]
        public string? Description { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public float? Price { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
