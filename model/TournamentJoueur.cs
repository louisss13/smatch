namespace model
{
    public class TournamentJoueur
    {
        public int TournamentId { get; set; }
        public int UserInfoId { get; set; }
        public Tournament Tournament { get; set; }
        public UserInfo User { get; set; }
    }
}