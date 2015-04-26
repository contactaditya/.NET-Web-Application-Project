using DataLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Analytics
    {
        public IEnumerable<StringRow> movieRank()
        {
            using (var db = new YATContext())
            {
                var rows = from movie in db.Likes
                                          orderby movie.Users.Count
                           select new StringRow
                           {
                               name = movie.Movie.ToString(),
                               value = movie.Users.Count
                           };
                foreach (var row in rows)
                {
                    Console.WriteLine("Movie: {0} {1}", row.name, row.value);
                }

                return rows.ToList();
            }
        }

        public IEnumerable<BoolRow> genderCount()
        {
            using (var db = new YATContext())
            {
                var rows = from user in db.User
                           group user by user.Gender into tempTable
                           select new BoolRow { name = tempTable.Key, value = tempTable.Count() };
                foreach (var row in rows)
                {
                    Console.WriteLine("Gender: {0} {1}", row.name, row.value);
                }
                return rows.ToList();
            }
        }

        public IEnumerable<IntRow> ageRank()
        {
            using (var db = new YATContext())
            {
                var rows = from user in db.User
                           group user by user.Age into tempTable
                           select new IntRow { name = tempTable.Key, value = tempTable.Count() };
                foreach (var row in rows)
                {
                    Console.WriteLine("Age: {0} {1}", row.name, row.value);
                }
                return rows.ToList();
            }
        }

        public IEnumerable<IntRow> registrationMonths()
        {
            using (var db = new YATContext())
            {
                var rows = from user in db.User
                           group user by user.RegistrationDate.Month into tempTable
                           select new IntRow { name = tempTable.Key, value = tempTable.Count() };
                foreach (var row in rows)
                {
                    Console.WriteLine("Month: {0} {1}", DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(row.name), row.value);
                }
                return rows.ToList();
            }
        }
        public IEnumerable<IntRow> zipCount()
        {
            using (var db = new YATContext())
            {
                var rows = from user in db.User
                           group user by user.Zip into tempTable
                           select new IntRow { name = tempTable.Key, value = tempTable.Count() };
                foreach (var row in rows)
                {
                    Console.WriteLine("Zip: {0} {1}", row.name, row.value);
                }
                return rows.ToList();
            }
        }

    }

    public class StringRow
    {
        public string name { get; set; }
        public int value {get;set;}
    }
    public class IntRow
    {
        public int name { get; set; }
        public int value { get; set; }
    }
    public class BoolRow
    {
        public bool name {get; set; }
        public int value { get; set; }
    }
}
