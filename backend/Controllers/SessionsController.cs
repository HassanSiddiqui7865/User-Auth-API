using backend.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vonage;
using Vonage.Request;
using Vonage.Common.Monads;
using Vonage.Video.Sessions.CreateSession;
using Vonage.Meetings.CreateRoom;
using Vonage.Meetings.GetRooms;
using System.Text.Json;
using Vonage.Video.Authentication;
using Vonage.Video.Sessions;
using Vonage.Video.ExperienceComposer;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : Controller
    {
        private IConfiguration _Configuration;
        string ApiKey = "dbaa60e3";
        string ApiSecret = "pyznq9mhscfe6Lp1";
        //string appId = "b172b950-ead4-4c49-90a3-e61ed510907a";
        //string privateKeyPath = "netvideo_app.key";
        string appId = "3a4b6ec4-efbc-4703-9707-ee0caa9a2a14";
        string privateKeyPath = "united_wounds.key";
        Credentials creds;
        VonageClient client;

        public SessionsController(IConfiguration config)
        {
            _Configuration = config;
            creds = Credentials.FromAppIdAndPrivateKeyPath(appId, privateKeyPath);
            client = new VonageClient(creds);

        }

        public class RoomForm
        {
            public string RoomName { get; set; }
        }


        public static Result<Vonage.Video.Sessions.CreateSession.CreateSessionRequest> request = CreateSessionRequest.Default;

        [HttpPost]
        [Route("CreateMeetingRoom")]
        public async Task<IActionResult> CreateMeetingRoom([FromBody] RoomForm roomForm)
        {

            var displayName = roomForm.RoomName;
            DateTime expiration = DateTime.Now.AddDays(7);
            var roomName = roomForm.RoomName;

            var request = CreateRoomRequest.Build()
                .WithDisplayName(displayName)
                .AsLongTermRoom(expiration)
                .WithApprovalLevel(Vonage.Meetings.Common.RoomApprovalLevel.ExplicitApproval)
                .Create();

            var response = await client.MeetingsClient.CreateRoomAsync(request);
            return Ok(response);
            //response.Match(
            //    successOperation => $"");




            //using (var db = new TMSBackupContext())
            //{
            //    var room = db.Rooms.Where(r => r.RoomName == roomName).FirstOrDefault();
            //    if (room != null)
            //    {
            //        sessionId = room.SessionId;
            //        token = opentok.GenerateToken(sessionId);
            //        room.Token = token;
            //        db.SaveChanges();
            //    }
            //    else
            //    {
            //        var session = opentok.CreateSession();
            //        sessionId = session.Id;
            //        token = opentok.GenerateToken(sessionId);
            //        var roomInsert = new Room
            //        {
            //            SessionId = sessionId,
            //            Token = token,
            //            RoomName = roomName
            //        };
            //        db.Add(roomInsert);
            //        db.SaveChanges();
            //    }

        }


        [HttpPost]
        [Route("GetRooms")]

        public async Task<IActionResult> Execute()
        {

            var request = GetRoomsRequest.Build().Create();
            Result<GetRoomsResponse> response = await client.MeetingsClient.GetRoomsAsync(request);

            var message = response.Match(
                success => $"Rooms retrieved: {success.Rooms}",
                failure => $"Rooms retrieval failed: {failure.GetFailureMessage()}");

            if (response.IsSuccess)
            {
                Debug.WriteLine(response.GetSuccessUnsafe().Rooms); //Rooms is a List of Vonage.Meetings.Common.Room

                //let's put that into a variable and set it's type so autocomplete has an easier time
                //getting it's members and methods when we loop
                List<Vonage.Meetings.Common.Room> rooms = response.GetSuccessUnsafe().Rooms;

                //you can iterate the rooms
                rooms.ForEach(room => {
                    new Room
                    {
                        RoomName = room.DisplayName,
                    };
                    //more members inside room
                });

                //let's return a json
                string jsonString = JsonSerializer.Serialize(rooms);
                return Ok(jsonString);
            }
            else
            {
                return BadRequest("Error");
            }


        }

        [HttpPost]
        [Route("GetSession")]
        public async Task<IActionResult> Sessions([FromBody] RoomForm roomForm)
        {
            
            var roomName = roomForm.RoomName;
            string sessionId;
            string token;

            using (var db = new TMSBackupContext())
            {
                var room = db.Rooms.Where(r => r.RoomName == roomName).FirstOrDefault();
                if (room != null)
                {
                    sessionId = room.SessionId;
                    //New way to generate token
                    VideoTokenGenerator tokenGenerator = new VideoTokenGenerator(); 
                    var tokenres = tokenGenerator.GenerateToken(creds, TokenAdditionalClaims.Parse(sessionId));
                    token = tokenres.GetSuccessUnsafe().Token;
                    room.Token = token;
                    db.SaveChanges();
                }
                else
                {
                    var request = CreateSessionRequest.Default;
                       
                    var response = await client.VideoClient.SessionClient.CreateSessionAsync(request);

                    if (response.IsSuccess)
                    {
                        sessionId = response.GetSuccessUnsafe().SessionId;
                    }
                    else
                    {
                        return BadRequest("Error");
                    }
                    //New way to generate token
                    VideoTokenGenerator tokenGenerator = new VideoTokenGenerator();
                    var tokenres = tokenGenerator.GenerateToken(creds, TokenAdditionalClaims.Parse(sessionId));
          
                    token = tokenres.GetSuccessUnsafe().Token;
                    var roomInsert = new Room
                    {
                        SessionId = sessionId,
                        Token = token,
                        RoomName = roomName
                    };
                    db.Add(roomInsert);
                    db.SaveChanges();
                }
            }
            return Json(new { sessionId = sessionId, token = token, appId = appId });

        }

        [HttpPost]
        [Route("create-session")]
        public async Task<IActionResult> CreateSession([FromBody] RoomForm roomForm)
        {
            string roomName = roomForm.RoomName;
            string sessionId = null;
            using (var db = new TMSBackupContext())
            {
                var room = db.Rooms.Where(r => r.RoomName == roomName).FirstOrDefault();
                if (room.SessionId != null)
                {
                    sessionId = room.SessionId;
                    VideoTokenGenerator token = new VideoTokenGenerator();
                    VideoToken token1 = new VideoToken();
                    var tokenres = token.GenerateToken(creds, TokenAdditionalClaims.Parse(sessionId));
                    if (tokenres.IsSuccess)
                    {
                        token1 = tokenres.GetSuccessUnsafe();
                    }
                    return Json(new { sessionId = room.SessionId, token = token1.Token , appId} );
                }
                else
                {
                    CreateSessionResponse sessioninfo = new CreateSessionResponse();
                    var request = CreateSessionRequest.Default;
                    var response = await client.VideoClient.SessionClient.CreateSessionAsync(request);
                    if (response.IsSuccess)
                    {
                        sessioninfo = response.GetSuccessUnsafe();
                    }
                    var roominsert = new Room
                    {
                        Id = room.Id,
                        RoomName = roomForm.RoomName,
                        SessionId = sessioninfo.SessionId,
                    };
                    db.Add(roominsert); db.SaveChanges();

                    return Ok(new {sessioninfo});
                }
            }

        }

        [HttpPost("create-token")]
        public async Task<IActionResult> CreateToken(string sessionId)
        {
            
            VideoTokenGenerator token = new VideoTokenGenerator();
            VideoToken token1 = new VideoToken();
            using (var db = new TMSBackupContext())
            {
                var session = db.Rooms.Where(r => r.SessionId == sessionId).FirstOrDefault();
                if (session.SessionId != null)
                {
                    //sessionId = room.SessionId;
                    //New way to generate token
                    //VideoTokenGenerator tokenGenerator = new VideoTokenGenerator();
                    var tokenres = token.GenerateToken(creds, TokenAdditionalClaims.Parse(sessionId));
                    if (tokenres.IsSuccess)
                    {
                        token1 = tokenres.GetSuccessUnsafe();
                    }
                    session.Token = token1.Token;
                    db.SaveChanges();
                    

                }
            }
            return Json(new { tokenfin = token1.Token , appId = appId , sessionfin = token1.SessionId});

        }
    }


   

}

           
