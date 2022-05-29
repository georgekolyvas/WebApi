using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.ApiModels
{
    public class ApiMatch
    {
        public int Id { get; set; }
        public string Description { get; set; }
        
        public DateTime MatchDate { get; set; }
        
        [Range(0, 23)]
        public int Hour{ get; set; }

        [Range(0, 59)]
        public int Minutes { get; set; }
        
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public int Sport { get; set; }
    }
}
