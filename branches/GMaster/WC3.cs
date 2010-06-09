using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Net;
using CustomSockets;

namespace WC3
{
    class WC3C
    {
        public delegate void ConsoleLogEventHandler(string i );
        public delegate void ChatEventHandler( ChatEvent i );
        public delegate void ProcessCommandHandler(WC3C sender,string Command,string Payload);

        public event ConsoleLogEventHandler OnConsoleLog;
        public event ChatEventHandler OnChatEvent;
        public event ProcessCommandHandler OnNewCommand;

        CTCPClient m_Local;
        CTCPClient m_Remote;
        bool m_IsBNET;
        int m_Port;
        string m_UserName;

        List<GameHost> m_Games;


        void Log(string i)
        {
            if (OnConsoleLog != null)
            {
                OnConsoleLog(i);
            }
        }
        public bool IsBNET { get { return m_IsBNET; } }
        public WC3C(Socket con, string hostname,int port)
        {
            m_Port = port;
            m_IsBNET = false;
            m_Games = new List<GameHost>();
            m_Local = new CTCPClient();
            m_Local.Client = con;
            m_Local.OnSocketDataArrival += new CTCPClient.OnSocketDataArrivalHandler(m_Local_OnSocketDataArrival);
            m_Local.OnSocketDisconnect += new CTCPClient.OnSocketDisconnectHandler(m_Local_OnSocketDisconnect);
            m_Remote = new CTCPClient();
            m_Remote.OnSocketConnect += new CTCPClient.OnSocketConnectHandler(m_Remote_OnSocketConnect);
            m_Remote.OnSocketDataArrival += new CTCPClient.OnSocketDataArrivalHandler(m_Remote_OnSocketDataArrival);
            m_Remote.OnSocketDisconnect += new CTCPClient.OnSocketDisconnectHandler(m_Remote_OnSocketDisconnect);
            m_Remote.ConnectAsync(hostname, 6112);
        }

        #region SocketsFunctions
        void m_Remote_OnSocketDisconnect(CTCPClient sender)
        {
            Log("Remote socket disconnected from [" + sender.Client.RemoteEndPoint.ToString() + "]");
            if( m_Local.Connected)
            m_Local.DisconnectAsync();
        }
        void m_Remote_OnSocketDataArrival(CTCPClient sender, byte[] data)
        {
            if (data[0] == 2)
            {
                m_Local.WriteAsync(data);
            }
            else
            {
                BNET_ProcessPackets(ExtractPackets(data));
            }
        }
        void m_Remote_OnSocketConnect(CTCPClient sender)
        {
            Log("Remote socket connected to [" + sender.Client.RemoteEndPoint.ToString( )+ "]");
            m_Local.BeginRead();
            m_Remote.BeginRead();
        }
        void m_Local_OnSocketDisconnect(CTCPClient sender)
        {
            Log("Remote socket disconnected from [" + sender.Client.RemoteEndPoint.ToString() + "]");
            if (m_Remote.Connected) 
            m_Remote.DisconnectAsync();

            if (OnChatEvent != null)
            {
                OnChatEvent(new ChatEvent(666, 0, 0, "Devil!!!", "GO to hell!!!!"));
            }
        }
        void m_Local_OnSocketDataArrival(CTCPClient sender, byte[] data)
        {
            if (data[0] == 1)
            {
                m_IsBNET = true;
                m_Remote.WriteAsync(data);
            }
            else if (data[0] == 2)
            {
                m_Remote.WriteAsync(data);
            }
            else
            {
                WC3_ProcessPackets(ExtractPackets(data));
            }
        }
        #endregion

