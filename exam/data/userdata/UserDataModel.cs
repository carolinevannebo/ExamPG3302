using System;
namespace exam.data.json
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

