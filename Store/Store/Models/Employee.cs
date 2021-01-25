using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class Employee
    {   
        [Key]
        public int EmployeeId { get; set; }
        
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Display(Name = "Documento")]
        public long Dni { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public string BornDate { get; set; }

        [Display(Name = "Edad")]
        public int Age { get; set; }

        [Display(Name = "Nro Afiliado")]
        public string FileNumber { get; set; }

        public string FullName { get { return string.Format("{0} {1}", Name, LastName); } }

        public virtual ICollection<Order> Orders { get; set; }
    }
}