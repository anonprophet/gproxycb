using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using Microsoft.Win32;
using CustomSockets;
using WC3;

namespace GMaster
{
    public partial class frmMain : Form
    {
        //// objects
        CTCPServer m_Server;
        CTCPServer m_GameServer;
        List<WC3C> m_Clients = new List<WC3C>();
        List<BaseConnectionProxy> m_Games = new List<BaseConnectionProxy>();
        string m_Channel;
        string m_InstallPath;

        public frmMain()
        {
            InitializeComponent();
        }

        private void CONSOLE_Print(string line)
        {
            Print.RtbPrint(ref rtbConsole,"["+DateTime.Now + "] " + line, Color.White, true);
        }
        private void Main_Load(object sender, EventArgs e)
        {
            m_Server = new CTCPServer(6112);
            m_GameServer = new CTCPServer((int)NumLPort.Value);
            CheckForGateaway();
            CheckForLoader();
            System.Net.WebClient i = new System.Net.WebClient();
        }
        private bool StartServer()
        {
            try
            {
                m_Server.Stop();
                m_Server = new CTCPServer(6112);
                m_Server.Start();
                m_Server.OnSocketConnectionRequest += new CTCPServer.OnSocketConnectionRequestHandler(m_Server_OnSocketConnectionRequest);
                m_Server.BeginAcceptingConnections();
                CONSOLE_Print("Listening for warcraft connections on port 6112");
                m_GameServer.Stop();
                m_GameServer = new CTCPServer((int)NumLPort.Value);
                m_GameServer.Start();
                m_GameServer.OnSocketConnectionRequest += new CTCPServer.OnSocketConnectionRequestHandler(m_GameServer_OnSocketConnectionRequest);
                m_GameServer.BeginAcceptingConnections();
                return true;
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                CONSOLE_Print("Error binding port 6112");
                return false;
            }
        }

        void m_GameServer_OnSocketConnectionRequest(System.Net.Sockets.Socket con)
        {
        }
        void m_Server_OnSocketConnectionRequest(System.Net.Sockets.Socket con)
        {
            WC3C i = new WC3C(con, txtHostname.Text,(int)NumLPort.Value);
            i.OnConsoleLog += new WC3C.ConsoleLogEventHandler(i_OnConsoleLog);
            i.OnChatEvent += new WC3C.ChatEventHandler(i_OnChatEvent);
            m_Clients.Add(i);
            CONSOLE_Print("Accepted wc3 connection from [" + con.RemoteEndPoint.ToString( ) +"]" );
        }

        void i_OnChatEvent(ChatEvent i)
        {

            IncomingChatEvent id = (IncomingChatEvent)i.EventID;
            if (id == IncomingChatEvent.EID_WHISPER)
            {
                Print.RtbPrint(ref rtbBnetChat, "[" + DateTime.Now + "] ", Color.Gray, false);
                Print.RtbPrint(ref rtbBnetChat, i.User + " whispers: ", Color.Gold, false);
                Print.RtbPrint(ref rtbBnetChat, i.Message, Color.Lime, true);
            }
            else if (id == IncomingChatEvent.EID_TALK)
            {
                Print.RtbPrint(ref rtbBnetChat, "[" + DateTime.Now + "] ", Color.Gray, false);
                Print.RtbPrint(ref rtbBnetChat, i.User + ": ", Color.Gold, false);
                Print.RtbPrint(ref rtbBnetChat, i.Message, Color.White, true);
            }
            else if (id == IncomingChatEvent.EID_EMOTE)
            {
                Print.RtbPrint(ref rtbBnetChat, "[" + DateTime.Now + "] ", Color.Gray, false);
                Print.RtbPrint(ref rtbBnetChat, i.User + ": ", Color.Gold, false);
                Print.RtbPrint(ref rtbBnetChat, i.Message, Color.Yellow, true);
            }
            else if (id == IncomingChatEvent.EID_WHISPERSENT)
            {
                Print.RtbPrint(ref rtbBnetChat, "[" + DateTime.Now + "] ", Color.Gray, false);
                Print.RtbPrint(ref rtbBnetChat, "You whisper to " + i.User + ": ", Color.Gold, false);
                Print.RtbPrint(ref rtbBnetChat, i.Message, Color.Lime, true);
            }
            else if (id == IncomingChatEvent.EID_INFO)
            {
                Print.RtbPrint(ref rtbBnetChat, "[" + DateTime.Now + "] ", Color.Gray, false);
                Print.RtbPrint(ref rtbBnetChat, i.Message, Color.DodgerBlue, true);
            }
            else if (id == IncomingChatEvent.EID_ERROR)
            {
                Print.RtbPrint(ref rtbBnetChat, "[" + DateTime.Now + "] ", Color.Gray, false);
                Print.RtbPrint(ref rtbBnetChat, i.Message, Color.Red, true);
            }
            else if (id == IncomingChatEvent.EID_CHANNEL)
            {
                Print.RtbPrint(ref rtbBnetChat, "[" + DateTime.Now + "] ", Color.Gray, false);
                Print.RtbPrint(ref rtbBnetChat, "Joined channel ", Color.White, false);
                Print.RtbPrint(ref rtbBnetChat, i.Message,Color.Gold, true);
                m_Channel = i.Message;
                Print.LbPrint(ref ListUsers, "", 3);
                Print.TxtPrint(ref TxtChannel, m_Channel + "(" + ListUsers.Items.Count + ")");
            }
            else if (id == IncomingChatEvent.EID_BROADCAST)
            {
                Print.RtbPrint(ref rtbBnetChat, "[" + DateTime.Now + "] ", Color.Gray, false);
                Print.RtbPrint(ref rtbBnetChat, i.Message, Color.AliceBlue, true);
            }
            else if (id == IncomingChatEvent.EID_JOIN)
            {
                Print.LbPrint(ref ListUsers, i.User, 1);
                Print.TxtPrint(ref TxtChannel, m_Channel + "(" + ListUsers.Items.Count + ")");
            }
            else if (id == IncomingChatEvent.EID_LEAVE)
            {
                Print.LbPrint(ref ListUsers, i.User, 2);
                Print.TxtPrint(ref TxtChannel, m_Channel + "(" + ListUsers.Items.Count + ")");
            }
            else if (id == IncomingChatEvent.EID_SHOWUSER)
            {
                Print.LbPrint(ref ListUsers, i.User, 1);
                Print.TxtPrint(ref TxtChannel, m_Channel + "(" + ListUsers.Items.Count + ")");
            }
            else if ((int)id == 666) // devils case the freaking disconnect!!!1 
            {
                Print.LbPrint(ref ListUsers, i.User, 3);
                Print.TxtPrint(ref TxtChannel, "Channel( )");
                Print.RtbPrint(ref rtbBnetChat, "[" + DateTime.Now + "] ", Color.Gray, false);
                Print.RtbPrint(ref rtbBnetChat, "Connection lost", Color.Red, true);
            }
        }

