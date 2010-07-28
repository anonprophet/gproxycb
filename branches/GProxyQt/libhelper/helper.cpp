#include "helper.h"
#include <QtEndian>
#include <QTextStream>

//////////////////
////// UTIL //////
//////////////////

void Util::AppendUInt16(QByteArray &data,quint16 i)
{
    uchar value[2];
    qToLittleEndian<quint16>(i, value);
    data.append((char *)value);
}
void Util::AppendUInt32(QByteArray &data, quint32 i)
{
    uchar value[4];
    qToLittleEndian<quint32>(i, value);
    data.append((char *)value);
}
void Util::AppendUInt64(QByteArray &data, quint64 i)
{
    uchar value[8];
    qToLittleEndian<quint64>(i, value);
    data.append((char *)value);
}
void Util::AppendCString(QByteArray &data,QString i)
{
    data.append(i);
    data.push_back("\0");
}
quint16 Util::ExtractUInt16(QByteArray &data, int start)
{
    return qFromLittleEndian<quint16>((uchar*)data.mid(start, 2).data());
}
quint32 Util::ExtractUInt32(QByteArray &data, int start)
{
    return qFromLittleEndian<quint32>((uchar*)data.mid(start, 4).data());
}
QString Util::ExtractCString(QByteArray &data, int start)
{
    QString str;
    for( QByteArray ::const_iterator i = data.begin()+ start ; i != data.end();i++)
    {
        if ( (*i) == 0 )
            break;
        str.push_back((unsigned char)(*i));
    }
    return str.toAscii();
}
QByteArray Util::Reverse(const QByteArray &data)
{
        QByteArray res;
        QByteArray::const_iterator it;
        for (it = data.end() - 1; it == data.begin(); it--)
                res.push_back(*it);

        return res;
}
QString Util::Reverse(QString data)
{
    QString res;
    QString::const_iterator it;
    for (it = data.end() - 1; it == data.begin(); it--)
            res.push_back(*it);

    return res;
}
QByteArray Util::EncodeStatString( QByteArray &data )
{
    unsigned char Mask = 1;
    QByteArray Result;

    for( int i = 0; i < data.size( ); i++ )
    {
            if( ( data[i] % 2 ) == 0 )
                    Result.push_back( data[i] + 1 );
            else
            {
                    Result.push_back( data[i] );
                    Mask |= 1 << ( ( i % 7 ) + 1 );
            }

            if( i % 7 == 6 || i == data.size( ) - 1 )
            {
                    Result.insert( Result.size() - 1 - ( i % 7 ), Mask);
                    Mask = 1;
            }
    }

    return Result;
}
QByteArray Util::DecodeStatString( QByteArray &data )
{
        unsigned char Mask;
        QByteArray Result;

        for( unsigned int i = 0; i < data.size( ); i++ )
        {
                if( ( i % 8 ) == 0 )
                        Mask = data[i];
                else
                {
                        if( ( Mask & ( 1 << ( i % 8 ) ) ) == 0 )
                                Result.push_back( data[i] - 1 );
                        else
                                Result.push_back( data[i] );
                }
        }

        return Result;
}

/////////////////
// CONVERSIONS //
/////////////////

quint16 Conversions::ToUInt16(QString i)
{
    return qFromLittleEndian<quint16>((uchar *)i.data());
}
quint32 Conversions::ToUInt32(QString i)
{
    return qFromLittleEndian<quint16>((uchar *)i.data());
}
QByteArray Conversions::ToQByteArray(QString i)
{
    return QByteArray((char *)i.data(),i.size());
}
QByteArray Conversions::ToQByteArray(quint16 i)
{
    uchar value[2];
    qToLittleEndian<quint16>(i, value);
    return QByteArray((char *)value,2);
}
QByteArray Conversions::ToQByteArray(quint32 i)
{
    uchar value[4];
    qToLittleEndian<quint32>(i, value);
    return QByteArray((char *)value,4);
}
QByteArray Conversions::ToQByteArray(quint64 i)
{
    uchar value[8];
    qToLittleEndian<quint64>(i, value);
    return QByteArray((char *)value,8);
}
QString Conversions::ByteArrayToDecQString(QByteArray data)
{
    if( data.isEmpty( ) )
            return QString( );

    QString result = QString::number( (unsigned char)data[0] );

    for( QByteArray :: const_iterator i = data.begin( ) + 1; i != data.end( ); i++ )
            result += " " + QString::number( (unsigned char)*i );

    return result;
}
quint16 Conversions::ToUInt16(QByteArray i )
{
    return qFromLittleEndian<quint16>((uchar*)i.data());
}
quint32 Conversions::ToUInt32(QByteArray i)
{
     return qFromLittleEndian<quint32>((uchar*)i.data());
}
QString Conversions::ToHexQString(QString i)
{
    QString res;
    QTextStream ss;
    ss << hex << i;
    ss >> res;
    return res;
}
QString Conversions::ToHexQString(quint32 i)
{
    QString res;
    QTextStream ss;
    ss << hex << i;
    ss >> res;
    return res;
}
QString Conversions::ToQString( int i )
{
    QString res;
    QTextStream ss;
    ss << i;
    ss >> res;
    return res;
}
