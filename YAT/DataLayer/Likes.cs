using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace DataLayer
{
    public class Likes
    {
        public Likes()
        {
            this.Users = new HashSet<User>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(1), MaxLength(50)]
        [Index(IsUnique = true)]
        public string Movie { get; set; }
        
        //navigation properties
        //a user can like many movies
        //a movie can have many users liking it
        public virtual ICollection<User> Users { get; private set; }

    }

}
