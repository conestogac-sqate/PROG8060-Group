using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROG8060_Group.Models
{
    public class MovieInfo
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Director { get; private set; }

        public string Genere { get; private set; }

        public string Cast { get; private set; }

        public int Year { get; private set; }

        public string Award { get; private set; }

        public bool IsOnShow { get; private set; }

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