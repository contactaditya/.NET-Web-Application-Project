using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Users
    {
        public IList<Likes> getUserLikes(string userID)
        {
            IList<Likes> likes = null;
            using (var dbContext = new YATContext())
            {
                likes = dbContext.User.Where(p => p.Id.Equals(userID)).First().Likes.ToList();
            }
            return likes;
        }

    }
}
