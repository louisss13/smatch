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
            Id = userInfo.Id;
            Name = userInfo.Name;
            FirstName = userInfo.FirstName;
            Email = userInfo.Email;
            Phone = userInfo.Phone;
            Adresse = userInfo.Adresse;
            Birthday = userInfo.Birthday;
            if (userInfo.CreatedBy != null )
                CreatedBy = new AccountDTO(userInfo.CreatedBy);
        }
        public UserDTO(UserInfo userInfo, String stop)
        {
            Id = userInfo.Id;
            Name = userInfo.Name;
            FirstName = userInfo.FirstName;
            Email = userInfo.Email;
            Phone = userInfo.Phone;
            Adresse = userInfo.Adresse;
            Birthday = userInfo.Birthday;
            if(userInfo.CreatedBy != null && stop == null)
                CreatedBy = new AccountDTO(userInfo.CreatedBy);
        }

        public UserInfo getUser() {
            return new UserInfo()
            {
                Id = this.Id,
                Name = this.Name,
                FirstName = this.FirstName,
                Email = this.Email,
                Phone = this.Phone,
                Adresse = this.Adresse,
                Birthday = this.Birthday,
                
                CreatedBy = (this.CreatedBy != null)?this.CreatedBy.GetAccount(): null
            };
        }
    }
}