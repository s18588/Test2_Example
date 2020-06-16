using System.Collections.Generic;
using Test2_Example.Models;

namespace Test2_Example.DTOs.Response
{
    public class MusicianDeleteResponse
    {
        public int IdMusician { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }

        public List<Track> DeletedTracks { get; set; }
    }
}