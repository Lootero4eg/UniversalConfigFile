using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalConfigFile.Classes
{
    public class MultiDbConnectionConfig: BaseConfig
    {
        public Dictionary<string, Connection> Connections { get; set; }

        public MultiDbConnectionConfig() { }
    }
}
