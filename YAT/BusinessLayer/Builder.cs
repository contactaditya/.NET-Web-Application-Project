using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Diagnostics;

namespace BusinessLayer
{
    public enum UserSort { Match, LastLog, LastJoin }
    public class Builder
    {
        public void putData()
        {
            using (var dbContext = new YATContext())
            {
                User paul = dbContext.User.Where(p => p.FirstName.Contains("Paul")).FirstOrDefault();
                if (paul != null)
                {
                    return;
                }

                paul = new User
                {
                    Zip = 11791,
                    FirstName = "Paul",
                    LastName = "Sultan",
                    Age = 28,
                    Gender = true,
                    Photo = "paul.jpg",
                    Deleted = false,
                    InterestedIn = false,
                    RegistrationDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,
                };
                var mike = new User
                {
                    Zip = 11791,
                    FirstName = "Mike",
                    LastName = "Sultan",
                    Age = 28,
                    Gender = true,
                    Photo = "mike.jpg",
                    Deleted = false,
                    InterestedIn = false,
                    RegistrationDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,

                };
                var sue = new User
                {
                    Zip = 11791,
                    FirstName = "Sue",
                    LastName = "Flower",
                    Age = 28,
                    Gender = false,
                    Photo = "sue.jpg",
                    Deleted = false,
                    InterestedIn = true,
                    RegistrationDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,

                };
                var Ariel = new User
                {
                    Zip = 11791,
                    FirstName = "Brad",
                    LastName = "Gabe",
                    Age = 28,
                    Gender = true,
                    Photo = "Dex.jpg",
                    Deleted = false,
                    InterestedIn = false,
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
               // Debug.Print(movie.Movie + " " + movie.Id);
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
                var reply = new Message
                {
                    Text = "Hello",
                    From = sue,
                    To = mike,
                    Date = DateTime.Now.AddMinutes(5),
                    Read = false
                };
                var repAgain = new Message
                {
                    Text = "wanna go program some tests??!?!!",
                    From = mike,
                    To = sue,
                    Date = DateTime.Now.AddMinutes(8),
                    Read = false
                };
                var anotherMessage = new Message
                {
                    Text = "something awesome",
                    From = Ariel,
                    To = sue,
                    Date = DateTime.Now,
                    Read = false
                };
                dbContext.Messages.Add(message);
                dbContext.Messages.Add(reply);
                dbContext.Messages.Add(repAgain);
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
                    Debug.WriteLine(user.FirstName);
                    Debug.WriteLine("-ZIP: " + user.Zip);
                    foreach (var likes in user.Likes)
                    {
                        Debug.WriteLine("-MOVIE: " + likes.Movie);
                    }
                }
            }
        }

        public List<User> queryUsers(int minAge, int maxAge, bool gender, int zipcode, int SearcherID, UserSort sortBy )
        {
            string qryStr;
                string filteredUsers = 
                    "SELECT u.ID from dbo.Users as u where " +
                     "u.ID!=" + SearcherID + " and u.Age >=" + minAge + " and u.Age<=" + maxAge +
                     " and u.Gender = " + Convert.ToInt32(gender) + "  and u.Zip = " + zipcode ;
                 string  defaultStr = "Select * from dbo.Users WHERE dbo.Users.ID in (" + filteredUsers + ")";
            //get the user's likes list, count the likes match for every result, and sort by top match
            using (var dbContext = new YATContext())
            {
                switch (sortBy) // Sort according to criterion
                {
                   case UserSort.Match: //sort by most mutual likes
                        qryStr = "Select * from dbo.Users inner join " +
                                "(select filteredUsers.ID,count(filteredUsers.ID) as score from " +
                             " ( " + filteredUsers + ")" +
                              " as filteredUsers " +
                                      "left join" +
                             "(SELECT dbo.LikesUsers.user_ID, dbo.LikesUsers.Likes_Id from   dbo.LikesUsers where " +
                                    "dbo.LikesUsers.Likes_Id in " +
                                    "(SELECT dbo.LikesUsers.Likes_ID from   dbo.LikesUsers where " +
                                                                  " dbo.LikesUsers.User_ID=1)) as movies " +
                                                                        " on filteredUsers.ID = movies.User_Id " +
                                                                              "GROUP by filteredUsers.ID) as results on results.ID=dbo.Users.ID order by score";
                    break;
                    case UserSort.LastLog:
                    qryStr = defaultStr + 
                              " order by LastLoginDate";
                    break;
                    case UserSort.LastJoin:
                    qryStr = defaultStr + 
                              " order by RegistrationDate";
                    break;
                    default:
                    qryStr = defaultStr; 
                    break;
                }
            
           List<User> users = dbContext.User.SqlQuery(qryStr).ToList();
           return users;
            }
        }
    }
}
