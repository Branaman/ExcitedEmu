using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 
namespace ExcitedEmu.Models
{
    public class Order : BaseEntity
    {
        [Required]
        public int users_idusers { get; set; }
        [Required]
        public int products_idproducts { get; set; }
        [Required]
        public int quantity {get;set;}
        public int idorders {get;set;}
    }
}