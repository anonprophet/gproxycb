#ifndef BLIZPACKET_H
#define BLIZPACKET_H

#include "databuffer.h"

class BlizPacket : public DataBuffer
{
public:
    BlizPacket(unsigned char id ,unsigned char header);
    ~BlizPacket( );
    void AssignLength();

};

#endif // BNCSPACKET_H
