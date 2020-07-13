using System;
using System.Linq;
using Google.Protobuf;
using SocketDemoProtocol;
using UnityEngine;

namespace SocketDemo
{
    public class Message
    {
        private byte[] buffer = new byte[1024];

        private int startIndex;

        public byte[] Buffer 
        { 
            get => buffer; 
            set => buffer = value; 
        }
        public int StartIndex 
        { 
            get => startIndex; 
            set => startIndex = value; 
        }

        public int RemSize
        {
            get => buffer.Length - startIndex;
        }
        
        public void ReadBuffer(int len,Action<MainPack> HandleResponse)
        {
            startIndex += len;
            
            while (true)
            {
                if (startIndex <= 4)
                {
                    return;
                }
                int count = BitConverter.ToInt32(buffer, 0);
                if (startIndex>=(count+4))
                {
                    MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 4, count);
                    HandleResponse(pack);
                    Array.Copy(buffer, count + 4, buffer, 0, startIndex - count - 4);
                    startIndex -= (count + 4);
                }
                else
                {
                    break;
                }
            }
            Debug.Log("readbuffer处理完毕!");
        }

        public static byte[] PackData(MainPack pack)
        {
            byte[] data = pack.ToByteArray();
            byte[] head = BitConverter.GetBytes(data.Length);
            return head.Concat(data).ToArray();
        }
        
        public static byte[] PackDataUDP(MainPack pack)
        {
            return pack.ToByteArray();
        }
    }
}