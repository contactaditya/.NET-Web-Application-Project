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
        YATContext db = new YATContext();

        public IList<Likes> getUserLikes(int userID)
        {
            var likes = db.User.Where(p => p.Id.Equals(userID)).First().Likes.ToList();
            return likes;
        }

    }
}
