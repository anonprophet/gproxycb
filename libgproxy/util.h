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

#ifndef UTIL_H
#define UTIL_H

// byte arrays

QByteArray UTIL_CreateQByteArray( unsigned char *a, int size );
QByteArray UTIL_CreateQByteArray( unsigned char c );
QByteArray UTIL_CreateQByteArray( quint16 i, bool reverse );
QByteArray UTIL_CreateQByteArray( quint32 i, bool reverse );
quint16 UTIL_QByteArrayToUInt16( QByteArray b, bool reverse, unsigned int start = 0 );
quint32 UTIL_QByteArrayToUInt32( QByteArray b, bool reverse, unsigned int start = 0 );
QString UTIL_QByteArrayToDecQString( QByteArray b );
QString UTIL_QByteArrayToHexQString( QByteArray b );
void UTIL_AppendQByteArray( QByteArray &b, QByteArray append );
void UTIL_AppendQByteArrayFast( QByteArray &b, QByteArray &append );
void UTIL_AppendQByteArray( QByteArray &b, unsigned char *a, int size );
void UTIL_AppendQByteArray( QByteArray &b, QString append, bool terminator = true );
void UTIL_AppendQByteArrayFast( QByteArray &b, QString &append, bool terminator = true );
void UTIL_AppendQByteArray( QByteArray &b, quint16 i, bool reverse );
void UTIL_AppendQByteArray( QByteArray &b, quint32 i, bool reverse );
QByteArray UTIL_ExtractCQString( QByteArray &b, unsigned int start );
unsigned char UTIL_ExtractHex( QByteArray &b, unsigned int start, bool reverse );
QByteArray UTIL_ExtractNumbers( QString s, unsigned int count );
QByteArray UTIL_ExtractHexNumbers( QString s );

// conversions

QString UTIL_ToQString( unsigned long i );
QString UTIL_ToQString( unsigned short i );
QString UTIL_ToQString( unsigned int i );
QString UTIL_ToQString( long i );
QString UTIL_ToQString( short i );
QString UTIL_ToQString( int i );
QString UTIL_ToQString( float f, int digits );
QString UTIL_ToQString( double d, int digits );
QString UTIL_ToHexQString( quint32 i );
quint16 UTIL_ToUInt16( QString &s );
quint32 UTIL_ToUInt32( QString &s );
qint16 UTIL_ToInt16( QString &s );
qint16 UTIL_ToInt32( QString &s );
double UTIL_ToDouble( QString &s );
QString UTIL_MSToQString( quint32 ms );

// files

bool UTIL_FileExists( QString file );
QString UTIL_FileRead( QString file, quint32 start, quint32 length );
QString UTIL_FileRead( QString file );
bool UTIL_FileWrite( QString file, unsigned char *data, quint32 length );
QString UTIL_FileSafeName( QString fileName );
QString UTIL_AddPathSeperator( QString path );

// stat QStrings

QByteArray UTIL_EncodeStatQString( QByteArray &data );
QByteArray UTIL_DecodeStatQString( QByteArray &data );

#endif
