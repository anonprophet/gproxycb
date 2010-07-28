/*

   Copyright 2010 Trevor Hogan

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

*/

#include "gproxy.h"
#include "bnetprotocol.h"
#include "helper.h"

CBNETProtocol :: CBNETProtocol( )
{
        m_ClientToken = new QByteArray((char *){ 220, 1, 203, 7 });
}

CBNETProtocol :: ~CBNETProtocol( )
{

}

///////////////////////
// RECEIVE FUNCTIONS //
///////////////////////

bool CBNETProtocol :: RECEIVE_SID_NULL( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_NULL" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length

	return ValidateLength( data );
}

QList<CIncomingGameHost *> CBNETProtocol :: RECEIVE_SID_GETADVLISTEX( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_GETADVLISTEX" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
	// 4 bytes					-> GamesFound
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
        //		null term QString	-> GameName
	//		1 byte				-> GamePassword
	//		1 byte				-> SlotsTotal
	//		8 bytes				-> HostCounter (ascii hex format)
        //		null term QString	-> StatString

        QList<CIncomingGameHost *> Games;

	if( ValidateLength( data ) && data.size( ) >= 8 )
	{
		unsigned int i = 8;
                quint32 GamesFound = Util::ExtractUInt32(data,i);

		while( GamesFound > 0 )
		{
			GamesFound--;

			if( data.size( ) < i + 33 )
				break;

                        quint32 GameType = Util::ExtractUInt16(data,i);
			i += 2;
                        quint32 Parameter = Util::ExtractUInt32(data,i);
			i += 2;
                        quint32 LanguageID = Util::ExtractUInt32(data,i);
			i += 4;
			// AF_INET
			i += 2;
                        quint32 Port = Util::ExtractUInt16(data,i);
			i += 2;
                        QByteArray IP = data.mid(i,4);
			i += 4;
			// zeros
			i += 4;
			// zeros
			i += 4;
                        quint32 Status = Util::ExtractUInt32(data,i);
			i += 4;
                        quint32 ElapsedTime = Util::ExtractUInt32(data,i);
			i += 4;
                        QString GameName = Util::ExtractCString(data,i);
			i += GameName.size( ) + 1;

			if( data.size( ) < i + 1 )
				break;

                        QString GamePassword = Util::ExtractCString(data,i);
			i += GamePassword.size( ) + 1;

			if( data.size( ) < i + 10 )
				break;

			// SlotsTotal is in ascii hex format

			unsigned char SlotsTotal = data[i];
			unsigned int c;
                        QTextStream ss;
                        SS << QString( 1, SlotsTotal );
			SS >> hex >> c;
			SlotsTotal = c;
			i++;

			// HostCounter is in reverse ascii hex format
			// e.g. 1  is "10000000"
			// e.g. 10 is "a0000000"
                        // extract it, reverse it, parse it, construct a single quint32

                        QByteArray HostCounterRaw = data.mid(i,8);
                        QString HostCounterString = Util::Reverse(HostCounterString);
                        quint32 HostCounter = 0;

			for( int j = 0; j < 4; j++ )
			{
				unsigned int c;
                                QTextStream SS;
                                SS << HostCounterString.mid( j * 2, 2 );
				SS >> hex >> c;
				HostCounter |= c << ( 24 - j * 8 );
			}

			i += 8;
                        QByteArray StatString = Conversions::ToQByteArray( Util::ExtractCString(data,i));
                        i += StatString.size( ) + 1;

                        Games.push_back( new CIncomingGameHost( GameType, Parameter, LanguageID, Port, IP, Status, ElapsedTime, GameName, SlotsTotal, HostCounter, StatString ) );
		}
	}

	return Games;
}

bool CBNETProtocol :: RECEIVE_SID_ENTERCHAT( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_ENTERCHAT" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
        // null terminated QString	-> UniqueName

	if( ValidateLength( data ) && data.size( ) >= 5 )
	{
                m_UniqueName = Util::ExtractCString(data,4);
		return true;
	}

	return false;
}

