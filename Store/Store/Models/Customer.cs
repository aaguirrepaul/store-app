using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class Customer
    {
        [Key]
        
        public int CustomerId { get; set; }

        [Required]
        [Display(Name="Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Documento")]
        public long Dni { get; set; }

        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public string BornDate { get; set; }

        [Required]
        [Display(Name = "Edad")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Numero de Tarjeta")]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Fecha de Expiracion")]
        public string ExpirationDate { get; set; }

        [Required]
        public string cvv { get; set; }

        

        public string FullName { get { return string.Format("{0} {1}", Name, LastName);}} 
        

        public virtual ICollection<Order> Orders { get; set; }
    }
}