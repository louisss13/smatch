namespace model
{
    public class TournamentAdmin
    {
        public int TournamentId { get; set; }
        public int AccountId { get; set; }
        public Tournament Tournament { get; set; }
        public Account User { get; set; }
    }
}