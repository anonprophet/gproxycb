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

#ifndef GPROXY_H
#define GPROXY_H

#include "includes.h"

#include <QTcpSocket>
#include <QTcpServer>
#include <QUdpSocket>

// output

void CONSOLE_Print( const QString &message );
void DEBUG_Print( const QString &message );

//
// CGProxy
//

class QTcpServer;
class QTcpSocket;
class CTCPClient;
class CUDPSocket;
class CBNET;
class CIncomingGameHost;
class CGameProtocol;
class CGPSProtocol;
class CCommandPacket;

class CGProxy : public QObject
{
    Q_OBJECT
public:
        QString m_Version;
        QTcpServer *m_LocalServer;
        QTcpSocket *m_LocalSocket;
        QTcpSocket *m_RemoteSocket;
        QUdpSocket *m_UDPSocket;
	CBNET *m_BNET;
        QList<CIncomingGameHost *> m_Games;
	CGameProtocol *m_GameProtocol;
	CGPSProtocol *m_GPSProtocol;
        QQueue<CCommandPacket *> m_LocalPackets;
        QQueue<CCommandPacket *> m_RemotePackets;
        QQueue<CCommandPacket *> m_PacketBuffer;
        QList<unsigned char> m_Laggers;
        quint32 m_TotalPacketsReceivedFromLocal;
        quint32 m_TotalPacketsReceivedFromRemote;
	bool m_Exiting;
	bool m_TFT;
        QString m_War3Path;
        QString m_CDKeyROC;
        QString m_CDKeyTFT;
        QString m_Server;
        QString m_Username;
        QString m_Password;
        QString m_Channel;
        quint32 m_War3Version;
        quint16 m_Port;
        quint32 m_LastConnectionAttemptTime;
        quint32 m_LastRefreshTime;
        QString m_RemoteServerIP;
        quint16 m_RemoteServerPort;
	bool m_GameIsReliable;
	bool m_GameStarted;
	bool m_LeaveGameSent;
	bool m_ActionReceived;
	bool m_Synchronized;
        quint16 m_ReconnectPort;
	unsigned char m_PID;
	unsigned char m_ChatPID;
        quint32 m_ReconnectKey;
	unsigned char m_NumEmptyActions;
	unsigned char m_NumEmptyActionsUsed;
        quint32 m_LastAckTime;
        quint32 m_LastActionTime;
        QString m_JoinedName;
        QString m_HostName;

        CGProxy( bool nTFT, QString nWar3Path, QString nCDKeyROC, QString nCDKeyTFT, QString nServer, QString nUsername, QString nPassword, QString nChannel, quint32 nWar3Version, quint16 nPort, QByteArray nEXEVersion, QByteArray nEXEVersionHash, QString nPasswordHashType );
	~CGProxy( );

	// processing functions

	bool Update( long usecBlock );

	void ExtractLocalPackets( );
	void ProcessLocalPackets( );
	void ExtractRemotePackets( );
	void ProcessRemotePackets( );

	bool AddGame( CIncomingGameHost *game );
        void SendLocalChat( QString message );
	void SendEmptyAction( );
};

#endif
