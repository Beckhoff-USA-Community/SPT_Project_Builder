using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TcTemplateBuilder.VisualStudioController;
using TCatSysManagerLib;
using VSLangProj;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace TcTemplateBuilder
{
    public class TwinCATController
    {
        static EnvDTE.Project _project;
        static ITcSysManager3 _systemManager;
        static ITcProjectRoot _projectRoot;
        static string plcPath;
        public static void createTcProject(EnvDTE.Solution _solution, string folderName, string PrjName)
        {
            MessageFilter.Register();

            string TCTEMPLATEPATH = @"C:\TwinCAT\3.1\Components\Base\PrjTemplate\TwinCAT Project.tsproj";


            try
            {
                _project = _solution.AddFromTemplate(TCTEMPLATEPATH, folderName, PrjName);
                _systemManager = (ITcSysManager3)_project.Object;
            }
            catch (Exception  e)
            {
                MessageBox.Show(e.Message, "Beta Version", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                throw;
            }

            MessageFilter.Revoke();
         }
        public static void createPlcProject(string plcName)
        {
            MessageFilter.Register();

            ITcSmTreeItem plc = _systemManager.LookupTreeItem("TIPC");
            ITcSmTreeItem newProject = plc.CreateChild(plcName, 0, "", "Standard PLC Template");
            _projectRoot = (ITcProjectRoot)newProject;
            plcPath = _projectRoot.NestedProject.Name;

            MessageFilter.Revoke();
        }

        public static void addPlcLibrary(string plcName, TcLibrary tcLibrary)
        {
            MessageFilter.Register();
            try
            {
                ITcSmTreeItem lib = _systemManager.LookupTreeItem("TIPC^" + plcName + " Project^References");
                ITcPlcLibraryManager libMgr = (ITcPlcLibraryManager)lib;
                libMgr.AddLibrary(tcLibrary.Name, tcLibrary.Version, tcLibrary.Company);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Beta Version", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                throw;
            }

            MessageFilter.Revoke();
        }

        public static void createFolder(string path, string name)
        {
            MessageFilter.Register();

            ITcSmTreeItem folder = _systemManager.LookupTreeItem("TIPC^" + path);
            folder.CreateChild(name, 601);

            MessageFilter.Revoke();
        }
  
        public static void createComponentOrEM(FunctionBlockDefinition fb)
        {
            string path = plcPath + "^Machine";
            if (fb.Type == "M")
            {
                path += "^MachineModule";
            }
            if (fb.Type == "E")
            {
                path += "^EquipmentModules";
            }
            else if (fb.Type == "C")
            {
                path += "^Components";
            }

            createAndAddFB(path, fb);
        }

        public static void createAndAddFB(string path, FunctionBlockDefinition fb)
        {
            string folderName;
            if (fb.Type != "M")
            {
                folderName = fb.Name.Replace("FB_", "");//Use fb Name without FB_ for the folder name
                createFolder(path, folderName);
                path += "^" + folderName;
            }
            else
            {
                folderName = "";
                path += folderName;
            }

            createFB(path, fb.Name);

            string interfaceName = fb.Name.Replace("FB", "I");
            createInterface(path, interfaceName);

            path += "^" + fb.Name;
            addCode(path, fb.code);

            addParentAndInterface(path, fb);

            if (fb.varInput != "")
            {
                addVarDeclartion(path, fb.varInput);
            }
            if (fb.varOutput != "")
            {
                addVarDeclartion(path, fb.varOutput);
            }
            if (fb.localVar != "")
            {
                addVarDeclartion(path, fb.localVar);
            }
            if (fb.varInst != "")
            {
                addVarDeclartion(path, fb.varInst);
            }
            if (fb.constantVar != "") 
            {
                addVarDeclartion(path, fb.constantVar);
            }


            createFolder(path, "Methods");
            path += "^" + "Methods";
            foreach (string folder in fb.folders)
            {
                createFolder(path, folder);
            }


            string methodPath;
            string methodFolderPath;
            foreach (Method method in fb.methods)
            {
                if ((method.Folder == "") || (method.Folder == null))
                {
                    methodFolderPath = path;
                }
                else
                {
                    methodFolderPath = path + "^" + method.Folder;
                }
                
                addMethod(methodFolderPath, method.Name);

                methodFolderPath += "^" + method.Name;
                addCode(methodFolderPath, method.code);
            }
        }

        public static void createFB(string path, string name)
        {
            MessageFilter.Register();

            ITcSmTreeItem POUs = _systemManager.LookupTreeItem("TIPC^" + path);
            POUs.CreateChild(name, 604);

            MessageFilter.Revoke();
        }

        public static void addParentAndInterface(string path, FunctionBlockDefinition fbDef)
        {
            MessageFilter.Register();

            ITcSmTreeItem fb = _systemManager.LookupTreeItem("TIPC^" + path);
            ITcPlcDeclaration fb_decl = (ITcPlcDeclaration)fb;

            string parentName = "";
            string interfaceName = "";
            if (fbDef.parentName != "")
            {
                parentName = " EXTENDS " + fbDef.parentName;
            }
            if (fbDef.interfaceName != "")
            {
                interfaceName = " IMPLEMENTS " + fbDef.interfaceName;
            }

            string code = "{attribute 'no_explicit_call' := 'This FB is a CLASS and must be accessed using methods or properties'}";

            fb_decl.DeclarationText = code + "\r" + @"FUNCTION_BLOCK " + fbDef.Name + parentName + interfaceName;

            MessageFilter.Revoke();
        }

        public static void addMethod(string path, string methodName)
        {
            MessageFilter.Register();

            string[] vInfo = new string[3]; 

            if (methodName == "Initialize")
            {
                vInfo[1] = "BOOL";
                vInfo[2] = "PROTECTED";
            }
            else
            {
                vInfo[1] = "";
                vInfo[2] = "";
            }

            ITcSmTreeItem POUs = _systemManager.LookupTreeItem("TIPC^" + path);
            POUs.CreateChild(methodName, 609, "", vInfo);

            MessageFilter.Revoke();
        }

        public static void addCode(string path, string code)
        {
            MessageFilter.Register();

            ITcSmTreeItem fb = _systemManager.LookupTreeItem("TIPC^" + path);
            ITcPlcImplementation fbImpl = (ITcPlcImplementation)fb;
            fbImpl.ImplementationText = code;

            MessageFilter.Revoke();
        }

        public static void createInterface(string path, string name)
        {
            MessageFilter.Register();

            //name = name.Replace("FB", "I");
            ITcSmTreeItem POUs = _systemManager.LookupTreeItem("TIPC^" + path);
            POUs.CreateChild(name, 618);

            MessageFilter.Revoke();
        }

        public static void addVarDeclartion(string path, string var)
        {
            MessageFilter.Register();

            ITcSmTreeItem fb = _systemManager.LookupTreeItem("TIPC^" + path);
            ITcPlcDeclaration fb_decl = (ITcPlcDeclaration)fb;
            
            //Read current declaration section
            string currentDeclaration = fb_decl.DeclarationText;

            //Rewrite it all
            fb_decl.DeclarationText = currentDeclaration + "\r" + var;

            MessageFilter.Revoke();
        }

 

        public static void deleteFolder(string path, string name)
        {
            MessageFilter.Register();

            ITcSmTreeItem folder = _systemManager.LookupTreeItem("TIPC^" + path);
            folder.DeleteChild(name);

            MessageFilter.Revoke();
        }
    }
}
