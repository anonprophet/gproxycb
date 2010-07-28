#ifndef BNCSPACKET_H
#define BNCSPACKET_H

#include "databuffer.h"

class BlizzardPacket : public DataBuffer
{
public:
    BncsPacket(unsigned char id , unsigned char header);
    ~BncsPacket( );
    void AssignLength();

};

#endif // BNCSPACKET_H
