namespace model
{
    public class TournamentAdmin
    {
        public int TournamentId { get; set; }
        public int UserId { get; set; }
        public Tournament Tournament { get; set; }
        public User User { get; set; }
    }
}