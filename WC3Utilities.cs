using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using CustomSockets;

namespace WC3
{
    struct CCommandPacket
    {
        byte Header;
        byte ID;
        BYTEARRAY Data;
        public CCommandPacket(byte header, byte id, byte[] data)
        {
            Header = header;
            ID = id;
            Data = new BYTEARRAY(data);
        }
        public BYTEARRAY PacketData  { get { return Data; } }
        public byte PacketHeader     { get { return Header; } }
        public byte PacketID         { get {return ID; }}
    } 
    struct GameHost
    {
        UInt16 m_GameType;
        UInt16 m_Parameter;
        UInt32 m_LanguageID;
        UInt16 m_Port;
        byte[] m_IP;
        UInt32 m_Status;
        UInt32 m_ElapsedTime;
        string m_GameName;
        byte m_SlotsTotal;
        UInt64 m_HostCounter;
        byte[] m_StatString;
        byte[] m_GamePassword;

        UInt32 m_MapFlags;
        UInt16 m_MapWidth;
        UInt16 m_MapHeight;
        byte[] m_MapCRC;
        string m_MapPath;
        string m_HostName;

        public GameHost(UInt16 nGameType, UInt16 nParameter, UInt32 nLanguageID, UInt16 nPort, byte[] nIP, UInt32 nStatus, UInt32 nElapsedTime, string nGameName, byte[] nGamePassword, byte nSlotsTotal, UInt64 nHostCounter, byte[] nStatString)
        {
            m_GameType = nGameType;
            m_Parameter = nParameter;
            m_LanguageID = nLanguageID;
            m_Port = nPort;
            m_IP = nIP;
            m_Status = nStatus;
            m_ElapsedTime = nElapsedTime;
            m_GameName = nGameName;
            m_GamePassword = nGamePassword;
            m_SlotsTotal = nSlotsTotal;
            m_HostCounter = nHostCounter;
            m_StatString = nStatString;

            byte[] StatString = (byte[])nStatString.Clone();

            byte Mask = 0;
            BYTEARRAY Result = new BYTEARRAY();

            for (int i = 0; i < StatString.Length; i++)
            {
                if ((i % 8) == 0)
                    Mask = StatString[i];
                else
                {
                    if ((Mask & (1 << (i % 8))) == 0)
                        Result.Add((byte)(StatString[i] - 1));
                    else
                        Result.Add(StatString[i]);
                }
            }
            if (Result.Count > 14)
            {
                m_MapFlags = Result.GetUInt32();
                m_MapWidth = Result.GetUInt16();
                m_MapCRC = Result.GetArray(4);
                m_MapHeight = Result.GetUInt16();
                m_MapPath = Result.GetCString();
                m_HostName = Result.GetCString();
            }
            else
            {
                m_MapFlags = new uint();
                m_MapWidth = new ushort();
                m_MapHeight = new ushort();
                m_MapCRC = new byte[4];
                m_MapPath = "";
                m_HostName = "";
            }

        }

        public UInt16 GameType      { get { return m_GameType; } }
        public UInt16 Parameter     { get { return m_Parameter; } }
        public UInt32 LanguageID    { get { return m_LanguageID; } }
        public UInt16 Port          { get { return m_Port; } }
        public byte[] IPAdress      { get { return m_IP; } }
        public UInt32 Status        { get { return m_Status; } }
        public UInt32 ElapsedTime   { get { return m_ElapsedTime; } }
        public string GameName      { get { return m_GameName; } }
        public byte TotalSlots      { get { return m_SlotsTotal; } }
        public UInt64 HostCounter   { get { return m_HostCounter; } }
        public byte[] StatString    { get { return m_StatString; } }
        public byte[] GamePassword  { get { return m_GamePassword; } }
        public UInt32 Flags         { get { return m_MapFlags; } }
        public UInt16 Width         { get { return m_MapWidth; } }
        public UInt16 Height        { get { return m_MapHeight; } }
        public byte[] CRC32         { get { return m_MapCRC; } }
        public string Path          { get { return m_MapPath; } }
        public string HostName      { get { return m_HostName; } }

    }
    struct ChatEvent
    {
        UInt32 m_EventID;
        UInt32 m_UserFlags;
        UInt32 m_Ping;
        string m_User;
        string m_Message;
        public ChatEvent(UInt32 eventid,UInt32 UserFlags,UInt32 ping ,string user,string message)
        {
            m_EventID = eventid;
            m_Ping = ping;
            m_UserFlags = UserFlags;
            m_User = user;
            m_Message = message;
        }
        public UInt32 Ping { get { return m_Ping;}}
        public UInt32 EventID { get{ return m_EventID;}}
        public string User { get { return m_User;}}
        public string Message { get { return m_Message;}}
        public UInt32 UserFlags { get { return m_UserFlags; } }
        
    }


