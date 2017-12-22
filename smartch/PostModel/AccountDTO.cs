using model;

namespace smartch.PostModel
{
    public class AccountDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public AccountDTO(){}
        public AccountDTO(Account account)
        {
            Id = account.Id;
            Email = account.Email;
        }

        public Account GetAccount()
        {
            return new Account()
            {
                Id = this.Id,
                Email = this.Email
            };
        }

    }
}