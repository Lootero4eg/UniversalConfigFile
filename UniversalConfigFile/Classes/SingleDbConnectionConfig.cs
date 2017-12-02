using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalConfigFile.Classes
{
    public class SingleDbConnectionConfig: BaseConfig
    {
        public Connection Connection { get; set; }

        public SingleDbConnectionConfig() { }

        public void CreateConnection
        (
            string serverName,
            string login,
            string password,
            string databaseName,
            string appName,
            bool isWindowsAuth
        )
        {
            Connection = new Connection();
            Connection.ServerName = serverName;
            Connection.Login = login;
            Connection.EncodedPassword = anvlib.Crypt.BaseEncryptor<System.Security.Cryptography.DESCryptoServiceProvider>.Encrypt(password);
            Connection.DatabaseName = databaseName;
            Connection.ApplicationName = appName;
            Connection.IsWindowsAuth = isWindowsAuth;
        }
    }
}
