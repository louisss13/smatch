namespace model
{
    public class ClubMember
    {
        public int UserInfoId { get; set; }
        public int ClubId { get; set; }
        public UserInfo UserInfo { get; set; }
        public Club Club { get; set; }
    }
}