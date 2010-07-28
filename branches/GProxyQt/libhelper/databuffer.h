#ifndef DATABUFFER_H
#define DATABUFFER_H

#include <QByteArray>

class DataBuffer : public QByteArray
{
Q_OBJECT
public:
    DataBuffer( );
    ~DataBuffer( );

    void InsertUInt16( quint16 i );
    void InsertUInt32( quint32 i );
    void InsertUInt64( quint64 i );
    void InsertCString( const QString i );

signals:

public slots:

};

#endif // DATABUFFER_H
