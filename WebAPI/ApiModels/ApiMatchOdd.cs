namespace WebAPI.ApiModels
{
    public class ApiMatchOdd
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string Specifier { get; set; }
        public decimal Odd { get; set; }
    }
}
