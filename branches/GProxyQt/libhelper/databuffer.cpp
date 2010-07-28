
#include "databuffer.h"

DataBuffer::DataBuffer : QByteArray( )        
{

}
DataBuffer::~DataBuffer()
{

}
DataBuffer::InsertUInt16(quint16 i)
{
    uchar dest[2];
    qToLittleEndian<quint16>(value, dest);
    this->append(dest);
}
DataBuffer::InsertUInt32(quint32 i)
{
    uchar dest[2];
    qToLittleEndian<quint32>(value, dest);
    this->append(dest);
}
DataBuffer::InsertUInt64(quint64 i)
{
    uchar dest[2];
    qToLittleEndian<quint64>(value, dest);
    this->append(dest);
}
{
    this->InsertString( i);
    this->push_back(terminator);
}
DataBuffer::InsertCString(QString i)
{
    this->append(i);
    this->push_back(0);
}
