using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcTemplateBuilder
{
    public class Method
    {
        public string Path { get; set; }
        public string Folder { get; set; }
        public string Name { get; set; }
        public string AccessModifier { get; set; }
        public string ReturnType { get; set; }
        public string varInput { get; set; }
        public string varOutput { get; set; }
        public string varLocal { get; set; }
        public string varConstant { get; set; }
        public string varInst { get; set; }
        public string code { get; set; }
    }
}
