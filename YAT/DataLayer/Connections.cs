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
        
        [Required]
        [ForeignKey("Id")]
        public User From { get; set; }
        [Required]
        [ForeignKey("Id")]
        public User To { get; set; }
        
        public bool IsRemovied { get; set; }
        public bool IsBlocked { get; set; }
    }

}
