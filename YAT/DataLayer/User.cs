using System;
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
        public User()
        {
            this.Likes = new HashSet<Likes>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        public int Zip { get; set; }
        [MinLength(1), MaxLength(30)]
        public string FirstName { get; set; }
        [MinLength(1), MaxLength(30)]
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public string Photo { get; set; }
        public bool Deleted { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        
        //navigtaion properties
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Connections> Connections { get; set; }

        //many to many relationship
        public virtual ICollection<Likes> Likes { get; private set; }
    }
}
