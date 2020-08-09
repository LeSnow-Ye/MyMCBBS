using System;
using System.Collections.Generic;

namespace MyMCBBS.Model
{
    [Serializable]
    public struct DataOfOneDay
    {
        public UserModel UserData;
        public DateTime DateTime;
    }

    [Serializable]
    public class UserDataModel
    {
        public List<DataOfOneDay> Datas = new List<DataOfOneDay>();
    }
}
