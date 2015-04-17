using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using System.Data.Entity;
using DataLayer;
using System.Diagnostics;
namespace ConsoleApplication1
{
    class Program
    {
    
      

        static void Main(string[] args)
        {
            //using (var dbContext = new YATContext())
            //{
            //    User Ariel = dbContext.User.FirstOrDefault(u => u.FirstName == "DEX");
            //    Likes movie = dbContext.Likes.FirstOrDefault(L => L.Movie == "Avengers");
            //    Console.WriteLine(movie.Movie + " " + movie.Id);
            //    Ariel.Likes.Add(movie);
            //    dbContext.SaveChanges();
            //}


            //Work on this
            Builder builder = new Builder();
            string test = builder.queryUsersTEXT(minAge: 20, maxAge: 30, gender: false, zipcode: 11791, SearcherID: 1);
            Console.WriteLine(test);
            Debug.WriteLine(test);


            List<User> users = builder.queryUsers(minAge: 20, maxAge: 30, gender: false, zipcode: 11791, SearcherID: 1);
            foreach (User user in users)
            {
                Console.WriteLine(user.FirstName + " " + user.LastName);

            }

                Console.WriteLine("Done!");
                Console.ReadKey();
        }
    }
}
