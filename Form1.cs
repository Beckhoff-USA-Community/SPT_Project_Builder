using System.ComponentModel;
using System.IO.Compression;
using System.Reflection;


namespace TcTemplateBuilder
{
    public partial class Form1 : Form {

        //List Declarations
        public List<Module> Modules { get; set; }//EMorC
        public List<Module> EMs { get; set; }//List for UI drop down
        public List<FunctionBlockDefinition> PredefinedFunctionBlocks { get; set; }
        public List<FunctionBlockDefinition> UserDefinedEMorC { get; set; }
        public List<TcLibrary> TcLibraries { get; set; }




        //Populate the lists on load
        private List<Module>? GetModules()
        {
            var modules = new List<Module>();
            modules.Add(new Module() { Type = TcType.Machine, Name = "Machine", Parent = "None" });
            modules.Add(new Module() { Type = TcType.Equipment, Name = "EquipmentModule1", Parent = "Machine" });
            modules.Add(new Module() { Type = TcType.Component, Name = "WeighScale", Parent = "EquipmentModule1" });
            modules.Add(new Module() { Type = TcType.Component, Name = "RotaryKnife", Parent = "EquipmentModule2" });
            modules.Add(new Module() { Type = TcType.Equipment, Name = "EquipmentModule2", Parent = "Machine" });
            modules.Add(new Module() { Type = TcType.Equipment, Name = "EquipmentModule3", Parent = "Machine" });

            return modules;
        }


        //Populate Drop down
        private List<Module>? GetEMs()
        {
            var parents = new List<Module>();

            //Add Machine as First Parent
            parents.Add(new Module() { Type = TcType.Machine, Name = "Machine", Parent = "None" });

            //Add all other EMs
            foreach (var module in Modules)
            {
                if (module.Type == TcType.Equipment)
                {
                    parents.Add(module);
                }
            }
            return parents;
        }


        //Edit the UI Grid
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            Modules.Add(new Module() { Type = textToEnumTcType(comboBoxType.Text), Name = textBox2.Text, Parent = comboBoxParent.Text });
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Modules;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;