    enum BattleNetProtocol : byte
    {
        SID_NULL = 0x00,
        SID_STOPADV = 0x02,
        SID_SERVERLIST = 0x04,
        SID_CLIENTID = 0x05,
        SID_STARTVERSIONING = 0x06,
        SID_REPORTVERSION = 0x07,
        SID_STARTADVEX = 0x08,
        SID_GETADVLISTEX = 0x09,
        SID_ENTERCHAT = 0x0A,
        SID_GETCHANNELLIST = 0x0B,
        SID_JOINCHANNEL = 0x0C,
        SID_CHATCOMMAND = 0x0E,
        SID_CHATEVENT = 0x0F,
        SID_LEAVECHAT = 0x10,
        SID_LOCALEINFO = 0x12,
        SID_FLOODDETECTED = 0x13,
        SID_UDPPINGRESPONSE = 0x14,
        SID_CHECKAD = 0x15,
        SID_CLICKAD = 0x16,
        SID_REGISTRY = 0x18,
        SID_MESSAGEBOX = 0x19,
        SID_STARTADVEX2 = 0x1A,
        SID_GAMEDATAADDRESS = 0x1B,
        SID_STARTADVEX3 = 0x1C,
        SID_LOGONCHALLENGEEX = 0x1D,
        SID_CLIENTID2 = 0x1E,
        SID_LEAVEGAME = 0x1F,
        SID_DISPLAYAD = 0x21,
        SID_NOTIFYJOIN = 0x22,
        SID_PING = 0x25,
        SID_READUSERDATA = 0x26,
        SID_WRITEUSERDATA = 0x27,
        SID_LOGONCHALLENGE = 0x28,
        SID_LOGONRESPONSE = 0x29,
        SID_CREATEACCOUNT = 0x2A,
        SID_SYSTEMINFO = 0x2B,
        SID_GAMERESULT = 0x2C,
        SID_GETICONDATA = 0x2D,
        SID_GETLADDERDATA = 0x2E,
        SID_FINDLADDERUSER = 0x2F,
        SID_CDKEY = 0x30,
        SID_CHANGEPASSWORD = 0x31,
        SID_CHECKDATAFILE = 0x32,
        SID_GETFILETIME = 0x33,
        SID_QUERYREALMS = 0x34,
        SID_PROFILE = 0x35,
        SID_CDKEY2 = 0x36,
        SID_LOGONRESPONSE2 = 0x3A,
        SID_CHECKDATAFILE2 = 0x3C,
        SID_CREATEACCOUNT2 = 0x3D,
        SID_LOGONREALMEX = 0x3E,
        SID_STARTVERSIONING2 = 0x3F,
        SID_QUERYREALMS2 = 0x40,
        SID_QUERYADURL = 0x41,
        SID_WARCRAFTGENERAL = 0x44,
        SID_NETGAMEPORT = 0x45,
        SID_NEWS_INFO = 0x46,
        SID_OPTIONALWORK = 0x4A,
        SID_EXTRAWORK = 0x4B,
        SID_REQUIREDWORK = 0x4C,
        SID_TOURNAMENT = 0x4E,
        SID_AUTH_INFO = 0x50,
        SID_AUTH_CHECK = 0x51,
        SID_AUTH_ACCOUNTCREATE = 0x52,
        SID_AUTH_ACCOUNTLOGON = 0x53,
        SID_AUTH_ACCOUNTLOGONPROOF = 0x54,
        SID_AUTH_ACCOUNTCHANGE = 0x55,
        SID_AUTH_ACCOUNTCHANGEPROOF = 0x56,
        SID_AUTH_ACCOUNTUPGRADE = 0x57,
        SID_AUTH_ACCOUNTUPGRADEPROOF = 0x58,
        SID_SETEMAIL = 0x59,
        SID_RESETPASSWORD = 0x5A,
        SID_CHANGEEMAIL = 0x5B,
        SID_SWITCHPRODUCT = 0x5C,
        SID_REPORTCRASH = 0x5D,
        SID_WARDEN = 0x5E,
        SID_GAMEPLAYERSEARCH = 0x60,
        SID_FRIENDSLIST = 0x65,
        SID_FRIENDSUPDATE = 0x66,
        SID_FRIENDSADD = 0x67,
        SID_FRIENDSREMOVE = 0x68,
        SID_FRIENDSPOSITION = 0x69,
        SID_CLANFINDCANDIDATES = 0x70,
        SID_CLANINVITEMULTIPLE = 0x71,
        SID_CLANCREATIONINVITATION = 0x72,
        SID_CLANDISBAND = 0x73,
        SID_CLANMAKECHIEFTAIN = 0x74,
        SID_CLANINFO = 0x75,
        SID_CLANQUITNOTIFY = 0x76,
        SID_CLANINVITATION = 0x77,
        SID_CLANREMOVEMEMBER = 0x78,
        SID_CLANINVITATIONRESPONSE = 0x79,
        SID_CLANRANKCHANGE = 0x7A,
        SID_CLANSETMOTD = 0x7B,
        SID_CLANMOTD = 0x7C,
        SID_CLANMEMBERLIST = 0x7D,
        SID_CLANMEMBERREMOVED = 0x7E,
        SID_CLANMEMBERSTATUSCHANGE = 0x7F,
        SID_CLANMEMBERRANKCHANGE = 0x81,
        SID_CLANMEMBERINFORMATION = 0x82
    }
    enum GameProtocol : byte
    {
        W3GS_PING_FROM_HOST = 1,	// 0x01
        W3GS_SLOTINFOJOIN = 4,	// 0x04
        W3GS_REJECTJOIN = 5,	// 0x05
        W3GS_PLAYERINFO = 6,	// 0x06
        W3GS_PLAYERLEAVE_OTHERS = 7,	// 0x07
        W3GS_GAMELOADED_OTHERS = 8,	// 0x08
        W3GS_SLOTINFO = 9,	// 0x09
        W3GS_COUNTDOWN_START = 10,	// 0x0A
        W3GS_COUNTDOWN_END = 11,	// 0x0B
        W3GS_INCOMING_ACTION = 12,	// 0x0C
        W3GS_CHAT_FROM_HOST = 15,	// 0x0F
        W3GS_START_LAG = 16,	// 0x10
        W3GS_STOP_LAG = 17,	// 0x11
        W3GS_HOST_KICK_PLAYER = 28,	// 0x1C
        W3GS_REQJOIN = 30,	// 0x1E
        W3GS_LEAVEGAME = 33,	// 0x21
        W3GS_GAMELOADED_SELF = 35,	// 0x23
        W3GS_OUTGOING_ACTION = 38,	// 0x26
        W3GS_OUTGOING_KEEPALIVE = 39,	// 0x27
        W3GS_CHAT_TO_HOST = 40,	// 0x28
        W3GS_DROPREQ = 41,	// 0x29
        W3GS_SEARCHGAME = 47,	// 0x2F (UDP/LAN)
        W3GS_GAMEINFO = 48,	// 0x30 (UDP/LAN)
        W3GS_CREATEGAME = 49,	// 0x31 (UDP/LAN)
        W3GS_REFRESHGAME = 50,	// 0x32 (UDP/LAN)
        W3GS_DECREATEGAME = 51,	// 0x33 (UDP/LAN)
        W3GS_CHAT_OTHERS = 52,	// 0x34
        W3GS_PING_FROM_OTHERS = 53,	// 0x35
        W3GS_PONG_TO_OTHERS = 54,	// 0x36
        W3GS_MAPCHECK = 61,	// 0x3D
        W3GS_STARTDOWNLOAD = 63,	// 0x3F
        W3GS_MAPSIZE = 66,	// 0x42
        W3GS_MAPPART = 67,	// 0x43
        W3GS_MAPPARTOK = 68,	// 0x44
        W3GS_MAPPARTNOTOK = 69,	// 0x45 - just a guess, received this packet after forgetting to send a crc in W3GS_MAPPART (f7 45 0a 00 01 02 01 00 00 00)
        W3GS_PONG_TO_HOST = 70,	// 0x46
        W3GS_INCOMING_ACTION2 = 72	// 0x48 - received this packet when there are too many actions to fit in W3GS_INCOMING_ACTION
    }
    enum IncomingChatEvent:int
	{
		EID_SHOWUSER			= 1,	// received when you join a channel (includes users in the channel and their information)
		EID_JOIN				= 2,	// received when someone joins the channel you're currently in
		EID_LEAVE				= 3,	// received when someone leaves the channel you're currently in
		EID_WHISPER				= 4,	// received a whisper message
		EID_TALK				= 5,	// received when someone talks in the channel you're currently in
		EID_BROADCAST			= 6,	// server broadcast
		EID_CHANNEL				= 7,	// received when you join a channel (includes the channel's name, flags)
		EID_USERFLAGS			= 9,	// user flags updates
		EID_WHISPERSENT			= 10,	// sent a whisper message
		EID_CHANNELFULL			= 13,	// channel is full
		EID_CHANNELDOESNOTEXIST	= 14,	// channel does not exist
		EID_CHANNELRESTRICTED	= 15,	// channel is restricted
		EID_INFO				= 18,	// broadcast/information message
		EID_ERROR				= 19,	// error message
		EID_EMOTE				= 23	// emote

	}



