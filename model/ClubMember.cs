namespace model
{
    public class ClubMember
    {
        public int UserId { get; set; }
        public int ClubId { get; set; }
        public User User { get; set; }
        public Club Club { get; set; }
    }
}