using DataLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Analytics
    {
        private static Dictionary<string, string> zipStates = new Dictionary<string, string>();
        public Analytics()
        {
            if (zipStates.Count == 0)
            {
                ZipStates.makeZipStates(zipStates);
            }
        }

        public IEnumerable<StringRow> movieRank()
        {
            using (var db = new YATContext())
            {
                var rows = (from movie in db.Likes
                           orderby movie.Users.Count
                           select new StringRow
                           {
                               name = movie.Movie.ToString(),
                               value = movie.Users.Count
                           } into selection
                           orderby selection.value descending
                           select selection).Take(30);
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
                           select new BoolRow { name = tempTable.Key, value = tempTable.Count() } 
                           into selection
                           orderby selection.value descending
                           select selection;
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
                           select new IntRow { name = tempTable.Key, value = tempTable.Count() } 
                           into selection
                           orderby selection.value descending
                           select selection;
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
                           select new IntRow { name = tempTable.Key, value = tempTable.Count() } 
                           into selection
                           orderby selection.value descending
                           select selection;
                foreach (var row in rows)
                {
                    Console.WriteLine("Month: {0} {1}", DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(row.name), row.value);
                }
                return rows.ToList();
            }
        }
        public IEnumerable<StringRow> stateCount()
        {
            using (var db = new YATContext())
            {
                var rows = from user in db.User
                           group user by user.Address into tempTable
                           select new { name = tempTable.Key, value = tempTable.Count() } 
                           into selection
                           orderby selection.value descending
                           select selection;               
                SortedDictionary<String, int> stateCount = new SortedDictionary<String, int>();
                foreach (var row in rows)
                {
                    if (stateCount.Keys.Contains(zipStates[row.name]))
                    {
                        stateCount[zipStates[row.name]] += row.value;
                    }
                    else{
                        stateCount[zipStates[row.name]] = row.value;
                    }
                }
                var newRows = from entry in stateCount 
                              select new StringRow {name = entry.Key, value = entry.Value}
                              into selection
                              orderby selection.value descending
                              select selection;
                foreach (var row in newRows)
                {
                    Console.WriteLine("State: {0} {1}", row.name, row.value);
                }
                return newRows;
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
