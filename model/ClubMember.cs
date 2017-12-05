namespace model
{
    public class ClubMember
    {
        public long UserInfoId { get; set; }
        public long ClubId { get; set; }
        public UserInfo UserInfo { get; set; }
        
    }
}