CIncomingChatEvent *CBNETProtocol :: RECEIVE_SID_CHATEVENT( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_CHATEVENT" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
	// 4 bytes					-> EventID
	// 4 bytes					-> ???
	// 4 bytes					-> Ping
	// 12 bytes					-> ???
        // null terminated QString	-> User
        // null terminated QString	-> Message

	if( ValidateLength( data ) && data.size( ) >= 29 )
	{
                quint32 EventID = Util::ExtractUInt32(data,4);
                quint32 Ping = Util::ExtractUInt32(data,12);
                QString User = Util::ExtractCString( data, 28 );
                QString Message = Util::ExtractCString( data, User.size( ) + 29 );

                switch( EventID )
		{
		case CBNETProtocol :: EID_SHOWUSER:
		case CBNETProtocol :: EID_JOIN:
		case CBNETProtocol :: EID_LEAVE:
		case CBNETProtocol :: EID_WHISPER:
		case CBNETProtocol :: EID_TALK:
		case CBNETProtocol :: EID_BROADCAST:
		case CBNETProtocol :: EID_CHANNEL:
		case CBNETProtocol :: EID_USERFLAGS:
		case CBNETProtocol :: EID_WHISPERSENT:
		case CBNETProtocol :: EID_CHANNELFULL:
		case CBNETProtocol :: EID_CHANNELDOESNOTEXIST:
		case CBNETProtocol :: EID_CHANNELRESTRICTED:
		case CBNETProtocol :: EID_INFO:
		case CBNETProtocol :: EID_ERROR:
		case CBNETProtocol :: EID_EMOTE:
                        return new CIncomingChatEvent(	(CBNETProtocol :: IncomingChatEvent)EventID,Ping,User,Message);

		}

	}

	return NULL;
}

bool CBNETProtocol :: RECEIVE_SID_CHECKAD( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_CHECKAD" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length

	return ValidateLength( data );
}

bool CBNETProtocol :: RECEIVE_SID_STARTADVEX3( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_STARTADVEX3" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
	// 4 bytes					-> Status

	if( ValidateLength( data ) && data.size( ) >= 8 )
	{
            quint32 Status = Util::ExtractUInt32(data,4);

                if( Status == 0 )
			return true;
	}

	return false;
}

QByteArray CBNETProtocol :: RECEIVE_SID_PING( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_PING" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
	// 4 bytes					-> Ping

	if( ValidateLength( data ) && data.size( ) >= 8 )
                return data.mid(4,4);

        return QByteArray( );
}

bool CBNETProtocol :: RECEIVE_SID_LOGONRESPONSE( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_LOGONRESPONSE" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
	// 4 bytes					-> Status

	if( ValidateLength( data ) && data.size( ) >= 8 )
	{
                quint32 Status = Util::ExtractUInt32(data,4);

                if( Status == 1 )
			return true;
	}

	return false;
}

bool CBNETProtocol :: RECEIVE_SID_AUTH_INFO( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_AUTH_INFO" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
	// 4 bytes					-> LogonType
	// 4 bytes					-> ServerToken
	// 4 bytes					-> ???
	// 8 bytes					-> MPQFileTime
        // null terminated QString	-> IX86VerFileName
        // null terminated QString	-> ValueQStringFormula

	if( ValidateLength( data ) && data.size( ) >= 25 )
	{
                m_LogonType = data.mid(4,4);
                m_ServerToken = data.mid(8,4);
                m_MPQFileTime = data.mid(16,4);
                m_IX86VerFileName = Util::ExtractCString(data,24);
                m_ValueQStringFormula = Util::ExtractCString( data, m_IX86VerFileName.size( ) + 25 );
		return true;
	}

	return false;
}

bool CBNETProtocol :: RECEIVE_SID_AUTH_CHECK( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_AUTH_CHECK" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
	// 4 bytes					-> KeyState
        // null terminated QString	-> KeyStateDescription

	if( ValidateLength( data ) && data.size( ) >= 9 )
	{
                m_KeyState = data.mid(4,4);
                m_KeyStateDescription = Util::ExtractCString(data,8);

                if( Conversions::ToUInt32( m_KeyState ) == KR_GOOD )
			return true;
	}

	return false;
}

bool CBNETProtocol :: RECEIVE_SID_AUTH_ACCOUNTLOGON( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_AUTH_ACCOUNTLOGON" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
	// 4 bytes					-> Status
	// if( Status == 0 )
	//		32 bytes			-> Salt
	//		32 bytes			-> ServerPublicKey

	if( ValidateLength( data ) && data.size( ) >= 8 )
	{
                quint32 Status = Util::ExtractUInt32(data,4);

                if( Status == 0 && data.size( ) >= 72 )
		{
                        m_Salt = data.mid(8,32);
                        m_ServerPublicKey = data.mid(40,32);
			return true;
		}
	}

	return false;
}

bool CBNETProtocol :: RECEIVE_SID_AUTH_ACCOUNTLOGONPROOF( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_AUTH_ACCOUNTLOGONPROOF" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
	// 4 bytes					-> Status

	if( ValidateLength( data ) && data.size( ) >= 8 )
	{
                quint32 Status = Util::ExtractUInt32(data,4);

                if( Status == 0 )
			return true;
	}

	return false;
}

