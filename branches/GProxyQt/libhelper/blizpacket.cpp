#include "blizpacket.h"
#include "QtEndian"

BlizPacket::BlizPacket(unsigned char id,unsigned char header) : DataBuffer()
{
    this->push_back(header);
    this->push_back(id);
    this->push_back(0);
    this->push_back(0);
}
BlizPacket::~BlizPacket()
{

}
void BlizPacket::AssignLength()
{
    uchar length[2];
    qToLittleEndian<quint16>((quint16)(this->size()), length);
    this[2] = length[0];
    this[3] = length[1];
}
