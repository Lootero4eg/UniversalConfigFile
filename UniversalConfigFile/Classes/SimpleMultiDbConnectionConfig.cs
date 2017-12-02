using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalConfigFile.Classes
{
    public class SimpleMultiDbConnectionConfig: BaseConfig
    {
        public List<Connection> Connections { get; set; }

        public SimpleMultiDbConnectionConfig() { }
    }
}