    class BaseConnectionProxy
    {
        protected CTCPClient m_Local;
        protected CTCPClient m_Remote;

        public BaseConnectionProxy(Socket socket, string hostname, int port)
        {
            m_Local = new CTCPClient();
            m_Local.Client = socket;
            m_Local.OnSocketDataArrival += new CTCPClient.OnSocketDataArrivalHandler(m_Local_OnSocketDataArrival);
            m_Local.OnSocketDisconnect += new CTCPClient.OnSocketDisconnectHandler(m_Local_OnSocketDisconnect);
            m_Remote = new CTCPClient();
            m_Remote.OnSocketConnect += new CTCPClient.OnSocketConnectHandler(m_Remote_OnSocketConnect);
            m_Remote.OnSocketDataArrival += new CTCPClient.OnSocketDataArrivalHandler(m_Remote_OnSocketDataArrival);
            m_Remote.OnSocketDisconnect += new CTCPClient.OnSocketDisconnectHandler(m_Remote_OnSocketDisconnect);
            m_Remote.ConnectAsync(hostname, (short)port);
        }
        public BaseConnectionProxy( CTCPClient local,CTCPClient remote)
        {
            m_Local = local;
            m_Remote = remote;
            m_Local.BeginRead();
            m_Remote.BeginRead();
        }
        public BaseConnectionProxy() { }

