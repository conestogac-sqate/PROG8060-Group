using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROG8060_Group.Models
{
    public class MovieInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Director { get; set; }

        public string Genere { get; set; }

        public string Cast { get; set; }

        public int Year { get; set; }

        public string Award { get; set; }

        public bool IsOnShow { get; set; }

        public MovieInfo() { }

        public MovieInfo(string title, string director, string genere, string cast, int year, string award)
        {
            this.Title = title;
            this.Director = director;
            this.Genere = genere;
            this.Cast = cast;
            this.Year = year;
            this.Award = award;
        }

        public MovieInfo(int id, string title, string director, string genere, string cast, int year, string award)
        {
            this.Id = id;
            this.Title = title;
            this.Director = director;
            this.Genere = genere;
            this.Cast = cast;
            this.Year = year;
            this.Award = award;
        }

        public MovieInfo(int id, string title, string director, string genere, string cast, int year, string award, bool isOnShow)
        {
            this.Id = id;
            this.Title = title;
            this.Director = director;
            this.Genere = genere;
            this.Cast = cast;
            this.Year = year;
            this.Award = award;
            this.IsOnShow = isOnShow;
        }
    }
}