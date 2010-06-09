using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


public class BYTEARRAY : List<byte>
{
    public BYTEARRAY(byte[] data) : base(data) { }
    public BYTEARRAY() : base() { }


    /////////////////////
    // Add Functions ///
    ////////////////////

    /// <summary>
    /// Adds a string to the bytearray based on the encoding provided
    /// </summary>
    /// <param name="enc">Encoding</param>
    /// <param name="i"></param>
    public void AddString(Encoding enc, string i)
    {
        this.AddRange(enc.GetBytes(i));
    }
    /// <summary>
    /// Adds a string to the bytearray base on the encoding provided plus a terminator
    /// </summary>
    /// <param name="enc"></param>
    /// <param name="i"></param>
    /// <param name="terminator"></param>
    public void AddTerminatedString(Encoding enc, string i, byte terminator)
    {
        this.AddString(enc, i);
        this.Add(terminator);
    }
    /// <summary>
    /// Adds a null terminated string to the bytearray
    /// </summary>
    /// <param name="i"></param>
    public void AddCString(string i)
    {
        this.AddTerminatedString(Encoding.ASCII, i, 0);
    }
    /// <summary>
    /// Adds a int16 (short) in the bytearray
    /// </summary>
    /// <param name="i"></param>
    public void AddInt16(Int16 i)
    {
        this.AddRange(BitConverter.GetBytes(i));
    }
    /// <summary>
    /// Adds a int32 (int) in the bytearray
    /// </summary>
    /// <param name="i"></param>
    public void AddInt32(Int32 i)
    {
        this.AddRange(BitConverter.GetBytes(i));
    }
    /// <summary>
    /// Adds a int64 (long) in the bytearray
    /// </summary>
    /// <param name="i"></param>
    public void AddInt64(Int64 i)
    {
        this.AddRange(BitConverter.GetBytes(i));
    }
    /// <summary>
    /// Adds an unsinged int16 ( ushort ) in the bytearray
    /// </summary>
    /// <param name="i"></param>
    public void AddUInt16(UInt16 i)
    {
        this.AddRange(BitConverter.GetBytes(i));
    }
    /// <summary>
    /// Adds an unsigned int32 ( uint ) in the bytearray
    /// </summary>
    /// <param name="i"></param>
    public void AddUInt32(UInt32 i)
    {
        this.AddRange(BitConverter.GetBytes(i));
    }
    /// <summary>
    /// Adds an unsigned int64 ( ulong ) in the bytearray
    /// </summary>
    /// <param name="i"></param>
    public void AddUInt64(UInt64 i)
    {
        this.AddRange(BitConverter.GetBytes(i));
    }

    //////////////////////////
    ///// Get Functions //////
    /////////////////////////

    /// <summary>
    /// Gets a byte from the array and then removes it from the array
    /// </summary>
    /// <returns></returns>
    public byte GetByte()
    {
        byte i = this[0];
        this.RemoveAt(0);
        return i;
    }
    /// <summary>
    /// Gets a bytearray from the array and then removes it from the array
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public byte[] GetArray(int length)
    {
        byte[] r;
        if (length < this.Count)
        {
            r = this.GetRange(0, length).ToArray();
            this.RemoveRange(0, length);
        }
        else
        {
            r = this.ToArray();
            this.Clear();
            return r;
        }
        return r;
    }
    /// <summary>
    /// Gets a string based on the appearance of a specific terminator using the provided encoding
    /// </summary>
    /// <param name="enc"></param>
    /// <param name="terminator"></param>
    /// <returns></returns>
    public string GetTerminatedString(Encoding enc, byte terminator)
    {
        int i = this.IndexOf(terminator);
        if (i == -1)
            return enc.GetString(this.GetArray(this.Count));
        else
        {
            string res = enc.GetString(this.GetArray(i));
            this.RemoveAt(0);
            return res;
        }
    }
    /// <summary>
    /// Gets a null terminated string
    /// </summary>
    /// <returns></returns>
    public string GetCString()
    {
        return GetTerminatedString(Encoding.ASCII, 0);
    }
    /// <summary>
    /// Gets an int16 from the array
    /// </summary>
    /// <returns></returns>
    public Int16 GetInt16()
    {
        return BitConverter.ToInt16(GetArray(2), 0);
    }
    /// <summary>
    /// Gets an int32 from the array
    /// </summary>
    /// <returns></returns>
    public Int32 GetInt32()
    {
        return BitConverter.ToInt32(GetArray(4), 0);
    }
    /// <summary>
    /// Gets an int64 from the array
    /// </summary>
    /// <returns></returns>
    public Int64 GetInt64()
    {
        return BitConverter.ToInt64(GetArray(8), 0);
    }
    public UInt16 GetUInt16()
    {
        return BitConverter.ToUInt16(GetArray(2), 0);
    }
    /// <summary>
    /// Gets an UInt32 from the array
    /// </summary>
    /// <returns></returns>
    public UInt32 GetUInt32()
    {
        return BitConverter.ToUInt32(GetArray(4), 0);
    }
    /// <summary>
    /// Gets an UInt64 from the array
    /// </summary>
    /// <returns></returns>
    public UInt64 GetUInt64()
    {
        return BitConverter.ToUInt64(GetArray(8), 0);
    }

}