        void m_Remote_OnSocketDisconnect(CTCPClient sender)
        {
            RemoteDisconnected();
        }
        void m_Remote_OnSocketDataArrival(CTCPClient sender, byte[] data)
        {
            RemoteDataArrival(data);
        }
        void m_Remote_OnSocketConnect(CTCPClient sender)
        {
          
            RemoteConnected();
        }
        void m_Local_OnSocketDisconnect(CTCPClient sender)
        {
            LocalDisconnected();
        }
        void m_Local_OnSocketDataArrival(CTCPClient sender, byte[] data)
        {
            LocalDataArrival(data);
        }

        protected virtual void RemoteConnected()
        {
            m_Local.BeginRead();
            m_Remote.BeginRead();
        }
        protected virtual void RemoteDisconnected()
        {
            m_Local.DisconnectAsync();
        }
        protected virtual void RemoteDataArrival(byte[] data)
        {
            m_Local.WriteAsync(data);
        }
        protected virtual void LocalDataArrival(byte[] data)
        {
            m_Remote.WriteAsync(data);
        }
        protected virtual void LocalDisconnected()
        {
            m_Remote.DisconnectAsync();
        }

    }
    class BNETProtocol
    {
         // decoding functions
        public static List<GameHost> Decode_SID_GETADVLISTEX(BYTEARRAY data)
        {
            // (DWORD) GamesFound
            // for( 1 .. GamesFound )
            //		2 bytes				-> GameType
            //		2 bytes				-> Parameter
            //		4 bytes				-> Language ID
            //		2 bytes				-> AF_INET
            //		2 bytes				-> Port
            //		4 bytes				-> IP
            //		4 bytes				-> zeros
            //		4 bytes				-> zeros
            //		4 bytes				-> Status
            //		4 bytes				-> ElapsedTime
            //		null term string	-> GameName
            //		1 byte				-> GamePassword
            //		1 byte				-> SlotsTotal
            //		8 bytes				-> HostCounter (ascii hex format)
            //		null term string	-> StatString
            //(DWORD) Number of games
            //If count is 0:
            //(DWORD) Status
            //Otherwise, games are listed thus:
            //For each list item:
            //(WORD) Game Type
            //(WORD) Parameter
            //(DWORD) Language ID
            //(WORD) Address Family (Always AF_INET)
            //(WORD) Port
            //(DWORD) Host's IP
            //(DWORD) sin_zero (0)
            //(DWORD) sin_zero (0)
            //(DWORD) Game Status
            //(DWORD) Elapsed time (in seconds)
            //(STRING) Game name
            //(STRING) Game password
            //{BYTE) Slots total ( hex format )
            //(QWORD) HostCounter ( reverse hex format )
            //(STRING) Game statstring
            data.RemoveRange(0,4);
            UInt32 GamesFound = data.GetUInt32();
            List<GameHost> m_Games = new List<GameHost>((int)GamesFound);
            while (GamesFound > 0)
            {
                GamesFound--;
                UInt16 GameType = data.GetUInt16();
                UInt16 Parameter = data.GetUInt16();
                UInt32 LanguageID = data.GetUInt32();
                UInt16 AddressFamily = data.GetUInt16();
                UInt16 Port = data.GetUInt16();
                byte[] HostIP = data.GetArray(4);
                data.GetUInt32();
                data.GetUInt32();
                UInt32 GameStatus = data.GetUInt32();
                UInt32 ElapsedTime = data.GetUInt32();
                string GameName = data.GetCString();
                data.GetByte();
                byte SlotsTotal = data.GetByte();
                UInt64 HostCounter = data.GetUInt64();
                int i = data.IndexOf(0);
                byte[] StatString;
                if (i == -1)
                    StatString = data.GetArray(data.Count);
                else
                {
                    StatString = data.GetArray(i);
                    data.GetByte();
                }
               
                m_Games.Add(new GameHost(GameType, Parameter, LanguageID, Port, HostIP, GameStatus, ElapsedTime, GameName, new byte[]{}, SlotsTotal, HostCounter, StatString));
               
            }
            return m_Games;
        }
        public static ChatEvent Decode_SID_CHATEVENT(BYTEARRAY data)
        {
            //(DWORD) Event ID
            //(DWORD) User's Flags
            //(DWORD) Ping
            //(DWORD) IP Address (Defunct)
            //(DWORD) Account number (Defunct)
            //(DWORD) Registration Authority (Defunct)
            //(STRING) Username
            //(STRING) Text *
            data.RemoveRange(0,4);
            UInt32 EventID = data.GetUInt32( );
            UInt32 UserFlags = data.GetUInt32();
            UInt32 Ping = data.GetUInt32();
            data.GetUInt32();
            data.GetUInt32();
            data.GetUInt32();
            string User = data.GetCString();
            string Message = data.GetCString();
            return new ChatEvent(EventID, UserFlags, Ping, User, Message);
        }
        public static string Decode_SID_CHATCOMMAND(BYTEARRAY data)
        {
            data.RemoveRange(0, 4);
            return data.GetCString();
        }
       
