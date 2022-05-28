using System;
using System.Collections.Generic;

namespace WebAPI
{
    public partial class DbMatchOdd
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string Specifier { get; set; }
        public decimal Odd { get; set; }

        public virtual DbMatch Match { get; set; }
    }
}