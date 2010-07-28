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

   CODE PORTED FROM THE ORIGINAL GHOST PROJECT: http://ghost.pwner.org/

*/

#include "gproxy.h"
#include "util.h"
#include "bncsutilinterface.h"

#include <bncsutil/bncsutil.h>
#include <QDir>
#include <libhelper/helper.h>

//
// CBNCSUtilInterface
//

CBNCSUtilInterface :: CBNCSUtilInterface( QString userName, QString userPassword )
{
	// m_nls = (void *)nls_init( userName.c_str( ), userPassword.c_str( ) );
	m_NLS = new NLS( userName, userPassword );
}

CBNCSUtilInterface :: ~CBNCSUtilInterface( )
{
	// nls_free( (nls_t *)m_nls );
	delete (NLS *)m_NLS;
}

void CBNCSUtilInterface :: Reset( QString userName, QString userPassword )
{
	// nls_free( (nls_t *)m_nls );
	// m_nls = (void *)nls_init( userName.c_str( ), userPassword.c_str( ) );
	delete (NLS *)m_NLS;
	m_NLS = new NLS( userName, userPassword );
}

bool CBNCSUtilInterface :: HELP_SID_AUTH_CHECK( bool TFT, QString war3Path, QString keyROC, QString  keyTFT, QByteArray valueQStringFormula, QByteArray  mpqFileName, QByteArray  clientToken, QByteArray  serverToken )
{
	// set m_EXEVersion, m_EXEVersionHash, m_EXEInfo, m_InfoROC, m_InfoTFT

        QString FileWar3EXE = war3Path + "war3.exe";
        QString FileStormDLL = war3Path + "Storm.dll";

        if( !QDir::exists( FileStormDLL ) )
		FileStormDLL = war3Path + "storm.dll";

        QString FileGameDLL = war3Path + "game.dll";
        bool ExistsWar3EXE = QDir::exists( FileWar3EXE );
        bool ExistsStormDLL = QDir::exists( FileStormDLL );
        bool ExistsGameDLL = QDir::exists( FileGameDLL );

	if( ExistsWar3EXE && ExistsStormDLL && ExistsGameDLL )
	{
		// todotodo: check getExeInfo return value to ensure 1024 bytes was enough

		char buf[1024];
		uint32_t EXEVersion;
		getExeInfo( FileWar3EXE.c_str( ), (char *)&buf, 1024, (uint32_t *)&EXEVersion, BNCSUTIL_PLATFORM_X86 );
		m_EXEInfo = buf;
                m_EXEVersion = UTIL_CreateQString( EXEVersion, false );
		uint32_t EXEVersionHash;
                checkRevisionFlat( valueQStringFormula.c_str( ), FileWar3EXE.toStdString().c_str(), FileStormDLL.toStdString().c_str( ), FileGameDLL.toStdString().c_str( ), extractMPQNumber( mpqFileName.toStdString().c_str( ) ), (unsigned long *)&EXEVersionHash );
                m_EXEVersionHash = Conversions::ToQByteArray(EXEVersionHash);
                m_KeyInfoROC = CreateKeyInfo( keyROC, Conversions::ToUInt32(clientToken),Conversions::ToUInt32(serverToken) );

		if( TFT )
                        m_KeyInfoTFT = CreateKeyInfo( keyTFT,Conversions::ToUInt32(clientToken),Conversions::ToUInt32(serverToken) );

		if( m_KeyInfoROC.size( ) == 36 && ( !TFT || m_KeyInfoTFT.size( ) == 36 ) )
			return true;
		else
		{
			if( m_KeyInfoROC.size( ) != 36 )
				CONSOLE_Print( "[BNCSUI] unable to create ROC key info - invalid ROC key" );

			if( TFT && m_KeyInfoTFT.size( ) != 36 )
				CONSOLE_Print( "[BNCSUI] unable to create TFT key info - invalid TFT key" );
		}
	}
	else
	{
		if( !ExistsWar3EXE )
			CONSOLE_Print( "[BNCSUI] unable to open [" + FileWar3EXE + "]" );

		if( !ExistsStormDLL )
			CONSOLE_Print( "[BNCSUI] unable to open [" + FileStormDLL + "]" );

		if( !ExistsGameDLL )
			CONSOLE_Print( "[BNCSUI] unable to open [" + FileGameDLL + "]" );
	}

	return false;
}

bool CBNCSUtilInterface :: HELP_SID_AUTH_ACCOUNTLOGON( )
{
	// set m_ClientKey

	char buf[32];
	// nls_get_A( (nls_t *)m_nls, buf );
	( (NLS *)m_NLS )->getPublicKey( buf );
        m_ClientKey = QByteArray((unsigned char *)buf, 32 );
	return true;
}

bool CBNCSUtilInterface :: HELP_SID_AUTH_ACCOUNTLOGONPROOF( QByteArray salt, QByteArray serverKey )
{
	// set m_M1

	char buf[20];
        // nls_get_M1( (nls_t *)m_nls, buf, QString( serverKey.begin( ), serverKey.end( ) ).c_str( ), QString( salt.begin( ), salt.end( ) ).c_str( ) );
        ( (NLS *)m_NLS )->getClientSessionKey( buf, QString( salt.begin( ), salt.end( ) ).c_str( ), QString( serverKey.begin( ), serverKey.end( ) ).c_str( ) );
        m_M1 = QByteArray((unsigned char *)buf, 20 );
	return true;
}

bool CBNCSUtilInterface :: HELP_PvPGNPasswordHash( QString userPassword )
{
	// set m_PvPGNPasswordHash

	char buf[20];
	hashPassword( userPassword.c_str( ), buf );
        m_PvPGNPasswordHash = QByteArray( (unsigned char *)buf, 20 );
	return true;
}

QByteArray CBNCSUtilInterface :: CreateKeyInfo( QString key, quint32 clientToken, quint32 serverToken )
{
	unsigned char Zeros[] = { 0, 0, 0, 0 };
        QByteArray KeyInfo;
	CDKeyDecoder Decoder( key.c_str( ), key.size( ) );

	if( Decoder.isKeyValid( ) )
	{
                KeyInfo.append(Conversions::ToQByteArray((quint32)key.size( ) ));
                KeyInfo.append(Conversions::ToQByteArray((quint32) Decoder.getProduct( ) ) );
                KeyInfo.append( Conversions::ToQByteArray( (quint32 )Decoder.getVal1( ) ) );
                KeyInfo.append(Conversions::ToQByteArray((quint32)0));
		size_t Length = Decoder.calculateHash( clientToken, serverToken );
		char *buf = new char[Length];
		Length = Decoder.getHash( buf );
                KeyInfo.append(QByteArray( (unsigned char *)buf,(int)Length));
		delete [] buf;
	}

	return KeyInfo;
}