        // creating functions
        public static BYTEARRAY Create_SID_CHATCOMMAND(string messsage)
        {
            BYTEARRAY packet = new BYTEARRAY();
            packet.Add(255);
            packet.Add((byte)BattleNetProtocol.SID_CHATCOMMAND);
            packet.Add(0);
            packet.Add(0);
            packet.AddCString(messsage);
            AssignLength(ref packet);
            return packet;
        }
        public static BYTEARRAY Create_SID_GETADVLISTEX(List<GameHost> games, System.Net.IPAddress custom_ipadress,ushort custom_port)
        {
            BYTEARRAY packet = new BYTEARRAY();
            packet.Add(255);
            packet.Add((byte)BattleNetProtocol.SID_GETADVLISTEX);
            packet.Add(0);
            packet.Add(0);
            packet.AddUInt32((uint)games.Count);
            foreach (GameHost i in games)
            {
                //(WORD) Game Type
                //(WORD) Parameter
                //(DWORD) Language ID
                //(WORD) Address Family (Always AF_INET)
                //(WORD) Port
                //(DWORD) Host's IP
                //(DWORD) sin_zero (0)
                //(DWORD) sin_zero (0)
                //(DWORD) Game Status
                //(DWORD) Elapsed time (in seconds)
                //(STRING) Game name
                //(STRING) Game password
                //{BYTE) Slots total ( hex format )
                //(QWORD) HostCounter ( reverse hex format )
                //(STRING) Game statstring
                packet.AddUInt16(i.GameType);
                packet.AddUInt16(i.Parameter);
                packet.AddUInt32(i.LanguageID);
                packet.AddUInt16((ushort)2);
                packet.AddUInt16(custom_port);
                packet.AddRange(custom_ipadress.GetAddressBytes());
                packet.AddUInt64(0);
                packet.AddUInt32(i.Status);
                packet.AddUInt32(i.ElapsedTime);
                packet.AddCString(i.GameName);
                packet.Add(0);
                packet.Add(i.TotalSlots);
                packet.AddUInt64(i.HostCounter);
                packet.AddRange(i.StatString);
                packet.Add(0);
            }
            AssignLength(ref packet);
            return packet;
        }
        public static BYTEARRAY Create_SID_CHATEVENT( IncomingChatEvent eventid, string user,string message)
        {
             //(DWORD) Event ID
            //(DWORD) User's Flags
            //(DWORD) Ping
            //(DWORD) IP Address (Defunct)
            //(DWORD) Account number (Defunct)
            //(DWORD) Registration Authority (Defunct)
            //(STRING) Username
            //(STRING) Text *
            BYTEARRAY packet = new BYTEARRAY();
            packet.Add(255);
            packet.Add((byte)eventid);
            packet.Add(0);
            packet.Add(0);
            packet.AddUInt32(0);
            packet.AddUInt32(0);
            packet.AddUInt32(0);
            packet.AddUInt32(0);
            packet.AddUInt32(0);
            packet.AddUInt32(0);
            packet.AddCString(user);
            packet.AddCString(message);
            AssignLength(ref packet);
            return packet;
            
        }
        
        // extra functions
        public static void AssignLength(ref BYTEARRAY packet)
        {
            byte[] length = BitConverter.GetBytes((ushort)packet.Count);
            packet[2] = length[0];
            packet[3] = length[1];
        }
    }
}
