using System;
using System.Collections.Generic;

namespace WebAPI
{
    public partial class SportDescr
    {
        public SportDescr()
        {
            Matches = new HashSet<Match>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}