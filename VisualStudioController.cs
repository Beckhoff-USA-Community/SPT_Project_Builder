using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using TCatSysManagerLib;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using EnvDTE90;



namespace TcTemplateBuilder
{
    public class VisualStudioController
    {
        public VisualStudioController() { }

        static EnvDTE.DTE _dte;
        static EnvDTE.Solution _solution;
       
        public static void createVsInstance(bool suppressUI, bool userControl, bool visible, string VSVersion)
        {
            Type t = System.Type.GetTypeFromProgID(VSVersion);
            _dte = (EnvDTE.DTE)System.Activator.CreateInstance(t);
            _dte.SuppressUI = suppressUI;
            _dte.UserControl = userControl; // true = leaves VS window open after code execution
            _dte.MainWindow.Visible = visible;
        }

        public static EnvDTE.Solution createVsSolution(string folderName, string slnName)
        {
            Directory.CreateDirectory(folderName);

            _solution = _dte.Solution;
            _solution.Create(folderName, slnName);
            _solution.SaveAs(folderName + @"\" + slnName);
            return _solution;
        }

 

        #region Message Filter
        public class MessageFilter : IOleMessageFilter
        {

            //
            // Class containing the IOleMessageFilter
            // thread error-handling functions.

            // Start the filter.
            public static void Register()
            {
                IOleMessageFilter newFilter = new MessageFilter();
                IOleMessageFilter oldFilter = null;
                CoRegisterMessageFilter(newFilter, out oldFilter);
            }


            // Done with the filter, close it.
            public static void Revoke()
            {
                IOleMessageFilter oldFilter = null;
                CoRegisterMessageFilter(null, out oldFilter);
            }


            //
            // IOleMessageFilter functions.
            // Handle incoming thread requests.
            int IOleMessageFilter.HandleInComingCall(int dwCallType,
            System.IntPtr hTaskCaller, int dwTickCount, System.IntPtr lpInterfaceInfo)
            {

                //Return the flag SERVERCALL_ISHANDLED.
                return 0;
            }


            // Thread call was rejected, so try again.
            int IOleMessageFilter.RetryRejectedCall(System.IntPtr
            hTaskCallee, int dwTickCount, int dwRejectType)
            {

                if (dwRejectType == 2)
                // flag = SERVERCALL_RETRYLATER.
                {
                    // Retry the thread call immediately if return >=0 & 
                    // <100.
                    return 99;
                }
                // Too busy; cancel call.
                return -1;
            }


            int IOleMessageFilter.MessagePending(System.IntPtr hTaskCallee,
            int dwTickCount, int dwPendingType)
            {
                //Return the flag PENDINGMSG_WAITDEFPROCESS.
                return 2;
            }


            // Implement the IOleMessageFilter interface.
            [DllImport("Ole32.dll")]
            private static extern int
              CoRegisterMessageFilter(IOleMessageFilter newFilter, out
          IOleMessageFilter oldFilter);
        }



        [ComImport(), Guid("00000016-0000-0000-C000-000000000046"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        interface IOleMessageFilter
        {
            [PreserveSig]
            int HandleInComingCall(
            int dwCallType,
            IntPtr hTaskCaller,
            int dwTickCount,
            IntPtr lpInterfaceInfo);

            [PreserveSig]
            int RetryRejectedCall(
            IntPtr hTaskCallee,
            int dwTickCount,
            int dwRejectType);


            [PreserveSig]
            int MessagePending(
                IntPtr hTaskCallee,
                int dwTickCount,
                int dwPendingType);
        }

        #endregion
    }
}
