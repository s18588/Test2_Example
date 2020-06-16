using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Test2_Example.DTOs.Response;
using Test2_Example.Models;

namespace Test2_Example.Services
{
    public class SqlMusicDbService : ControllerBase, IMusicDbService
    {

        private readonly MusicDbContext _context;

        public SqlMusicDbService(MusicDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable GetLabelInfo(int id)
        {
            
            
                if (_context.MusicLabel.Any(e => e.IdMusicLabel == id))
                {
                    var labelResponse = from o in _context.MusicLabel
                        where o.IdMusicLabel == id
                        select new LabelResponse()
                        {
                            IdMusicLabel = o.IdMusicLabel,
                            Name = o.Name,
                            AlbumList = (from a in _context.Albums
                                where a.IdMusicLabel == id
                                select new Album()
                                {
                                    IdAlbum = a.IdAlbum,
                                    AlbumName = a.AlbumName,
                                    PublishDate = a.PublishDate
                                }).ToList()
                        };
                    return labelResponse;
                }
                else
                {
                    throw new Exception("Label with given ID doesn't exist!");
                }    
                

            return null;
        }

        public IActionResult DeleteMusician(int id)
        {
            if (_context.Musician.Any(c => c.IdMusician == id))
            {
                var ListOfSongsOnAlbums = (from o in _context.Musician
                        join m_t in _context.Musician_Track on o.IdMusician equals m_t.IdMusician
                        join t in _context.Tracks on m_t.IdTrack equals t.IdTrack
                        where (o.IdMusician == id && t.IdMusicAlbum != 0)
                        select t
                    ).ToList();
                if (ListOfSongsOnAlbums.Count > 0)
                {
                    throw new Exception("Artist already has tracks appearing on albums!");
                }
                else
                {
                    var musician = _context.Musician.Where(m => m.IdMusician == id).FirstOrDefault();
                    var res = new MusicianDeleteResponse()
                    {
                        IdMusician = id,
                        FirstName = musician.FirstName,
                        LastName = musician.LastName,
                        NickName = musician.Nickname,
                        DeletedTracks = new List<Track>()
                    };
                    
                    var MusicianTrack = _context.Musician_Track.Where(e => e.IdMusician == musician.IdMusician).ToList();
                    foreach (var m_t in MusicianTrack)
                        _context.Musician_Track.Remove(m_t);
                    var ListOfSongsNotOnAlbums = (from o in _context.Musician
                        join m_t in _context.Musician_Track on o.IdMusician equals m_t.IdMusician
                        join t in _context.Tracks on m_t.IdTrack equals t.IdTrack
                        where (o.IdMusician == id && t.IdMusicAlbum == 0)
                        select t).ToList();

                    foreach (var track in ListOfSongsNotOnAlbums)
                    {
                        res.DeletedTracks.Add(track);
                        MusicianTrack = _context.Musician_Track.Where(e => e.IdTrack == track.IdTrack).ToList();
                        foreach (var m_t in MusicianTrack)
                            _context.Musician_Track.Remove(m_t);
                        _context.Tracks.Remove(track);
                        _context.SaveChanges();
                    }
                    
                    
                    _context.Musician.Remove(musician);
                    _context.SaveChanges();
                    return Ok(res);
                }
                
            }
            else
            {
                throw new Exception("Artist doesn't exist!");
            }
        }
    }
}