QByteArray CBNETProtocol :: RECEIVE_SID_WARDEN( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_WARDEN" );
	// DEBUG_PRINT( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
	// n bytes					-> Data

	if( ValidateLength( data ) && data.size( ) >= 4 )
                return data.mid(4,data.size( ) - 4);

        return QByteArray( );
}

QList<CIncomingFriendList *> CBNETProtocol :: RECEIVE_SID_FRIENDSLIST( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_FRIENDSLIST" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
	// 1 byte					-> Total
	// for( 1 .. Total )
        //		null term QString	-> Account
	//		1 byte				-> Status
	//		1 byte				-> Area
	//		4 bytes				-> ???
        //		null term QString	-> Location

        QList<CIncomingFriendList *> Friends;

	if( ValidateLength( data ) && data.size( ) >= 5 )
	{
		unsigned int i = 5;
		unsigned char Total = data[4];

		while( Total > 0 )
		{
			Total--;

			if( data.size( ) < i + 1 )
				break;

                        QString Account = Util::ExtractCString(data,i);
			i += Account.size( ) + 1;

			if( data.size( ) < i + 7 )
				break;

			unsigned char Status = data[i];
			unsigned char Area = data[i + 1];
			i += 6;
                        QString Location = Util::ExtractCString(data,i);
			i += Location.size( ) + 1;
                        Friends.push_back( new CIncomingFriendList(Account,Status,Area,Location));
                    }
	}

	return Friends;
}

QList<CIncomingClanList *> CBNETProtocol :: RECEIVE_SID_CLANMEMBERLIST( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_CLANMEMBERLIST" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
	// 4 bytes					-> ???
	// 1 byte					-> Total
	// for( 1 .. Total )
        //		null term QString	-> Name
	//		1 byte				-> Rank
	//		1 byte				-> Status
        //		null term QString	-> Location

        QList<CIncomingClanList *> ClanList;

	if( ValidateLength( data ) && data.size( ) >= 9 )
	{
		unsigned int i = 9;
		unsigned char Total = data[8];

		while( Total > 0 )
		{
			Total--;

			if( data.size( ) < i + 1 )
				break;

                        QString Name = Util::ExtractCString(data,i);
			i += Name.size( ) + 1;

			if( data.size( ) < i + 3 )
				break;

			unsigned char Rank = data[i];
			unsigned char Status = data[i + 1];
			i += 2;

                        // in the original VB source the location QString is read but discarded, so that's what I do here

                        QString Location = Util::ExtractCString(data,i);
			i += Location.size( ) + 1;
                        ClanList.push_back( new CIncomingClanList(Name,Rank,Status));
		}
	}

	return ClanList;
}

CIncomingClanList *CBNETProtocol :: RECEIVE_SID_CLANMEMBERSTATUSCHANGE( QByteArray data )
{
	// DEBUG_Print( "RECEIVED SID_CLANMEMBERSTATUSCHANGE" );
	// DEBUG_Print( data );

	// 2 bytes					-> Header
	// 2 bytes					-> Length
        // null terminated QString	-> Name
	// 1 byte					-> Rank
	// 1 byte					-> Status
        // null terminated QString	-> Location

	if( ValidateLength( data ) && data.size( ) >= 5 )
	{
            QString Name = Util::ExtractCString(data,4);

		if( data.size( ) >= Name.size( ) + 7 )
		{
			unsigned char Rank = data[Name.size( ) + 5];
			unsigned char Status = data[Name.size( ) + 6];

                        // in the original VB source the location QString is read but discarded, so that's what I do here

                        QByteArray Location = UTIL_ExtractCQString( data, Name.size( ) + 7 );
                        return new CIncomingClanList(Name,Rank,Status);
		}
	}

	return NULL;
}

////////////////////
// SEND FUNCTIONS //
////////////////////

