using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Analytics
    {
        public IEnumerable<MovieRank> movieRank()
        {
            using (var db = new YATContext())
            {
                var rows = from movie in db.Likes
                                          orderby movie.Users.Count
                           select new MovieRank
                           {
                               movie = movie.Movie.ToString(),
                               rank = movie.Users.Count
                           };
                foreach (var row in rows)
                {
                    Console.WriteLine("{0} {1}", row.movie, row.rank);
                }

                return rows.ToList();
            }
        }

        public void genderCount()
        {
            using (var db = new YATContext())
            {
                var rows = from user in db.User
                           group user by user.Gender into tempTable
                           select new { gender = tempTable.Key, count = tempTable.Count() };
                foreach (var row in rows)
                {
                    Console.WriteLine("{0} {1}", row.gender, row.count);
                }
            }
        }
    }

    public class MovieRank
    {
        public string movie { get; set; }
        public int rank {get;set;}
    }
}
