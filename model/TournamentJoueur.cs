namespace model
{
    public class TournamentJoueur
    {
        public int TournamentId { get; set; }
        public int UserId { get; set; }
        public Tournament Tournament { get; set; }
        public User User { get; set; }
    }
}