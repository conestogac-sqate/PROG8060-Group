using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG8060_Group.Models
{
    public class SearchConfiguration
    {
        public enum OnShow
        {
            UNKNOWN = 0,
            YES = 1,
            NO = 2
        }

        public string Title { get; set; }

        public string Director { get; set; }

        public string Genre { get; set; }

        public string Cast { get; set; }

        public int Year { get; set; }

        public string Award { get; set; }

        public OnShow IsOnShow { get; set; }

        public SearchConfiguration() { }

        public SearchConfiguration(string title, string director, string genre, string cast, int year, string award, OnShow isOnShow)
        {
            Title = title;
            Director = director;
            Genre = genre;
            Cast = cast;
            Year = year;
            Award = award;
            IsOnShow = isOnShow;
        }
    }
}
