using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2_Example.Models
{
    public class MusicLabel
    {
        [Key] public int IdMusicLabel { get; set; }
        public string Name { get; set; }
    }

    public class Album
    {
        [Key] public int IdAlbum { get; set; }
        public string AlbumName { get; set; }
        public DateTime PublishDate { get; set; }
        [ForeignKey("Label")] public int IdMusicLabel { get; set; }
    }

    public class Track
    {
        [Key] public int IdTrack { get; set; }
        public string TrackName { get; set; }
        public float Duration { get; set; }
        [ForeignKey("Album")] public int IdMusicAlbum { get; set; }
    }

    public class Musician_Track
    {
        [Key] public int IdMusicianTrack { get; set; }
        [ForeignKey("Track")] public int IdTrack { get; set; }
        [ForeignKey("Musician")] public int IdMusician { get; set; }
    }

    public class Musician
    {
        [Key] public int IdMusician { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
    }

}