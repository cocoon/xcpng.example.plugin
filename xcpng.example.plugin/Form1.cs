using GetVmRecords;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XenAPI;

namespace xcpng.example.plugin
{
    public partial class Form1 : Form
    {
        string[] args;

        Session session;

        string AppExeFullPath;
        string MasterHostUrl;
        string SessionRef;
        string ObjectClass;
        string ObjectUuid;


        public Form1()
        {
            InitializeComponent();

            args = Environment.GetCommandLineArgs();

            StringBuilder sb = new StringBuilder();

            if (args != null && args.Length > 0)
            {
                sb.Append("Plugin was called with the following parameters: " + Environment.NewLine);

                for (int i = 0; i < args.Length; i++)
                {
                    sb.Append("args[" + i + "]: " + args[i] + Environment.NewLine);
                }
            }
            else
            {
                sb.Append("[ERROR] Plugin was called without any parameters.");
            }

            t1.Text = sb.ToString();
        }


        private void ClearTextbox()
        {
            if (t1 != null) t1.Text = String.Empty;
        }


        private void buttonLogout_Click(object sender, EventArgs e)
        {
            ClearTextbox();
            if (session != null) session.logout();
        }

        private void buttonGetRecords_Click(object sender, EventArgs e)
        {
            ClearTextbox();

            if (args.Length >= 5)
            {
                AppExeFullPath = args[0];
                MasterHostUrl = args[1];
                SessionRef = args[2];
                ObjectClass = args[3];
                ObjectUuid = args[4];

                t1.Text += "args[0] = AppExeFullPath: " + AppExeFullPath + Environment.NewLine;
                t1.Text += "args[1] = MasterHostUrl: " + MasterHostUrl + Environment.NewLine;
                t1.Text += "args[2] = SessionRef: " + SessionRef + Environment.NewLine;
                t1.Text += "args[3] = ObjectClass: " + ObjectClass + Environment.NewLine;

                t1.Text += Environment.NewLine;

                t1.Text += "ObjectUuid: " + ObjectUuid + Environment.NewLine;

                //If selected object is a VM
                if (ObjectClass.ToLower() == "vm")
                {
                    t1.Text += "We are targeting a VM." + Environment.NewLine;
                }

                //If selected object is a Host
                if (ObjectClass.ToLower() == "host")
                {
                    t1.Text += "We are targeting a Host." + Environment.NewLine;
                }

                if (String.IsNullOrEmpty(SessionRef))
                {
                    //The Session is null if we target a disconnected host only class is set
                    t1.Text += "We are targeting a disconnected host" + Environment.NewLine;
                }
                else if (!String.IsNullOrEmpty(MasterHostUrl))
                {
                    // Establish a session
                    // Trust all certificates: DO NOT USE IN PRODUCTION CODE!
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    try
                    {
                        session = new Session(MasterHostUrl, SessionRef);
                        if (session != null)
                        {
                            t1.Text += Environment.NewLine;
                            t1.Text += GetVariousRecords.Run(session);
                        }
                        else
                        {
                            t1.Text += "[ERROR] Session was null";
                        }
                    }
                    catch (Exception ex)
                    {
                        t1.Text += "[ERROR] " + ex.GetType() + Environment.NewLine + ex.ToString();
                    }

                }
            }
            else
            {
                if (t1 != null) t1.Text = "[ERROR] unexcpected args.Length: " + args.Length;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearTextbox();
        }
    }
}
