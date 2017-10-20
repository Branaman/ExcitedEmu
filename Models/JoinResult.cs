using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 
namespace ExcitedEmu.Models
{
    public class JoinResult : BaseEntity
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public int quantity {get;set;}
    }
}