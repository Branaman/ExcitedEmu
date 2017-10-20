using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 
namespace ExcitedEmu.Models
{
    public class Object : BaseEntity
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string image {get;set;}
        [Required]
        public string description {get;set;}
        public int quantity {get;set;}
        public int idobjects {get;set;}
    }
}