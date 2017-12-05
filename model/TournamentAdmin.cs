namespace model
{
    public class TournamentAdmin
    {
        public long TournamentId { get; set; }
        public string AccountId { get; set; }
        public Account User { get; set; }
    }
}