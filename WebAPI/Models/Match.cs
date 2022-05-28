using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI
{
    public partial class Match
    {
        public Match()
        {
            MatchOdds = new HashSet<MatchOdd>();
        }

        public int Id { get; set; }
        public string Description { get; set; }        
        public DateTime MatchDate { get; set; }
        public TimeSpan MatchTime { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public int Sport { get; set; }

        public virtual SportDescr SportNavigation { get; set; }
        public virtual ICollection<MatchOdd> MatchOdds { get; set; }
    }
}