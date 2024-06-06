using System;
using System.Collections.Generic;

namespace backend.Model
{
    public partial class Room
    {
        public int Id { get; set; }
        public int? RoomId { get; set; }
        public string? SessionId { get; set; }
        public string? RoomName { get; set; }
        public string? Token { get; set; }
    }
}
