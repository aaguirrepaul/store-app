using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Descripcion")]
        [StringLength(30, ErrorMessage = "Wrong Description", MinimumLength = 0)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Precio Unitario")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal UnitPrice { get; set; }


        [DataType(DataType.Currency)]
        [Display(Name = "Precio Unitario")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public float Quantity { get; set; }



        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }


    }
}