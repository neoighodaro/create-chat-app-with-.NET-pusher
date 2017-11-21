using System;
using System.Collections.Generic;
namespace HeyChat.Models
{
    
    public class User
    {
        public User()
        {
        }

        public int id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
    }
}
