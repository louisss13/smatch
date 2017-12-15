using model;
using System;

namespace smartch.PostModel
{
    public class UserDTO
    {
        public long Id { get; set; }

        public String Name { get; set; }
        public String FirstName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public Address Adresse { get; set; }
        public DateTime Birthday { get; set; }
        public AccountDTO CreatedBy { get; set; }

        public UserDTO() { }
        public UserDTO(UserInfo userInfo)
        {
            Name = userInfo.Name;
            FirstName = userInfo.FirstName;
            Email = userInfo.Email;
            Phone = userInfo.Phone;
            Adresse = userInfo.Adresse;
            Birthday = userInfo.Birthday;
            CreatedBy = new AccountDTO(userInfo.CreatedBy);
        }
    }
}