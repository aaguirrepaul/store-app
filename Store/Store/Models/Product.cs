using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class Product
    {
        [Key]
        
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del producto")]
        [Display(Name = "Descripcion")]
        [StringLength(30, ErrorMessage = "Wrong String", MinimumLength =0)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Marca")]
        [StringLength(30, ErrorMessage = "Wrong String", MinimumLength = 0)]
        public string Brand { get; set; }

        [Required]
        [Display(Name = "Fecha Expiracion")]
        [StringLength(30, ErrorMessage = "Wrong String", MinimumLength = 0)]
        public string ExpireDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio Unitario")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal UnitPrice { get; set; }

        [Required]
        [Display(Name = "Proveerdor")]
        [StringLength(30, ErrorMessage = "Wrong String", MinimumLength = 0)]
        public string Provider { get; set; }


        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}