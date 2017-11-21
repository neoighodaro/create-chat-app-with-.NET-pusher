using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeyChat.Models;
using PusherServer;

namespace HeyChat.Controllers
{
    public class AuthController : Controller
    {
		[HttpPost]
		public ActionResult Login()
		{
            
			string user_name = Request.Form["username"];

			if (user_name.Trim() == "") {
				return Redirect("/");
			}


            using (var db = new Models.ChatContext()) {

                User user = db.Users.FirstOrDefault(u => u.name == user_name);

                if (user == null) {
                    user = new User { name = user_name };

                    db.Users.Add(user);
                    db.SaveChanges();
                }

                Session["user"] = user;
            }

			return Redirect("/chat");
		}

        public JsonResult AuthForChannel(string channel_name, string socket_id)
        {
            if (Session["user"] == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }

            var currentUser = (Models.User)Session["user"];

            var channelData = new PresenceChannelData()
            {
                user_id = currentUser.id.ToString(),
                user_info = new {
                    id   = currentUser.id,
                    name = currentUser.name
                },
            };

			var options = new PusherOptions();
			options.Cluster = "PUSHER_APP_CLUSTER";

			var pusher = new Pusher(
			  "PUSHER_APP_ID",
			  "PUSHER_APP_KEY",
			  "PUSHER_APP_SECRET", options);

			var auth = pusher.Authenticate(channel_name, socket_id, channelData);
			
            return Json( auth );
        }
    }
}