QByteArray CBNETProtocol :: SEND_PROTOCOL_INITIALIZE_SELECTOR( )
{
        QByteArray packet;
	packet.push_back( 1 );
	// DEBUG_Print( "SENT PROTOCOL_INITIALIZE_SELECTOR" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_NULL( )
{
        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );	// BNET header constant
	packet.push_back( SID_NULL );				// SID_NULL
        packet.push_back((char)0);						// packet length will be assigned later
        packet.push_back((char)0);						// packet length will be assigned later
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_NULL" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_STOPADV( )
{
        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );	// BNET header constant
	packet.push_back( SID_STOPADV );			// SID_STOPADV
        packet.push_back((char)0);						// packet length will be assigned later
        packet.push_back((char)0);						// packet length will be assigned later
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_STOPADV" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_GETADVLISTEX( QString gameName, quint32 numGames )
{
	unsigned char Condition1[]	= {   0,   0 };
	unsigned char Condition2[]	= {   0,   0 };
	unsigned char Condition3[]	= {   0,   0,   0,   0 };
	unsigned char Condition4[]	= {   0,   0,   0,   0 };

	if( gameName.empty( ) )
	{
		Condition1[0] = 0;		Condition1[1] = 224;
		Condition2[0] = 127;	Condition2[1] = 0;
	}
	else
	{
		Condition1[0] = 255;	Condition1[1] = 3;
		Condition2[0] = 0;		Condition2[1] = 0;
		Condition3[0] = 255;	Condition3[1] = 3;		Condition3[2] = 0;		Condition3[3] = 0;
		numGames = 1;
	}

        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );			// BNET header constant
	packet.push_back( SID_GETADVLISTEX );				// SID_GETADVLISTEX
        packet.push_back((char)0);								// packet length will be assigned later
        packet.push_back((char)0);								// packet length will be assigned later
        packet.append(Condition1);
        packet.append( Condition2 );
        packet.append( Condition3 );
        packet.append( Condition4 );
        Util::AppendUInt32( numGames );                             // maximum number of games to list
        Util::AppendCString( gamename );                            // Game Name
        packet.push_back((char)0);								// Game Password is NULL
        packet.push_back((char)0);								// Game Stats is NULL
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_GETADVLISTEX" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_ENTERCHAT( )
{
        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );	// BNET header constant
	packet.push_back( SID_ENTERCHAT );			// SID_ENTERCHAT
        packet.push_back((char)0);						// packet length will be assigned later
        packet.push_back((char)0);						// packet length will be assigned later
        packet.push_back((char)0);						// Account Name is NULL on Warcraft III/The Frozen Throne
        packet.push_back((char)0);						// Stat QString is NULL on CDKEY'd products
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_ENTERCHAT" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_JOINCHANNEL( QString channel )
{
	unsigned char NoCreateJoin[]	= { 2, 0, 0, 0 };
	unsigned char FirstJoin[]		= { 1, 0, 0, 0 };

        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );				// BNET header constant
	packet.push_back( SID_JOINCHANNEL );					// SID_JOINCHANNEL
        packet.push_back((char)0);									// packet length will be assigned later
        packet.push_back((char)0);									// packet length will be assigned later

	if( channel.size( ) > 0 )
                packet.append( NoCreateJoin );	// flags for no create join
	else
                packet.append(FirstJoin);		// flags for first join

        Util::AppendCString(channel);
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_JOINCHANNEL" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_CHATCOMMAND( QString command )
{
        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );		// BNET header constant
	packet.push_back( SID_CHATCOMMAND );			// SID_CHATCOMMAND
        packet.push_back((char)0);							// packet length will be assigned later
        packet.push_back((char)0);							// packet length will be assigned later
        Util::AppendCString(command);
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_CHATCOMMAND" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_CHECKAD( )
{
	unsigned char Zeros[] = { 0, 0, 0, 0 };

        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );	// BNET header constant
	packet.push_back( SID_CHECKAD );			// SID_CHECKAD
        packet.push_back((char)0);						// packet length will be assigned later
        packet.push_back((char)0);						// packet length will be assigned later
        Util::AppendUInt32(packet,0);
        Util::AppendUInt32(packet,0);
        Util::AppendUInt32(packet,0);
        Util::AppendUInt32(packet,0);
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_CHECKAD" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_STARTADVEX3( unsigned char state, QByteArray mapGameType, QByteArray mapFlags, QByteArray mapWidth, QByteArray mapHeight, QString gameName, QString hostName, quint32 upTime, QString mapPath, QByteArray mapCRC, QByteArray mapSHA1, quint32 hostCounter )
{
	// todotodo: sort out how GameType works, the documentation is horrendous

/*

Game type tag: (read W3GS_GAMEINFO for this field)
 0x00000001 - Custom
 0x00000009 - Blizzard/Ladder
Map author: (mask 0x00006000) can be combined
*0x00002000 - Blizzard
 0x00004000 - Custom
Battle type: (mask 0x00018000) cant be combined
 0x00000000 - Battle
*0x00010000 - Scenario
Map size: (mask 0x000E0000) can be combined with 2 nearest values
 0x00020000 - Small
 0x00040000 - Medium
*0x00080000 - Huge
Observers: (mask 0x00700000) cant be combined
 0x00100000 - Allowed observers
 0x00200000 - Observers on defeat
*0x00400000 - No observers
Flags:
 0x00000800 - Private game flag (not used in game list)

*/



        QString HostCounterString = UTIL_ToHexQString( hostCounter );

        if( HostCounterQString.size( ) < 8 )
                HostCounterString.insert( 0, 8 - HostCounterQString.size( ), '0' );

        HostCounterString = Util::Reverse(HostCounterString);

        QByteArray packet;

        // make the stat QString

        QByteArray StatString;
        StatString.append(mapFlags );
        StatString.push_back( 0 );
        StatString.append(mapWidth );
        StatString.append(mapHeight );
        StatString.append(mapCRC );
        Util::AppendCString(StatString, mapPath );
        Util::AppendCString(StatString,  hostName );
        StatString.push_back( 0 );
        StatString.append( mapSHA1 );
        StatString = Util::EncodeStatString( StatString );

        if( mapGameType.size( ) == 4 && mapFlags.size( ) == 4 && mapWidth.size( ) == 2 && mapHeight.size( ) == 2 && !gameName.empty( ) && !hostName.empty( ) && !mapPath.empty( ) && mapCRC.size( ) == 4 && mapSHA1.size( ) == 20 && StatString.size( ) < 128 && HostCounterQString.size( ) == 8 )
	{
		// make the rest of the packet

		packet.push_back( BNET_HEADER_CONSTANT );						// BNET header constant
		packet.push_back( SID_STARTADVEX3 );							// SID_STARTADVEX3
                packet.push_back((char)0);											// packet length will be assigned later
                packet.push_back((char)0);											// packet length will be assigned later
		packet.push_back( state );										// State (16 = public, 17 = private, 18 = close)
                packet.push_back((char)0);											// State continued...
                packet.push_back((char)0);											// State continued...
                packet.push_back((char)0);											// State continued...
                Util::AppendUInt32( upTime );				// time since creation
                packet.append( mapGameType );				// Game Type, Parameter
                Util::AppendUInt32(packet,255);				// ???
                Util::AppendUInt32(packet,0);					// Custom Game
                Util::AppendCString(gameName);					// Game Name
                packet.push_back((char)0);											// Game Password is NULL
		packet.push_back( 98 );											// Slots Free (ascii 98 = char 'b' = 11 slots free) - note: do not reduce this as this is the # of PID's Warcraft III will allocate
                Util::AppendCString(packet,HostCounterString );	// Host Counter
                packet.append(StatString);					// Stat QString
                packet.push_back((char)0);											// Stat QString null terminator (the stat QString is encoded to remove all even numbers i.e. zeros)
		AssignLength( packet );
	}
	else
		CONSOLE_Print( "[BNETPROTO] invalid parameters passed to SEND_SID_STARTADVEX3" );

	// DEBUG_Print( "SENT SID_STARTADVEX3" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_NOTIFYJOIN( QString gameName )
{
        unsigned char ProductID[]       = {  0, 0, 0, 0 };
	unsigned char ProductVersion[]	= { 14, 0, 0, 0 };	// Warcraft III is 14

        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );			// BNET header constant
	packet.push_back( SID_NOTIFYJOIN );					// SID_NOTIFYJOIN
        packet.push_back((char)0);								// packet length will be assigned later
        packet.push_back((char)0);								// packet length will be assigned later
        packet.append(ProductID);		// Product ID
        packet.append( ProductVersion );	// Product Version
        Util::AppendCString( gameName );		// Game Name
        packet.push_back((char)0);								// Game Password is NULL
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_NOTIFYJOIN" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_PING( QByteArray pingValue )
{
        QByteArray packet;

	if( pingValue.size( ) == 4 )
	{
		packet.push_back( BNET_HEADER_CONSTANT );		// BNET header constant
                packet.push_back( SID_PING );				// SID_PING
                packet.push_back((char)0);					// packet length will be assigned later
                packet.push_back((char)0);					// packet length will be assigned later
                packet.append( pingValue );                             // Ping Value
		AssignLength( packet );
	}
	else
		CONSOLE_Print( "[BNETPROTO] invalid parameters passed to SEND_SID_PING" );

	// DEBUG_Print( "SENT SID_PING" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_LOGONRESPONSE( QByteArray clientToken, QByteArray serverToken, QByteArray passwordHash, QString accountName )
{
        // todotodo: check that the passed QByteArray sizes are correct (don't know what they should be right now so I can't do this today)

        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );			// BNET header constant
	packet.push_back( SID_LOGONRESPONSE );				// SID_LOGONRESPONSE
        packet.push_back((char)0);                  			// packet length will be assigned later
        packet.push_back((char)0);             				// packet length will be assigned later
        packet.append(clientToken );                                    // Client Token
        packet.append( serverToken );                                   // Server Token
        packet.append( passwordHash );                                  // Password Hash
        Util::AppendCString( accountName );                             // Account Name
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_LOGONRESPONSE" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_NETGAMEPORT( quint32 serverPort )
{
        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );			// BNET header constant
	packet.push_back( SID_NETGAMEPORT );				// SID_NETGAMEPORT
        packet.push_back((char)0);						// packet length will be assigned later
        packet.push_back((char)0);						// packet length will be assigned later
        Util::AppendUInt32(serverPort);                         	// local game server port
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_NETGAMEPORT" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_AUTH_INFO( unsigned char ver, bool TFT, QString countryAbbrev, QString country )
{
	unsigned char ProtocolID[]		= {   0,   0,   0,   0 };
	unsigned char PlatformID[]		= {  54,  56,  88,  73 };	// "IX86"
	unsigned char ProductID_ROC[]	= {  51,  82,  65,  87 };	// "WAR3"
	unsigned char ProductID_TFT[]	= {  80,  88,  51,  87 };	// "W3XP"
	unsigned char Version[]			= { ver,   0,   0,   0 };
	unsigned char Language[]		= {  83,  85, 110, 101 };	// "enUS"
	unsigned char LocalIP[]			= { 127,   0,   0,   1 };
	unsigned char TimeZoneBias[]	= {  44,   1,   0,   0 };	// 300 minutes (GMT -0500)
	unsigned char LocaleID[]		= {   9,   4,   0,   0 };	// 0x0409 English (United States)
	unsigned char LanguageID[]		= {   9,   4,   0,   0 };	// 0x0409 English (United States)

        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );				// BNET header constant
        packet.push_back( SID_AUTH_INFO );					// SID_AUTH_INFO
        packet.push_back((char)0);							// packet length will be assigned later
        packet.push_back((char)0);                          			// packet length will be assigned later
        packet.append(ProtocolID );                                     	// Protocol ID
        packet.append(PlatformID );                                     	// Platform ID

	if( TFT )
                packet.append(ProductID_TFT );                                  // Product ID (TFT)
	else
                 packet.append(ProductID_ROC );                                 // Product ID (ROC)

        packet.append( Version );                               		// Version
        packet.append( Language );                                          	// Language
        packet.append( LocalIP );                               		// Local IP for NAT compatibility
        packet.append(TimeZoneBias );                                           // Time Zone Bias
        packet.append(LocaleID );                                       	// Locale ID
        packet.append(LanguageID );                                     	// Language ID
        Util::AppendCString(packet, countryAbbrev );            		// Country Abbreviation
        Util::AppendCString( packet, country );                         	// Country
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_AUTH_INFO" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_AUTH_CHECK( bool TFT, QByteArray clientToken, QByteArray exeVersion, QByteArray exeVersionHash, QByteArray keyInfoROC, QByteArray keyInfoTFT, QString exeInfo, QString keyOwnerName )
{
        quint32 NumKeys = 0;

	if( TFT )
		NumKeys = 2;
	else
		NumKeys = 1;

        QByteArray packet;

	if( clientToken.size( ) == 4 && exeVersion.size( ) == 4 && exeVersionHash.size( ) == 4 && keyInfoROC.size( ) == 36 && ( !TFT || keyInfoTFT.size( ) == 36 ) )
	{
		packet.push_back( BNET_HEADER_CONSTANT );			// BNET header constant
                packet.push_back( SID_AUTH_CHECK );				// SID_AUTH_CHECK
                packet.push_back((char)0);                                          // packet length will be assigned later
                packet.push_back((char)0);						// packet length will be assigned later
                packet.append(clientToken );                                    // Client Token
                packet.append( exeVersion );                                    // EXE Version
                packet.append(exeVersionHash );                                 // EXE Version Hash
                Util::AppendUInt32(packet, NumKeys);            		// number of keys in this packet
                Util::AppendUInt32( packet, (quint32)0 );               	// boolean Using Spawn (32 bit)
                packet.append( keyInfoROC );                                    // ROC Key Info

		if( TFT )
                       packet.append( keyInfoTFT );                             // TFT Key Info

                Util::AppendCString( packet, exeInfo );                         // EXE Info
                Util::AppendCString( packet, keyOwnerName );                    // CD Key Owner Name
		AssignLength( packet );
	}
	else
		CONSOLE_Print( "[BNETPROTO] invalid parameters passed to SEND_SID_AUTH_CHECK" );

	// DEBUG_Print( "SENT SID_AUTH_CHECK" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_AUTH_ACCOUNTLOGON( QByteArray clientPublicKey, QString accountName )
{
        QByteArray packet;

	if( clientPublicKey.size( ) == 32 )
	{
		packet.push_back( BNET_HEADER_CONSTANT );				// BNET header constant
		packet.push_back( SID_AUTH_ACCOUNTLOGON );				// SID_AUTH_ACCOUNTLOGON
                packet.push_back((char)0);                          			// packet length will be assigned later
                packet.push_back((char)0);                          			// packet length will be assigned later
                packet.append( clientPublicKey );                                       // Client Key
                Util::AppendCString( packet, accountName );                     	// Account Name
		AssignLength( packet );
	}
	else
		CONSOLE_Print( "[BNETPROTO] invalid parameters passed to SEND_SID_AUTH_ACCOUNTLOGON" );

	// DEBUG_Print( "SENT SID_AUTH_ACCOUNTLOGON" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_AUTH_ACCOUNTLOGONPROOF( QByteArray clientPasswordProof )
{
        QByteArray packet;

	if( clientPasswordProof.size( ) == 20 )
	{
                packet.push_back( BNET_HEADER_CONSTANT );				// BNET header constant
		packet.push_back( SID_AUTH_ACCOUNTLOGONPROOF );				// SID_AUTH_ACCOUNTLOGONPROOF
                packet.push_back((char)0);                                                  // packet length will be assigned later
                packet.push_back((char)0);							// packet length will be assigned later
                packet.append( clientPasswordProof );                                   // Client Password Proof
		AssignLength( packet );
	}
	else
		CONSOLE_Print( "[BNETPROTO] invalid parameters passed to SEND_SID_AUTH_ACCOUNTLOGON" );

	// DEBUG_Print( "SENT SID_AUTH_ACCOUNTLOGONPROOF" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_WARDEN( QByteArray wardenResponse )
{
        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );			// BNET header constant
        packet.push_back( SID_WARDEN );                 		// SID_WARDEN
        packet.push_back((char)0);						// packet length will be assigned later
        packet.push_back((char)0);						// packet length will be assigned later
        packet.append( wardenResponse );                                // warden response
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_WARDEN" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_FRIENDSLIST( )
{
        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );	// BNET header constant
	packet.push_back( SID_FRIENDSLIST );		// SID_FRIENDSLIST
        packet.push_back((char)0);				// packet length will be assigned later
        packet.push_back((char)0);				// packet length will be assigned later
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_FRIENDSLIST" );
	// DEBUG_Print( packet );
	return packet;
}

QByteArray CBNETProtocol :: SEND_SID_CLANMEMBERLIST( )
{
	unsigned char Cookie[] = { 0, 0, 0, 0 };

        QByteArray packet;
	packet.push_back( BNET_HEADER_CONSTANT );	// BNET header constant
	packet.push_back( SID_CLANMEMBERLIST );		// SID_CLANMEMBERLIST
        packet.push_back((char)0);				// packet length will be assigned later
        packet.push_back((char)0);				// packet length will be assigned later
        UTIL_AppendQByteArray( packet, Cookie, 4 );	// cookie
	AssignLength( packet );
	// DEBUG_Print( "SENT SID_CLANMEMBERLIST" );
	// DEBUG_Print( packet );
	return packet;
}

/////////////////////
// OTHER FUNCTIONS //
/////////////////////

bool CBNETProtocol :: AssignLength( QByteArray &content )
{
	// insert the actual length of the content array into bytes 3 and 4 (indices 2 and 3)

        QByteArray LengthBytes;

	if( content.size( ) >= 4 && content.size( ) <= 65535 )
	{
                LengthBytes = Conversions::ToQByteArray((quint16)content.size());
		content[2] = LengthBytes[0];
		content[3] = LengthBytes[1];
		return true;
	}

	return false;
}

bool CBNETProtocol :: ValidateLength( QByteArray &content )
{
	// verify that bytes 3 and 4 (indices 2 and 3) of the content array describe the length

        quint32 Length;
        QByteArray LengthBytes;

	if( content.size( ) >= 4 && content.size( ) <= 65535 )
	{
		LengthBytes.push_back( content[2] );
		LengthBytes.push_back( content[3] );
                Length = Conversions::ToUInt16(LengthBytes);

		if( Length == content.size( ) )
			return true;
	}

	return false;
}

//
// CIncomingGameHost
//

quint32 CIncomingGameHost :: NextUniqueGameID = 1;

CIncomingGameHost :: CIncomingGameHost( quint32 nGameType, quint32 nParameter, quint32 nLanguageID, quint32 nPort, QByteArray &nIP, quint32 nStatus, quint32 nElapsedTime, QString nGameName, unsigned char nSlotsTotal, quint32 nHostCounter, QByteArray &nStatString )
{
	m_GameType = nGameType;
	m_Parameter = nParameter;
	m_LanguageID = nLanguageID;
	m_Port = nPort;
	m_IP = nIP;
	m_Status = nStatus;
	m_ElapsedTime = nElapsedTime;
	m_GameName = nGameName;
	m_SlotsTotal = nSlotsTotal;
	m_HostCounter = nHostCounter;
        m_StatString = nStatString;
	m_UniqueGameID = NextUniqueGameID++;
	m_ReceivedTime = GetTime( );

        // decode stat QString

        QByteArray StatString = Util::DecodeStatString( m_StatString );
        QByteArray MapFlags;
        QByteArray MapWidth;
        QByteArray MapHeight;
        QByteArray MapCRC;
        QByteArray MapPath;

        if( StatString.size( ) >= 14 )
	{
		unsigned int i = 13;
                MapFlags = StatString.mid(0,4);
                MapWidth = StatString.mid(5,2);
                MapHeight = StatString.mid(7,2);
                MapCRC = StatString.mid(9,4);
                MapPath = Util::ExtractCString(StatString,13);
		i += MapPath.size( ) + 1;

                m_MapFlags = Conversions::ToUInt32(MapFlags);
                m_MapWidth = Conversions::ToUInt16(MapWidth);
                m_MapHeight = Conversions::ToUInt16(MapHeight);
		m_MapCRC = MapCRC;
                m_MapPath = MapPath;

                if( StatString.size( ) >= i + 1 )
		{
                     m_HostName =  Util::ExtractCString(StatString,i);
		}
	}
}

CIncomingGameHost :: ~CIncomingGameHost( )
{

}

QString CIncomingGameHost :: GetIPQString( )
{
        QString Result;

	if( m_IP.size( ) >= 4 )
	{
		for( unsigned int i = 0; i < 4; i++ )
		{
                    Result += Conversions::ToQString((int)m_IP[i]);

			if( i < 3 )
				Result += ".";
		}
	}

	return Result;
}

//
// CIncomingChatEvent
//

CIncomingChatEvent :: CIncomingChatEvent( CBNETProtocol :: IncomingChatEvent nChatEvent, quint32 nPing, QString nUser, QString nMessage )
{
	m_ChatEvent = nChatEvent;
	m_Ping = nPing;
	m_User = nUser;
	m_Message = nMessage;
}

CIncomingChatEvent :: ~CIncomingChatEvent( )
{

}

//
// CIncomingFriendList
//

CIncomingFriendList :: CIncomingFriendList( QString nAccount, unsigned char nStatus, unsigned char nArea, QString nLocation )
{
	m_Account = nAccount;
	m_Status = nStatus;
	m_Area = nArea;
	m_Location = nLocation;
}

CIncomingFriendList :: ~CIncomingFriendList( )
{

}

QString CIncomingFriendList :: GetDescription( )
{
        QString Description;
	Description += GetAccount( ) + "\n";
	Description += ExtractStatus( GetStatus( ) ) + "\n";
	Description += ExtractArea( GetArea( ) ) + "\n";
	Description += ExtractLocation( GetLocation( ) ) + "\n\n";
	return Description;
}

QString CIncomingFriendList :: ExtractStatus( unsigned char status )
{
        QString Result;

	if( status & 1 )
		Result += "<Mutual>";

	if( status & 2 )
		Result += "<DND>";

	if( status & 4 )
		Result += "<Away>";

	if( Result.empty( ) )
		Result = "<None>";

	return Result;
}

QString CIncomingFriendList :: ExtractArea( unsigned char area )
{
	switch( area )
	{
	case 0: return "<Offline>";
	case 1: return "<No Channel>";
	case 2: return "<In Channel>";
	case 3: return "<Public Game>";
	case 4: return "<Private Game>";
	case 5: return "<Private Game>";
	}

	return "<Unknown>";
}

QString CIncomingFriendList :: ExtractLocation( QString location )
{
        QString Result;

	if( location.substr( 0, 4 ) == "PX3W" )
                Result = location.right(4);

	if( Result.empty( ) )
		Result = ".";

	return Result;
}

//
// CIncomingClanList
//

CIncomingClanList :: CIncomingClanList( QString nName, unsigned char nRank, unsigned char nStatus )
{
	m_Name = nName;
	m_Rank = nRank;
	m_Status = nStatus;
}

CIncomingClanList :: ~CIncomingClanList( )
{

}

QString CIncomingClanList :: GetRank( )
{
	switch( m_Rank )
	{
	case 0: return "Recruit";
	case 1: return "Peon";
	case 2: return "Grunt";
	case 3: return "Shaman";
	case 4: return "Chieftain";
	}
	return "Rank Unknown";
}

QString CIncomingClanList :: GetStatus( )
{
	if( m_Status == 0 )
		return "Offline";
	else
		return "Online";
}

QString CIncomingClanList :: GetDescription( )
{
        QString Description;
	Description += GetName( ) + "\n";
	Description += GetStatus( ) + "\n";
	Description += GetRank( ) + "\n\n";
	return Description;
}
