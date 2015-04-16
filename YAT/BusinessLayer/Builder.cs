using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Diagnostics;

namespace BusinessLayer
{
    public class Builder
    {
        public void putData()
        {
            using (var dbContext = new YATContext())
            {
                var paul = new User
                {
                    Zip = 11791,
                    FirstName = "Paul",
                    LastName = "Sultan",
                    Age = 28,
                    Gender = false,
                    Photo = "paul.jpg",
                    Deleted = false,
                    RegistrationDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,
                    

                };
                var mike = new User
                {
                    Zip = 11791,
                    FirstName = "Mike",
                    LastName = "Sultan",
                    Age = 28,
                    Gender = false,
                    Photo = "mike.jpg",
                    Deleted = false,
                    RegistrationDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,

                };
                var sue = new User
                {
                    Zip = 11791,
                    FirstName = "Sue",
                    LastName = "Flower",
                    Age = 28,
                    Gender = true,
                    Photo = "sue.jpg",
                    Deleted = false,
                    RegistrationDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,

                };

                var Ariel = new User
                {
                    Zip = 11791,
                    FirstName = "DEX",
                    LastName = "Flow",
                    Age = 28,
                    Gender = true,
                    Photo = "Dex.jpg",
                    Deleted = false,
                    RegistrationDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,

                };

                var gump = new Likes { Movie = "Forest Gump" };
                var nemo = new Likes { Movie = "Finding Nemo" };
                var avengers = new Likes { Movie = "Avengers" };


                dbContext.User.Add(paul);
                dbContext.User.Add(mike);
                dbContext.User.Add(sue);
                dbContext.User.Add(Ariel);
            
               
                //dbContext.Likes.Add(gump);
                //dbContext.Likes.Add(nemo);
                //dbContext.Likes.Add(avengers);
                mike.Likes.Add(nemo);
                mike.Likes.Add(gump);
                paul.Likes.Add(nemo);
                sue.Likes.Add(avengers);
                sue.Likes.Add(gump);
                Ariel.Likes.Add(nemo);

                var sueConnection = new Connections { From = sue, To = mike, IsBlocked = true, IsRemoved=false };
                var sueConnection2 = new Connections { From = sue, To = paul, IsBlocked=false, IsRemoved = true };
                dbContext.Connections.Add(sueConnection);
                dbContext.Connections.Add(sueConnection2);
                
                var message = new Message
                {
                    Text = "Hi there",
                    From = mike,
                    To = sue,
                    Date = DateTime.Now,
                    Read = false
                };
                dbContext.Messages.Add(message);
                dbContext.SaveChanges();
            }
        }

        public void getData()
        {
            using (var db = new YATContext())
            {
                var users = from user in db.User select user;
                foreach (var user in users)
                {
                    Debug.WriteLine(user.Zip);
                    Debug.WriteLine(user.Likes);
                }
            }
        }
    }

    
}
