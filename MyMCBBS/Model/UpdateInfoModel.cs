using System;
using System.Collections.Generic;

namespace MyMCBBS.Model
{
    [Serializable]
    public class UpdateInfoModel
    {
        public struct File
        {
            public string Url;
            public string Path;
        }

        public int Version { get; set; }
        public List<File> Files;
    }
}