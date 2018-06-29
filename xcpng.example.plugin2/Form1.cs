using GetVmRecords;
using mshtml;
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

namespace xcpng.example.plugin2
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

        private string PluginResult = String.Empty;


        public Form1()
        {
            InitializeComponent();

            StartUp();
        }

        private void StartUp()
        {
            ParseArgs();
            DisplayInfos();
            UpdateTitleAndCreateSession();
        }


        private void ParseArgs()
        {
            args = Environment.GetCommandLineArgs();

            if (args.Length >= 5)
            {
                AppExeFullPath = args[0];
                MasterHostUrl = args[1];
                SessionRef = args[2];
                ObjectClass = args[3];
                ObjectUuid = args[4];
            }
            else
            {
                if (t1 != null) t1.Text = "[ERROR] unexcpected args.Length: " + args.Length;
            }
        }

        private void DisplayInfos()
        {
            try
            {
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

                sb.Append(Environment.NewLine);

                sb.Append("args[0] = AppExeFullPath: " + AppExeFullPath + Environment.NewLine);
                sb.Append("args[1] = MasterHostUrl: " + MasterHostUrl + Environment.NewLine);
                sb.Append("args[2] = SessionRef: " + SessionRef + Environment.NewLine);
                sb.Append("args[3] = ObjectClass: " + ObjectClass + Environment.NewLine);

                sb.Append(Environment.NewLine);

                sb.Append("ObjectUuid: " + ObjectUuid + Environment.NewLine);

                t1.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        
        private async void StartCreateSession()
        {
            var progress = new Progress<string>(s => t1.Text += s);
            await System.Threading.Tasks.Task.Factory.StartNew(() => CreateSession(progress), TaskCreationOptions.LongRunning);
        }
        private void CreateSession(IProgress<string> progress)
        {
            // Establish a session
            // Trust all certificates: DO NOT USE IN PRODUCTION CODE!
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            try
            {
                if (String.IsNullOrEmpty(SessionRef))
                {
                    //The Session is null if we target a disconnected host only class is set
                    string ProgressString = "We are targeting a disconnected host" + Environment.NewLine;
                    progress.Report(ProgressString);
                }
                else if (!String.IsNullOrEmpty(MasterHostUrl))
                {
                    session = new Session(MasterHostUrl, SessionRef);
                }
                else
                {
                    string ProgressString = "[ERROR] MasterHostUrl was empty!" + Environment.NewLine;
                    progress.Report(ProgressString);
                }
            }
            catch (Exception ex)
            {
                //DisplayException(ex);
                progress.Report(ex.ToString());
            }
        }

        private void SetTitle(IProgress<string> progress)
        {
            try
            {
                if (session == null)
                {
                    var progressCreateSession = new Progress<string>(s => t1.Text += s);
                    CreateSession(progressCreateSession);
                }

                //If selected object is a VM
                if (ObjectClass.ToLower() == "vm")
                {
                    var vm = VM.get_by_uuid(session, ObjectUuid);
                    progress.Report(" (" + VM.get_name_label(session, vm.opaque_ref) + ")");
                }

                //If selected object is a Host
                if (ObjectClass.ToLower() == "host")
                {
                    var host = Host.get_by_uuid(session, ObjectUuid);
                    progress.Report(" (" + Host.get_name_label(session, host.opaque_ref) + ")");
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private async void UpdateTitleAndCreateSession()
        {
            var progress = new Progress<string>(s => this.Text += s);
            await System.Threading.Tasks.Task.Factory.StartNew(() => SetTitle(progress), TaskCreationOptions.LongRunning);
        }


        private void DisplayException(Exception ex)
        {
            t1.Text += "[ERROR] " + ex.GetType() + Environment.NewLine + ex.ToString();
        }


        private void ClearTextbox()
        {
            if (t1 != null) t1.Text = String.Empty;
        }

        private void ClearBrowser()
        {
            if (browser != null) browser.DocumentText = String.Empty;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            ClearTextbox();
            ClearBrowser();
            if (session != null) session.logout();
        }


        private string ParsePluginHostGuids(string PluginResponse)
        {
            string[] split = PluginResponse.Split('\'');
            if (split.Length >= 3)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<p><H2 style=\"color: blue; font-family: Arial, Helvetica, sans-serif; \">Host GUIDs found: </H2><br>" + Environment.NewLine);

                for (int i = 0; i < split.Length; i++)
                {
                    //MessageBox.Show("split[i]: " + split[i]);
                    if (split[i].Length > 2)
                    {
                        string[] split2 = split[i].Split(':');
                        if (split2.Length >= 2)
                        {
                            sb.Append("Parsed: <b>" + split2[1] + "</b><br>" + Environment.NewLine);
                        }
                        else
                        {
                            //sb.Append("PluginParsingError1" + Environment.NewLine + "split2.Length: " + split.Length + Environment.NewLine + PluginResponse);
                        }
                    }
                }
                sb.Append("</p>");

                return sb.ToString();
            }
            else
            {
                return "PluginParsingError2" + Environment.NewLine + "split.Length: " + split.Length + Environment.NewLine + PluginResponse;
            }

        }

        private void buttonGetRecords_Click(object sender, EventArgs e)
        {
            ClearTextbox();

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

            try
            {
                if (session == null) UpdateTitleAndCreateSession();

                if (session != null)
                {
                    t1.Text += Environment.NewLine;
                    //t1.Text += GetVariousRecords.Run(session);

                    Dictionary<string, string> PluginArgs = new Dictionary<string, string> { { "CustomPrefix", "My other custom prefix text" } };
                    Dictionary<string, string> PluginArgs2 = new Dictionary<string, string> { { "CustomPrefix", "HOST GUIDS: " } };

                    try
                    {
                        if (ObjectClass.ToLower() == "host")
                        {
                            var host = Host.get_by_uuid(session, ObjectUuid);
                            PluginResult = XenAPI.Host.call_plugin(session, host, "FcInfoScript", "FcInfo", PluginArgs) + "<br>" + Environment.NewLine;
                            string PluginResult2 = XenAPI.Host.call_plugin(session, host, "FcInfoScript", "HostGUIDs", PluginArgs2) + "<br>" + Environment.NewLine;
                            string parsed = ParsePluginHostGuids(PluginResult2);
                            PluginResult += parsed;
                        }
                    }
                    catch (Exception ex)
                    {
                        PluginResult = "[ERROR] " + ex.ToString();
                    }

                    t1.Text = PluginResult;
                    browser.DocumentText = PluginResult;

                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearTextbox();
            ClearBrowser();
        }
    }
}
