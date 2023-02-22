using System;
using System.IO;
using Newtonsoft.Json;

namespace exam.data.userData
{
    public class UserData
    {
        private const string UserDataFileName = "userdata.json";

        public static UserDataModel Load()
        {
            if (!File.Exists(UserDataFileName))
            {
                return new UserDataModel();
            }

            try
            {
                var json = File.ReadAllText(UserDataFileName);
                return JsonConvert.DeserializeObject<UserDataModel>(json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load user data: {ex.Message}");
            }
        }

        public static void Save(UserDataModel userName)
        {
            try
            {
                var json = JsonConvert.SerializeObject(userName);
                File.WriteAllText(UserDataFileName, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save user data: {ex.Message}");
            }
        }
    }
}

