using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace YATApplication.Models
{
    public class Likes
    {
        public Likes()
        {
            this.Users = new HashSet<UserProfile>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(1), MaxLength(50)]
        public string Movie { get; set; }
        
        //navigation properties
        //a user can like many movies
        //a movie can have many users liking it
        public virtual ICollection<UserProfile> Users { get; set; }

    }

}
