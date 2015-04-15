﻿using System;
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
        [Required]
        [ForeignKey("Id")]

        public User From { get; set; }
        [Required]
        [ForeignKey("Id")]

        public User To { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool Read { get; set; }
    }

}
