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
        public User getCurrentUser(string userID)
        {
            using (var dbContext = new YATContext())
            {
                return dbContext.User.Where(p => p.Id.Equals(userID)).FirstOrDefault(); 
            }

        }
        
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
                    Id = "1",
                    Address = "11791",
                    FirstName = "Paul",
                    LastName = "Sultan",
                    Age = 28,
                    Gender = true,
                    Deleted = false,
                    InterestedIn = false,
                    Photo = "male.jpg",
                    RegistrationDate = Convert.ToDateTime("01/11/2015"),
                    LastLoginDate = DateTime.Now,
                };
                var mike = new User
                {
                    Address = "11791",
                    FirstName = "Mike",
                    LastName = "Sultan",
                    Age = 28,
                    Gender = true,
                    Deleted = false,
                    InterestedIn = false,
                    Photo = "male.jpg",
                    RegistrationDate = Convert.ToDateTime("01/20/2015"),
                    LastLoginDate = DateTime.Now,
                };
                var sue = new User
                {
                    Address = "10010",
                    FirstName = "Sue",
                    LastName = "Flower",
                    Age = 28,
                    Gender = false,
                    Deleted = false,
                    InterestedIn = true,
                    Photo = "female.jpg",
                    RegistrationDate = Convert.ToDateTime("04/30/2015"),
                    LastLoginDate = DateTime.Now,
                };
                var Ariel = new User
                {
                    Id = "d",
                    Address = "94101",
                    FirstName = "Brad",
                    LastName = "Gabe",
                    Age = 30,
                    Gender = true,
                    Deleted = false,
                    InterestedIn = false,
                    Photo = "male.jpg",
                    RegistrationDate = Convert.ToDateTime("04/20/2015"),
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
                    Debug.WriteLine("-Address: " + user.Address);
                    foreach (var likes in user.Likes)
                    {
                        Debug.WriteLine("-MOVIE: " + likes.Movie);
                    }
                }
            }
        }

        public void userGenerator(int count)
        {
            using (var dbContext = new YATContext())
            {
                if (dbContext.User.Count() > 10)
                {
                    return;
                }

                List<String> maleNames = new List<string>{"Jacob", "Michael", "Joshua", "Matthew", "Andrew", "Ethan", "Joseph", "Daniel", "Christopher", "Anthony", "William", "Nicholas", "Ryan", "David", "Tyler", "Alexander", "John", "James", "Dylan", "Zachary", "Brandon", "Jonathan", "Samuel", "Benjamin", "Christian", "Justin", "Nathan", "Logan", "Jose", "Noah", "Kevin", "Austin", "Caleb", "Robert", "Thomas", "Elijah", "Jordan", "Aidan", "Cameron", "Hunter", "Jason", "Connor", "Evan", "Jack", "Luke", "Angel", "Isaac", "Isaiah", "Aaron", "Gavin", "Jackson", "Kyle", "Mason", "Juan", "Eric", "Charles", "Adam", "Brian"};
                List<String> femaleNames = new List<string> { "Mary", "Patricia", "Jennifer", "Elizabeth", "Linda", "Barbara", "Susan", "Margaret", "Jessica", "Dorothy", "Sarah", "Karen", "Nancy", "Betty", "Lisa", "Sandra", "Helen", "Ashley", "Donna", "Kimberly", "Carol", "Michelle", "Emily", "Amanda", "Melissa", "Deborah", "Laura", "Stephanie", "Rebecca", "Sharon", "Cynthia", "Kathleen", "Ruth", "Anna", "Shirley", "Amy", "Angela", "Virginia", "Brenda", "Pamela", "Catherine", "Katherine", "Nicole", "Christine" };
                List<String> address = new List<string> {"94104", "10022", "20005", "20036", "20001", "20006", "10019", "60611", "60614", "10021", "11733", "10024", "10023", "67201", "10075", "89109", "10065", "94111", "77024", "22101", "20007", "90067", "10128", "33480", "82922", "60045", "10106", "20004", "20008", "15222", "75205", "94301", "10028", "75219", "10017", "76102", "10011", "60093", "20003", "90210", "30327", "20815", "20854", "20910", "90049"};
                List<String> movies = new List<string> { "The Shawshank Redemption", "The Godfather", "The Godfather: Part II", "The Dark Knight", "Pulp Fiction", "Schindler's List", "12 Angry Men", "The Good, the Bad and the Ugly", "The Lord of the Rings: The Return of the King", "Fight Club", "The Lord of the Rings: The Fellowship of the Ring", "Star Wars: Episode V - The Empire Strikes Back", "Inception", "One Flew Over the Cuckoo's Nest", "The Lord of the Rings: The Two Towers", "Goodfellas", "The Matrix", "Star Wars: Episode IV - A New Hope", "Seven Samurai", "City of God", "Se7en", "The Usual Suspects", "The Silence of the Lambs", "Interstellar", "It's a Wonderful Life", "Léon: The Professional", "Life Is Beautiful", "Once Upon a Time in the West", "Casablanca", "American History X", "Saving Private Ryan", "Raiders of the Lost Ark", "Spirited Away", "City Lights", "Psycho", "Rear Window"};
                foreach(var eachMovie in movies){
                    dbContext.Likes.Add(new Likes { Movie = eachMovie });
                }
               
                for (int i = 1; i <= count; i++)
                {
                    Random rnd = new Random();
                    
                    string zip = address[rnd.Next(address.Count)];
                    bool gender = rnd.Next(100) < 50 ? true : false;
                    string firstName;
                    if (gender){
                        firstName = maleNames[rnd.Next(maleNames.Count)];
                    }
                    else{
                        firstName = femaleNames[rnd.Next(femaleNames.Count)];
                    }
                    string lastName = maleNames[rnd.Next(maleNames.Count)];
                    int age = rnd.Next(18, 100);
                    
                    bool interestedIn = !gender;
                    DateTime registarationDate = DateTime.Now.AddDays(rnd.Next(365));
                    String photo = gender ? "male.jpg" : "female.jpg";
                    User u = new User
                    {
                        Address = zip,
                        FirstName = firstName,
                        LastName = lastName,
                        Age = age,
                        Gender = gender,
                        Photo = photo,
                        Deleted = false,
                        InterestedIn = interestedIn,
                        RegistrationDate = registarationDate,
                        LastLoginDate = DateTime.Now,
                    };
                    dbContext.User.Add(u);

                    string movie = movies[rnd.Next(movies.Count)];
                    Likes l = dbContext.Likes.Where(p => p.Movie.Contains(movie)).FirstOrDefault();
                    u.Likes.Add(l);

                    dbContext.SaveChanges();
                }
            }
        }

        public List<User> queryUsers(int minAge, int maxAge, bool gender, string address, string SearcherID, UserSort sortBy )
        {
            string qryStr;
            string filteredUsers =
                "SELECT u.ID from dbo.Users as u where " +
                 "u.ID!='" + SearcherID + "' and u.Age >=" + minAge + " and u.Age<=" + maxAge +
              " and u.Gender = " + Convert.ToInt32(gender) + "  and u.Address = '" + address + "'";
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
                                                                  " dbo.LikesUsers.User_ID='" + SearcherID  + "')) as movies " +
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
