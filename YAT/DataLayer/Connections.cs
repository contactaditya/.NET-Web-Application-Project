using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace DataLayer
{
    public class Connections
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        /*
        //paul - i think it might have to be closer to this
        [Required]
        public int FromId { get; set; } 
        [ForeignKey("FromId")] 
        public User From { get; set; } 
        
        [Required]
        public int ToId { get; set; } 
        [ForeignKey("ToId")] 
        public User To { get; set; } 
        */
        
        [Required]
        [ForeignKey("Id")]
        public virtual User From { get; set; }
        [Required]
        [ForeignKey("Id")]
        public virtual User To { get; set; }
        
        public bool IsRemoved { get; set; }
        public bool IsBlocked { get; set; }
    }

}
