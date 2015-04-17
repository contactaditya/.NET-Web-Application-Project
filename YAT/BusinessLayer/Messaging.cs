using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    class Messaging
    {
        YATContext db = new YATContext();

        public IQueryable<String> getConversation(int userID, int otherID)
        {

            IQueryable<String> query = (from message
                         in db.Messages
                                        where message.ToId == userID && message.FromId == otherID
                                        select message.Text)
                         .Union
                         (from message in db.Messages
                          where message.FromId == userID && message.ToId == otherID
                          select message.Text);
            return query;

        }

        public void sendMessage(int toID, int fromID, string textToSend)
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

        public IQueryable<Message> getInbox(int userID)
        {
            IQueryable<Message> query = from message
                                        in db.Messages
                                        where message.ToId == userID
                                        select message;
            return query;
        }
    }
}
