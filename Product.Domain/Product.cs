
namespace Products.Domain
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [MaxLength(50, ErrorMessage ="The field {0} only can have {1} characters length")]
        [Required (ErrorMessage ="The field {0} is required")]
        [Index("Product_Description")]
        public string Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode =false)]
        public decimal Price { get; set; }

        [Display(Name ="Is Active?")]
        public bool IsActive { get; set; }

        public double Stock { get; set; }

        [Display(Name = "Last Purshase")]
        [DataType(DataType.Date)]
        public DateTime LastPurchase { get; set; }

        [DataType(DataType.MultilineText)]
        public string  Remarks { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; } //Indicate that a product belong to a only one category
    }
}
