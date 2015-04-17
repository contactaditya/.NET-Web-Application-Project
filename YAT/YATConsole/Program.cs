using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using System.Data.Entity;
using DataLayer;

namespace YATConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Builder b = new Builder();

            User paul;
            using (var db = new YATContext())
            {
                paul = db.User.Where(p => p.FirstName.Contains("Paul")).FirstOrDefault();
            }
            if (paul == null) 
            {
                //we don't want to keep populating fake data so our test queries have the same results
                b.putData();
            }

             b.getData();
            
            Analytics a = new Analytics();
            a.movieRank();
            a.genderCount();


            Console.WriteLine("\nDone!");
            Console.ReadKey();
        }
    }
}