            try
            {
                Module? selectedModule = dataGridView1.SelectedRows[0].DataBoundItem as Module;

                //Set the Type combo box
                comboBoxType.SelectedIndex = comboBoxType.Items.IndexOf(selectedModule.Type);

                //Set the Name text box
                textBox2.Text = selectedModule.Name;

                //Set the Parent combo box
                comboBoxParent.Text = selectedModule.Parent;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Some error occured:" + ex.Message + " - " + ex.Source);

            }
        }
        private void comboBoxParent_Click(object sender, EventArgs e)
        {
            EMs = GetEMs();
            var ems = this.EMs;
            comboBoxParent.DataSource = EMs;
            comboBoxParent.DisplayMember = "Name";
            comboBoxParent.ValueMember = "Name";
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                //MessageBox.Show(e.KeyValue.ToString());
                deleteSelectedRow();
            }
        }
        private void deleteSelectedRow()
        {

            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    Module selectedModule = dataGridView1.SelectedRows[0].DataBoundItem as Module;

                    dataGridView1.DataSource = null;
                    this.Modules.Remove(selectedModule);
                    dataGridView1.DataSource = this.Modules;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        //Move grid data to List
        private List<FunctionBlockDefinition>? GetDataGridInfo()
        {
            //var Components = new List<Module>();
            //var EquipmentModules = new List<Module>();
            var FunctionBlocks = new List<FunctionBlockDefinition>();

            string type = "Bad";
            string parentName = "Error";
            var folders = new List<string>();
            var methods = new List<Method>();

            foreach (Module module in Modules)
            {
                folders.Clear();
                methods.Clear();

                //switch (this.Modules[0].Type)
                switch (module.Type)
                {
                    case TcType.Machine:
                        type = "M";
                        parentName = "FB_PackML_BaseModule";
                        break;
                    case TcType.Equipment:
                        type = "E";
                        parentName = "FB_CustomModuleBase";
                        //folders2.Add("Acting States");
                        //folders2.Add("Waiting States");
                        break;
                    case TcType.Component:
                        type = "C";
                        parentName = "FB_ComponentBase";
                        break;
                }

                string name = "FB_" + module.Name;
                string interfaceName = "I_" + module.Name;
                string code = "";
                string methodCode = "IF NOT InitComplete THEN\r\t_InitComplete := Initialize();\r\tRETURN;\rEND_IF\r\rSUPER^.CyclicLogic();";
                methods.Add(new Method() { Folder = "", Name = "CyclicLogic", code = methodCode });
                methods.Add(new Method() { Folder = "", Name = "Initialize", code = "SUPER^.Initialize();" });

                //UserDefinedFunctionBlocks.Clear();
                FunctionBlockDefinition fb = new FunctionBlockDefinition();

                fb.Type = type;
                fb.Name = name;
                fb.parentName = parentName;
                fb.interfaceName = interfaceName;
                fb.code = code;
                fb.methods = methods;
                fb.folders = folders;

                FunctionBlocks.Add(fb);
            }
            return FunctionBlocks;
        }

        //Create project
        private List<TcLibrary>? GetTcLibrarires()
        {
            var libs = new List<TcLibrary>();
            libs.Add(new TcLibrary() { Name = "SPT Base Types", Version = "*", Company = "Beckhoff Automation LLC" });
            libs.Add(new TcLibrary() { Name = "SPT Components", Version = "*", Company = "Beckhoff Automation LLC" });
            libs.Add(new TcLibrary() { Name = "SPT Event Logger", Version = "*", Company = "Beckhoff Automation LLC" });
            libs.Add(new TcLibrary() { Name = "SPT Motion Control", Version = "*", Company = "Beckhoff Automation LLC" });
            libs.Add(new TcLibrary() { Name = "SPT Utilities", Version = "*", Company = "Beckhoff Automation LLC" });
            libs.Add(new TcLibrary() { Name = "Tc3_PackML_V2", Version = "*", Company = "Beckhoff Automation GmbH" });
            libs.Add(new TcLibrary() { Name = "Tc3_EventLogger", Version = "*", Company = "Beckhoff Automation GmbH" });
            libs.Add(new TcLibrary() { Name = "Tc2_MC2", Version = "*", Company = "Beckhoff Automation GmbH" });
            libs.Add(new TcLibrary() { Name = "Tc2_Utilities", Version = "*", Company = "Beckhoff Automation GmbH" });
            libs.Add(new TcLibrary() { Name = "Tc2_MC2_Drive", Version = "*", Company = "Beckhoff Automation GmbH" });
            libs.Add(new TcLibrary() { Name = "Tc3_JsonXml", Version = "*", Company = "Beckhoff Automation GmbH" });
            libs.Add(new TcLibrary() { Name = "Tc3_IPCDiag", Version = "*", Company = "Beckhoff Automation GmbH" });
            libs.Add(new TcLibrary() { Name = "Tc3_MC2_AdvancedHoming", Version = "*", Company = "Beckhoff Automation GmbH" });
            libs.Add(new TcLibrary() { Name = "Tc2_EtherCAT", Version = "*", Company = "Beckhoff Automation GmbH" });
            libs.Add(new TcLibrary() { Name = "Tc2_Math", Version = "*", Company = "Beckhoff Automation GmbH" });
            return libs;
        }
        private List<FunctionBlockDefinition>? GetPredefinedFunctionBlocks()
        {
            var customFunctionBlocks = new List<FunctionBlockDefinition>();

            //Definition for Custom Component Base
            string type = "C";
            string plcProjectName = "PLC_Template" + " Project";
            string name = "FB_CustomComponentBase";
            string parentName = "FB_ComponentBase";
            string interfaceName = "I_CustomComponentBase";
            string varInput = "VAR_INPUT\r\tTest : BOOL;\rEND_VAR";
            string varOutput = "";
            string localVar = "VAR\r\tTest2:BOOL;\rEND_VAR";
            string constantVar = "";
            string varInst = "";
            string code = "{attribute 'no_explicit_call' := 'This FB is a CLASS and must be accessed using methods or properties'}";
            var folders0 = new List<string>();
            var methods0 = new List<Method>();
            string methodCode = "IF NOT InitComplete THEN\r\t_InitComplete := Initialize();\r\tRETURN;\rEND_IF\r\rSUPER^.CyclicLogic();";
            methods0.Add(new Method() { Folder = "", Name = "CyclicLogic", code = methodCode });
            methods0.Add(new Method() { Folder = "", Name = "Initialize", code = "SUPER^.Initialize();" });

            customFunctionBlocks.Add(new FunctionBlockDefinition()
            {
                Type = type,
                plcProjectName = plcProjectName,
                Name = name,
                parentName = parentName,
                interfaceName = interfaceName,
                varInput = varInput,
                varOutput = varOutput,
                localVar = localVar,
                constantVar = constantVar,
                varInst = varInst,
                code = code,
                methods = methods0,
                folders = folders0
            });

            //Definition for Custom Equipment Module Base
            type = "E";
            plcProjectName = "PLC_Template" + " Project";
            name = "FB_CustomModuleBase";
            parentName = "FB_PackML_BaseModule";
            interfaceName = "I_CustomModuleBase";
            varInput = "";
            varOutput = "";
            localVar = "";
            constantVar = "";
            varInst = "";
            code = "{attribute 'no_explicit_call' := 'This FB is a CLASS and must be accessed using methods or properties'}";
            var folders1 = new List<string>();
            folders1.Add("Acting States");
            folders1.Add("Waiting States");
            var methods1 = new List<Method>();
            methods1.Add(new Method() { Name = "CyclicLogic", code = "IF NOT InitComplete THEN\r\t_InitComplete := Initialize();\r\tRETURN;\rEND_IF\r\rSUPER^.CyclicLogic();" });
            methods1.Add(new Method() { Name = "Initialize", code = "SUPER^.Initialize();" });
            methods1.Add(new Method() { Folder = "Acting States", Name = "Starting", code = "Super^.Starting();" });
            customFunctionBlocks.Add(new FunctionBlockDefinition()
            {
                Type = type,
                plcProjectName = plcProjectName,
                Name = name,
                parentName = parentName,
                interfaceName = interfaceName,
                varInput = varInput,
                varOutput = varOutput,
                localVar = localVar,
                constantVar = constantVar,
                varInst = varInst,
                code = code,
                methods = methods1,
                folders = folders1
            });

            return customFunctionBlocks;
        }
        private void btnCreateTc_Click(object sender, EventArgs e)
        {
            
            UserDefinedEMorC = GetDataGridInfo();

            //Pass GUI data to local vars
            string folderName = txtBoxPath.Text;
            string solutionName = txtBoxSln.Text;
            string projectName = txtBoxProjectName.Text;
            string plcName = txtBoxPLCName.Text;
            //string plcProjectName = plcName + " Project";

            //Test for Existing Project


            //Open TcXAE
            VisualStudioController.createVsInstance(false, true, true, "TcXaeShell.DTE.15.0");

            //Add Solution
            EnvDTE.Solution _solution;
            _solution = VisualStudioController.createVsSolution(folderName, solutionName);

            //Add Project
            TwinCATController.createTcProject(_solution, folderName, projectName);

            //Add PLC
            TwinCATController.createPlcProject(plcName);

            //Add Libraries
            foreach (TcLibrary tcLibrary in TcLibraries)
            {
                TwinCATController.addPlcLibrary(plcName, tcLibrary);
            }



            

            //Add Standard Folders
            string prjPath = plcName + " Project";
            string machinePath = prjPath + "^Machine";
            string mmPath = machinePath + "^" + "MachineModule";
            string emPath = machinePath + "^" + "EquipmentModules";
            string coPath = machinePath + "^" + "Components";

            TwinCATController.createFolder(plcName + " Project", "Machine");

            TwinCATController.createFolder(machinePath, "MachineModule");
            TwinCATController.createFolder(machinePath, "EquipmentModules");
            TwinCATController.createFolder(machinePath, "Components");


 

            //Add Predefined FBs
            //  FB_CustomComponentBase
            //TwinCATController.createComponentOrEM(PredefinedFunctionBlocks[0]);
            //  FB_CustomModuleBase
            TwinCATController.createComponentOrEM(PredefinedFunctionBlocks[1]);

            //Create User Defined FBs
            foreach (FunctionBlockDefinition EMorC in UserDefinedEMorC)
            {
                TwinCATController.createComponentOrEM(EMorC);
            }

            //Delete Standard Folders
            TwinCATController.deleteFolder(prjPath, "DUTs");
            TwinCATController.deleteFolder(prjPath, "GVLs");

           

           
            //Within MAIN
            //  Declare Machine
            //  Add Code

            //EM POUs
            //  Decalre Components within EMs
            //  Within Initialize Method
            //      Add Code for Register Component()

            //Create MM
            //  Declare EMs within MM
            //  Within Initialize Method
            //      Add Code for Register SubModule()

            //Declare MM within MAIN
            //      Add Code to call Machine.CyclicLogic

            MessageBox.Show("Enjoy your code!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        }






        //Unused
        private List<FunctionBlockDefinition>? GetUserDefinedComponent()
        {
            var userDefinedComponent = new List<FunctionBlockDefinition>();
            string type = "C";
            string plcProjectName = "PLC_Template" + " Project";
            string name = "FB_Component1";
            string parentName = "FB_CustomComponentBase";
            string interfaceName = "I_Component1";
            string varInput = "";
            string varOutput = "";
            string localVar = "";
            string constantVar = "";
            string varInst = "";
            string code = "{attribute 'no_explicit_call' := 'This FB is a CLASS and must be accessed using methods or properties'}";

            var methods = new List<Method>();
            methods.Add(new Method() { Name = "CyclicLogic", code = "IF NOT InitComplete THEN _InitComplete := Initialize(); RETURN; END_IF SUPER^.CyclicLogic();" });
            methods.Add(new Method() { Name = "Initialize", code = "SUPER^.Initialize();" });
            userDefinedComponent.Add(new FunctionBlockDefinition()
            { Type = type, plcProjectName = plcProjectName, Name = name, parentName = parentName, interfaceName = interfaceName, varInput = varInput, varOutput = varOutput, localVar = localVar, constantVar = constantVar, varInst = varInst, code = code, methods = methods });

            return userDefinedComponent;
        }
        private List<FunctionBlockDefinition>? GetUserDefinedSubModule()
        {
            var userDefinedSubModule = new List<FunctionBlockDefinition>();

            string type = "E";
            string plcProjectName = "PLC_Template" + " Project";
            string name = "FB_EquipmentModule1";
            string parentName = "FB_PackML_BaseModule";
            string interfaceName = "I_EquipmentModule1";
            string varInput = "";
            string varOutput = "";
            string localVar = "";
            string constantVar = "";
            string varInst = "";
            string code = "{attribute 'no_explicit_call' := 'This FB is a CLASS and must be accessed using methods or properties'}";
            var methods = new List<Method>();
            methods.Add(new Method() { Name = "CyclicLogic", code = "IF NOT InitComplete THEN _InitComplete := Initialize(); RETURN; END_IF SUPER^.CyclicLogic();" });
            methods.Add(new Method() { Name = "Initialize", code = "SUPER^.Initialize();" });

            userDefinedSubModule.Add(new FunctionBlockDefinition()
            { Type = type, plcProjectName = plcProjectName, Name = name, parentName = parentName, interfaceName = interfaceName, varInput = varInput, varOutput = varOutput, localVar = localVar, constantVar = constantVar, varInst = varInst, code = code, methods = methods });

            return userDefinedSubModule;
        }
 

        //Form
        public Form1() {
            Modules = GetModules(); //Default Modules for Data Grid
            TcLibraries = GetTcLibrarires();
            PredefinedFunctionBlocks = GetPredefinedFunctionBlocks();
            

            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("This project is still in Beta testing, but we would like your feedback and feature requests.", "Beta Version", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

            var modules = this.Modules;
            dataGridView1.DataSource = modules;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;


            comboBoxType.Items.Add("Equipment");
            comboBoxType.Items.Add("Component");

        }


        


        private TcType textToEnumTcType(string text) {
            switch (text) {
                case "Component":
                    return TcType.Component;
                case "Machine":
                    return TcType.Machine;
                case "Equipment":
                    return TcType.Equipment;
            }

            return TcType.Component;
        }



        

    }
}