        Queue<CCommandPacket> ExtractPackets(byte[] data)
        {
            BYTEARRAY reader = new BYTEARRAY(data);
            Queue<CCommandPacket> packets = new Queue<CCommandPacket>();
            while (reader.Count >= 4)
            {
                if (reader[0] == 255)
                {
                    int length = (int)BitConverter.ToUInt16(new byte[] { reader[2], reader[3] }, 0);
                    if (length <= reader.Count)
                    {
                        packets.Enqueue(new CCommandPacket(255, reader[1], reader.GetArray(length)));
                    }
                    else
                        throw new Exception("[BNET] Invalid length");
                }
                else
                    throw new Exception("[BNET] Invalid header");
            }
            return packets;
        }
        void WC3_ProcessPackets(Queue<CCommandPacket> packets)
        {
            bool forward;
            while (packets.Count != 0)
            {
                forward = true;
                CCommandPacket packet = packets.Dequeue();
                switch ((BattleNetProtocol)packet.PacketID)
                {
                    case BattleNetProtocol.SID_AUTH_ACCOUNTLOGON:
                         BYTEARRAY parse = new BYTEARRAY(packet.PacketData.GetRange(0,packet.PacketData.Count).ToArray( ));
                        parse.RemoveRange(0,36);
                        m_UserName = parse.GetCString( );
                        break;
                    case BattleNetProtocol.SID_CHATCOMMAND:
                        if (OnNewCommand != null)
                        {
                            string command = BNETProtocol.Decode_SID_CHATCOMMAND(packet.PacketData);
                            int pos = command.IndexOf(" ");
                        
                            OnNewCommand(this, command.Substring(0, pos), command.Remove(0, pos + 1));
                            forward = false;
                        }
                        if (OnChatEvent != null)
                        {
                            OnChatEvent(new ChatEvent((int)IncomingChatEvent.EID_TALK, 0, 0, m_UserName, BNETProtocol.Decode_SID_CHATCOMMAND(new BYTEARRAY(packet.PacketData.GetRange(0, packet.PacketData.Count).ToArray()))));
                        }
                        break;

                }
                if (forward)
                {
                    m_Remote.WriteAsync(packet.PacketData.ToArray( ));
                }
            }
        }
        void BNET_ProcessPackets(Queue<CCommandPacket> packets)
        {
            bool forward;
            while (packets.Count != 0)
            {
                forward = true;
                CCommandPacket packet = packets.Dequeue();
                switch ((BattleNetProtocol)packet.PacketID)
                {
                    case BattleNetProtocol.SID_GETADVLISTEX: HandleGameListPacket(packet.PacketData); forward = false; break;
                    case BattleNetProtocol.SID_CHATEVENT :
                        if (OnChatEvent != null)
                        {
                            OnChatEvent(BNETProtocol.Decode_SID_CHATEVENT(new BYTEARRAY((byte[])packet.PacketData.ToArray( ).Clone( ))));
                        }
                        break;

                }
                if (forward)
                {
                    m_Local.WriteAsync(packet.PacketData.ToArray( ));
                }
            }
                
        }

        public void SendBroadCastToClient(string message)
        {
            m_Local.WriteAsync(BNETProtocol.Create_SID_CHATEVENT(IncomingChatEvent.EID_BROADCAST, "GProxy", message).ToArray());
        }
        public void SendChatCommand(string message)
        {
            m_Remote.WriteAsync(BNETProtocol.Create_SID_CHATCOMMAND(message).ToArray( ));
            //if (message[0] != '/')
            //{
            //    m_Local.WriteAsync(BNETProtocol.Create_SID_CHATEVENT(IncomingChatEvent.EID_TALK, m_UserName, message).ToArray( ));
            //    if (OnChatEvent != null)
            //    {
            //        OnChatEvent(new ChatEvent((int)IncomingChatEvent.EID_TALK, 0, 0, m_UserName+ " ", message));
            //    }
            //}
        }
        public void Shutdown()
        {
            if (m_Local.Connected && m_Remote.Connected)
            {
                m_Local.DisconnectAsync();
                m_Remote.DisconnectAsync();
            }
        }

        void HandleGameListPacket(BYTEARRAY data)
        {
            m_Games = BNETProtocol.Decode_SID_GETADVLISTEX(data);
            m_Local.WriteAsync(BNETProtocol.Create_SID_GETADVLISTEX(m_Games, IPAddress.Parse("127.0.0.1"), (ushort)m_Port).ToArray());
            Log("Received Gamelist packet. Games found : " + m_Games.Count.ToString());
        }
    }

}
