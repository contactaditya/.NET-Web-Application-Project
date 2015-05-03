using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Messaging
    {
        YATContext db = new YATContext();

        public IQueryable<Message> getConversation(string userID, string otherID)
        {

            IQueryable<Message> query = ((from message
                         in db.Messages
                                          where message.ToId == userID && message.FromId == otherID
                                          select message)
                         .Union
                         (from message in db.Messages
                          where message.FromId == userID && message.ToId == otherID
                          select message));
            query = query.OrderBy(o => o.Date);
            var result = from message in query select message;
            return result;
        }

        public void sendMessage(string toID, string fromID, string textToSend)
        {

            var message = new Message
            {
                Date = DateTime.Now,
                Text = textToSend,
                FromId = fromID,
                ToId = toID,
                Read = false
            };

            db.Messages.Add(message);
            db.SaveChanges();
        }

        public IQueryable<Message> getInbox(string userID)
        {
            IQueryable<Message> query = from message
                                        in db.Messages
                                        where message.ToId == userID || message.FromId == userID
                                        select message;
            query = query.OrderBy(o => o.Date);
            return query;
        }

        //I add userID here since I am not sure how we want to do this, userID can be removed later maybe
        //But if we just run read every time a user clicks a message then we need it since if it is from them,
        //read should not be marked true...
        public void read(string userID, int messageID)
        {
            IQueryable<Message> query = from message
                        in db.Messages
                                        where message.Id == messageID && message.To.Id == userID
                                        select message;
            //only one should exist
            foreach (Message message in query.ToList())
            {
                message.Read = true;
                db.SaveChanges();
            }
        }

        public int getUnread(string id)
        {
            {

                IQueryable<Message> query = from message
                                            in db.Messages
                                            where message.From.Id == id
                                            select message;
                return query.Count();
            }
        }
    }
}
