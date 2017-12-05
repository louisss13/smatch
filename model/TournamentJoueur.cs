namespace model
{
    public class TournamentJoueur
    {
        public long TournamentId { get; set; }
        public long UserInfoId { get; set; }
        public UserInfo User { get; set; }
    }
}