using System;

namespace exam.data.userData
{
    public class UserDataModel
    {
        public string? UserName { get; set; }

        public UserDataModel(string userName)
        {
            UserName = userName;
        }

        public UserDataModel() { }
    }
}

