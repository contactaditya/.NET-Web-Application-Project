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

        public IQueryable<String> getConversation(int userID, int otherID)
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
            var result = from message in query select message.Text;
            return result;
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

        //I add userID here since I am not sure how we want to do this, userID can be removed later maybe
        //But if we just run read every time a user clicks a message then we need it since if it is from them,
        //read should not be marked true...
        public void read(int userID, int messageID) 
        {
            IQueryable<Message> query = from message 
                        in db.Messages 
                        where message.Id == messageID && message.To.Id == userID 
                        select message;
            //Should only exist one
            foreach (Message message in query)
            {
                message.Read = true;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                //Write to console for now...
                Console.WriteLine(e);
            }
        }
    }
}
