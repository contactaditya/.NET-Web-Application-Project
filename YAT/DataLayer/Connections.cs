﻿using System;
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

        public string FromId { get; set; } 
        public virtual User From { get; set; }

        public string ToId { get; set; } 
        public virtual User To { get; set; }
        
        public bool IsRemoved { get; set; }
        public bool IsBlocked { get; set; }
    }

}
