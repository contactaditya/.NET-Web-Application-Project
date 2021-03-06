﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer
{
    public class User
    {
        public User() {
            this.Likes = new HashSet<Likes>();

            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        public string Address { get; set; }
        [MinLength(1), MaxLength(30)]
        [Required]
        public string FirstName { get; set; }
        [MinLength(1), MaxLength(30)]
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public bool Gender { get; set; }
        public string Photo { get; set; }
        public bool Deleted { get; set; }
        [Required]
        public bool InterestedIn { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        
        //navigtaion properties
        public virtual ICollection<Message> FromMessages { get; set; }        //user received 
        public virtual ICollection<Message> ToMessages { get; set; }          //user sent
        public virtual ICollection<Connections> FromConnections { get; set; } //user made
        public virtual ICollection<Connections> ToConnections { get; set; }   //user receives

        //many to many relationship
        public virtual ICollection<Likes> Likes { get; private set; }
    }
}
