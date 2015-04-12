using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace YATApplication.Models
{
    public class Connections
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public UserProfile From { get; set; }
        [Required]
        public UserProfile To { get; set; }
        
        public bool IsRemovied { get; set; }
        public bool IsBlocked { get; set; }
    }

}
