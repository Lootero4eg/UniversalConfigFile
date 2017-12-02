using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UniversalConfigFile.Classes
{
    public class GlobalConfigLocation
    {
        public string Host { get; set; }
        public string Folder { get; set; }
        public GlobalLocationType LocationType { get; set; }
        public string Login { get; set; }
        public byte[] EncodedPassword { get; set; }
        [XmlIgnore]
        public string Password
        {
            get
            {
                return anvlib.Crypt.BaseEncryptor<System.Security.Cryptography.DESCryptoServiceProvider>.Decrypt(EncodedPassword);
            }
        }

        public GlobalConfigLocation() { }        
    }
}
