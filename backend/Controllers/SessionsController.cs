using backend.Model;
using Microsoft.AspNetCore.Mvc;
using OpenTokSDK;

namespace backend.Controllers
{
    public class SessionsController : Controller
    {
        private IConfiguration _Configuration;

        public SessionsController(IConfiguration config)
        {
            _Configuration = config;
        }

        public class RoomForm
        {
            public string RoomName { get; set; }
        }

        [HttpPost]
        public IActionResult GetSession([FromBody] RoomForm roomForm)
        {
            var apiKey = int.Parse(_Configuration["ApiKey"]);
            var apiSecret = _Configuration["ApiSecret"];
            var opentok = new OpenTok(apiKey, apiSecret);
            var roomName = roomForm.RoomName;
            string sessionId;
            string token;
            using (var db = new TMSBackupContext())
            {
                var room = db.Rooms.Where(r => r.RoomName == roomName).FirstOrDefault();
                if(room != null)
                {
                    sessionId = room.SessionId;
                    token = opentok.GenerateToken(sessionId);
                    room.Token = token;
                    db.SaveChanges();
                }
                else
                {
                    var session = opentok.CreateSession();
                    sessionId = session.Id;
                    token = opentok.GenerateToken(sessionId);
                    var roomInsert = new Room
                    {
                        SessionId = sessionId,
                        RoomName = roomName,
                        Token = token
                    };
                    db.Add(roomInsert);
                    db.SaveChanges();
                }
            }
            return Json(new { sessionId = sessionId, token = token, apiKey = _Configuration["ApiKey"] });
        }
    }
}
