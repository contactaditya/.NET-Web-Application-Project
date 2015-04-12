using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace YATApplication.Models
{
    public class Message
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(1), MaxLength(200)]
        public string Text { get; set; }
        [Required]
        public UserProfile From { get; set; }
        [Required]
        public UserProfile To { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool Read { get; set; }
    }

}
