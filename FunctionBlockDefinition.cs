using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcTemplateBuilder
{
    public class FunctionBlockDefinition
    {
        public string Type { get; set; } //EM or Co
        public string Name { get; set; } //FB_Name also used for Containing folder name
        public string plcProjectName { get; set; }
        public string parentName { get; set; } //EXTENDS ???
        public string interfaceName { get; set; } //IMPLEMENTS ???
        public string varInput { get; set; }
        public string varOutput { get; set; }
        public string localVar { get; set; }
        public string constantVar { get; set; }
        public string varInst { get; set; }
        public string code { get; set; }
        public List<Method> methods { get; set; }
        public List<string> folders { get; set; }


    }
}
