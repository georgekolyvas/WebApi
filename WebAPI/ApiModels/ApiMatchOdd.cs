using System.ComponentModel.DataAnnotations;

namespace WebAPI.ApiModels

{
    public class ApiMatchOdd
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string Specifier { get; set; }
        [Range(1, float.MaxValue)]
        public decimal Odd { get; set; }
    }
}
