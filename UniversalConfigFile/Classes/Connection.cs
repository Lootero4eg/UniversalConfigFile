using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UniversalConfigFile.Classes
{
    public class Connection
    {
        public string ServerName { get; set; }
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
        public string DatabaseName { get; set; }
        public string ApplicationName { get; set; }
        public bool IsWindowsAuth { get; set; }

        public Connection(){ }        
    }
}
