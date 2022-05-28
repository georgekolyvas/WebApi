using System;
using System.Collections.Generic;

namespace WebAPI
{
    public partial class DbSportDescr
    {
        public DbSportDescr()
        {
            Matches = new HashSet<DbMatch>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<DbMatch> Matches { get; set; }
    }
}