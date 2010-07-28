#ifndef HELPER_H
#define HELPER_H

#include <QString>
#include <QByteArray>

class Util
{
public:
    static void AppendUInt16( QByteArray &data,quint16 i );
    static void AppendUInt32( QByteArray &data,quint32 i );
    static void AppendUInt64( QByteArray &data,quint64 i );
    static void AppendCString( QByteArray &data,const QString i );
    static quint16 ExtractUInt16( QByteArray &data, int start);
    static quint32 ExtractUInt32( QByteArray &data, int start);
    static QString ExtractCString( QByteArray &data,int start);
    static QByteArray Reverse( const QByteArray &data);
    static QString Reverse( const QString data);
    static QByteArray EncodeStatString( QByteArray &data );
    static QByteArray DecodeStatString( QByteArray &data );
};
class Conversions
{
public:
    static QByteArray ToQByteArray( quint16 i );
    static QByteArray ToQByteArray( quint32 i );
    static QByteArray ToQByteArray( quint64 i );
    static QByteArray ToQByteArray( QString i );
    static QString ToHexQString( QString i);
    static QString ToHexQString( quint32 i);




    static quint16 ToUInt16( QString i );
    static quint32 ToUInt32( QString i );
    static quint16 ToUInt16( QByteArray i);
    static quint32 ToUInt32( QByteArray i);
    static QString ToQString( int i);

    static QString ByteArrayToDecQString( QByteArray data );
   };

#endif // HELPER_H
