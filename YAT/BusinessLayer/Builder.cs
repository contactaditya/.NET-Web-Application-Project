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
                    Gender = false,
                    Photo = "Dex.jpg",
                    Deleted = false,
                    RegistrationDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,

                };
                dbContext.User.Add(paul);
                dbContext.User.Add(mike);
                dbContext.User.Add(sue);
                dbContext.User.Add(Ariel);
            
                var gump = new Likes { Movie = "Forest Gump" };
                var nemo = new Likes { Movie = "Finding Nemo" };
                var avengers = new Likes { Movie = "Avengers" };
                mike.Likes.Add(nemo);
                mike.Likes.Add(gump);
                paul.Likes.Add(nemo);
                sue.Likes.Add(avengers);
                sue.Likes.Add(gump);

                Likes movie = dbContext.Likes.FirstOrDefault(L => L.Movie == "Avengers");
                Debug.Print(movie.Movie + " " + movie.Id);
                Ariel.Likes.Add(movie);

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
                    Console.WriteLine(user.FirstName);
                    Console.WriteLine("-ZIP: "+user.Zip);
                    foreach (var likes in user.Likes)
                    {
                        Console.WriteLine("-MOVIE: "+likes.Movie);
                    }
                }
            }
        }

        public List<User> queryUsers(int minAge, int maxAge, bool gender, int zipcode, int SearcherID)
        {
            //get the user's likes list, count the likes match for every result, and sort by top match
            using (var dbContext = new YATContext())
            {
              
                List<User> users = dbContext.User.SqlQuery("Select * from dbo.Users inner join " +
                 " (select filteredUsers.ID,count(filteredUsers.ID) as score   from " +
                "(SELECT u.ID from dbo.Users as u where " +
                 "u.ID!=" + SearcherID + " and u.Age >=" + minAge + " and u.Age<=" + maxAge + " and u.Gender = " + Convert.ToInt32(gender) + "  and u.Zip = " + zipcode + ")" +
                  " as filteredUsers " +
                          "left join" +
                 "(SELECT dbo.LikesUsers.user_ID, dbo.LikesUsers.Likes_Id from   dbo.LikesUsers where " +
                        "dbo.LikesUsers.Likes_Id in (SELECT dbo.LikesUsers.Likes_ID from   dbo.LikesUsers where " +
                                                      " dbo.LikesUsers.User_ID=1)) as movies " +
                                                            " on filteredUsers.ID = movies.User_Id " +
                                                                  " group by filteredUsers.ID) as results on results.ID=dbo.Users.ID order by score").ToList();
      
                return users;
            }
        }
             

    }

    
}
