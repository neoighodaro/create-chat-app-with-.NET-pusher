using System;
namespace HeyChat.Models
{
    public class Conversation
    {
        public Conversation()
        {
            status = messageStatus.Sent;
        }

        public enum messageStatus
        {
            Sent, 
            Delivered
        }

        public int id { get; set; }
        public int sender_id { get; set; }
        public int receiver_id { get; set; }
        public string message { get; set; }
        public messageStatus status { get; set; }
        public DateTime created_at { get; set; }
    }
}
