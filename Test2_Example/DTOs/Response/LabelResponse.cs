using System.Collections.Generic;
using Test2_Example.Models;

namespace Test2_Example.DTOs.Response
{
    public class LabelResponse
    {
        public int IdMusicLabel { get; set; }
        public string Name { get; set; }
        public List<Album> AlbumList { get; set; }
    }
}