        void i_OnConsoleLog(string i)
        {
            CONSOLE_Print(i);
        }


        private void CheckForGateaway()
        {
            RegistryKey key = Registry.CurrentUser;
            key = key.OpenSubKey("Software\\Blizzard Entertainment\\Warcraft III", true);
            bool exists = false;
            string[] value = (string[])(key.GetValue("Battle.net Gateways"));
            foreach (string i in value )
            {
                if (i.ToLower() == "localhost")
                {
                    exists = true;
                }
            }
            if (!exists)
            {
                CONSOLE_Print("Bnet gateway pointing to localhost doesn't exist");
                int i = value.Length;
                Array.Resize(ref value, value.Length + 3);
                value[i] = "localhost";
                value[i + 1] = "8";
                value[i + 2] = "GProxy";
                value[1] = ((value.Length - 2) / 3).ToString();
                key.SetValue("Battle.net Gateways", value);
                CONSOLE_Print("Added gateway [GProxy] pointing to localhost and made it the default gateway");
            }

            key.Close();
        }
        private void CheckForLoader()
        {
            CONSOLE_Print("Checking for the loader");
            RegistryKey key = Registry.CurrentUser;
            key = key.OpenSubKey("Software\\Blizzard Entertainment\\Warcraft III", true);
            string InstallPath = (string)key.GetValue("InstallPath");
            m_InstallPath = InstallPath;
            if (!File.Exists(InstallPath + "\\w3l.exe"))
            {
                CONSOLE_Print("W3L doesn't exists");
                WebClient w3l = new WebClient();
                CONSOLE_Print("Downloading w3l.exe");
                w3l.DownloadFileAsync(new Uri("http://gproxycb.googlecode.com/files/w3l.exe"), InstallPath + "\\w3l.exe");
                w3l.DownloadProgressChanged += new DownloadProgressChangedEventHandler(w3l_DownloadProgressChanged);
                w3l.DownloadFileCompleted += new AsyncCompletedEventHandler(w3l_DownloadFileCompleted);
            }
            if (!File.Exists(InstallPath + "\\w3lh.dll"))
            {
                CONSOLE_Print("W3LH doesn't exists");
                WebClient w3lh = new WebClient();
                CONSOLE_Print("Downloading w3lh.dll");
                w3lh.DownloadFileAsync(new Uri("http://gproxycb.googlecode.com/files/w3lh.dll"), InstallPath + "\\w3lh.dll");
                w3lh.DownloadProgressChanged += new DownloadProgressChangedEventHandler(w3lh_DownloadProgressChanged);
                w3lh.DownloadFileCompleted += new AsyncCompletedEventHandler(w3lh_DownloadFileCompleted);
            }
            CONSOLE_Print("Loader is installed");
            key.Close();
        }




        // form stuff

        private void BStart_Click(object sender, EventArgs e)
        {
            if (StartServer())
            {
              BStart.Enabled = false;
              BStop.Enabled = true;
            }
        }
        private void BStop_Click(object sender, EventArgs e)
        {
            m_Server.Stop();
            CONSOLE_Print("Stopped listening for connections on port 6112");
            BStart.Enabled = true;
            BStop.Enabled = false;
        }

        private void txtChatter_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtChatter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                foreach (WC3C i in m_Clients)
                {
                    if (i.IsBNET)
                    {
                        i.SendChatCommand(txtChatter.Text);
                    }
                }
                txtChatter.Clear();
            }
        }
        private void BExitBnet_Click(object sender, EventArgs e)
        {
            foreach (WC3C i in m_Clients)
            {
                if (i.IsBNET)
                {
                    i.Shutdown();
                }
            }
        }

        private void installTheWc3LoaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        void w3l_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            CONSOLE_Print("Downloaded w3l.exe successfully");
        }
        void w3l_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            CONSOLE_Print("Downloading w3l.exe -> " + e.ProgressPercentage.ToString( ));
        }
        void w3lh_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            CONSOLE_Print("Downloaded w3lh.dll successfully");
        }
        void w3lh_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            CONSOLE_Print("Downloading w3lh.dll -> " + e.ProgressPercentage.ToString());
        }

        private void rtbConsole_TextChanged(object sender, EventArgs e)
        {

        }
        

    }
}
