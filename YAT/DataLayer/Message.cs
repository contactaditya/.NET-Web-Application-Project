using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace DataLayer
{
    public class Message
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(1), MaxLength(200)]
        public string Text { get; set; }
        
        public int FromId { get; set; }
        public virtual User From { get; set; }
        public int ToId { get; set; }
        public virtual User To { get; set; }
    
        [Required]
        public DateTime Date { get; set; }
        public bool Read { get; set; }
    }

}
