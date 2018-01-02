using model;
using System.Collections.Generic;

namespace smartch.PostModel
{
    public class AccountDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public ICollection<UserDTO> InfosUsers { get; set; }

        public AccountDTO(){}
        public AccountDTO(Account account)
        {
            Id = account.Id;
            Email = account.Email;
            InfosUsers = UserInfosToUserDtos(account.Infos);
        }

        public Account GetAccount()
        {
            return new Account()
            {
                Id = this.Id,
                Email = this.Email,
                Infos = UserDtoToUserInfo(InfosUsers)
            };
        }

        private ICollection<UserInfo> UserDtoToUserInfo(ICollection<UserDTO> usersDto)
        {
            if (usersDto == null) return null;
            ICollection<UserInfo> userInfos = new List<UserInfo>();
            foreach(UserDTO userDto in usersDto)
            {
                userInfos.Add(userDto.getUser());
            }
            return userInfos;
        }
        private ICollection<UserDTO> UserInfosToUserDtos(ICollection<UserInfo> users)
        {
            if (users == null) return null;
            ICollection<UserDTO> usersDto = new List<UserDTO>();
            foreach (UserInfo user in users)
            {
                usersDto.Add(new UserDTO(user, "STOP"));
            }
            return usersDto;
        }

    }
}