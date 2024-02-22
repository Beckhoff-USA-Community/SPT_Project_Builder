using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcTemplateBuilder
{
    public enum TcType {
        Component,
        Machine,
        Equipment
    }

    public class Module
    {
        public TcType Type { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }

        
    }
}
