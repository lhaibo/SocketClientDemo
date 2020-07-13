using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SocketDemoProtocol;
using UnityEngine;

namespace SocketDemo
{
    public class ClientManager:BaseManager
    {
        private Socket socket;
        private Message message;
        private string ip = "127.0.0.1";
        public ClientManager(GameFace face) : base(face)
        {
            
        }
        /// <summary>
        /// 初始化socket
        /// </summary>
        public override void OnInit()
        {
            if (face.Username==null)
            {
                base.OnInit();
                message=new Message();
                InitSocket();
                InitUDP();
            }
        }
        /// <summary>
        /// 关闭socket
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();
            message = null;
            CloseSocket();
        }

        private void InitSocket()
        {
            socket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            try
            {
                socket.Connect(ip,6666);
                StartReceive();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        private void CloseSocket()
        {
            if (socket.Connected&&socket!=null)
            {
                socket.Close();
            }
            
        }

        private void StartReceive()
        {
            socket.BeginReceive(message.Buffer, message.StartIndex, message.RemSize, SocketFlags.None, ReceiveCallBack,null);
            Debug.Log("开始接收消息...");
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            if (socket == null || socket.Connected == false)
            {
                Debug.Log("socket == null || socket.Connected == false");
                return;
            }
            try
            {
                int len = socket.EndReceive(ar);
                if (len==0)
                {
                    CloseSocket();
                    return;
                }
                message.ReadBuffer(len,HandleResponse);
                StartReceive();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        private void HandleResponse(MainPack pack)
        {
            face.HandleResponse(pack);
            Debug.Log("处理消息");

        }

        public void Send(MainPack pack)
        {
            socket.Send(Message.PackData(pack));
            Debug.Log("消息已发送");
        }

        
        
        //udp
        
        private Socket udpClient;
        private IPEndPoint ipEndPoint;
        private EndPoint endPoint;
        private Byte[] buffer=new byte[1024];
        private Thread receiveMsgThread;
        
        private void InitUDP()
        {
            udpClient=new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            ipEndPoint=new IPEndPoint(IPAddress.Parse(ip),6667);
            endPoint = (EndPoint) ipEndPoint;
            try
            {
                udpClient.Connect(endPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine("udp连接失败!"+e.Message);
                throw;
            }
            
            //receiveMsgThread=new Thread(ReceiveMsg);
            //receiveMsgThread.Start();
            Loom.RunAsync(() =>
            {
                receiveMsgThread=new Thread(ReceiveMsg);
                receiveMsgThread.Start();
            });
        }

        private void ReceiveMsg()
        {
            Debug.Log("udp开始接收");
            while (true)
            {
                int len = udpClient.ReceiveFrom(buffer, ref endPoint);
                MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 0, len);
                
                Loom.QueueOnMainThread((param) =>
                {
                    HandleResponse(pack);
                },null);
                //HandleResponse(pack);
            }
        }

        public void SendTo(MainPack pack)
        {
            Byte[] sendBuff = Message.PackDataUDP(pack);
            udpClient.Send(sendBuff, sendBuff.Length, SocketFlags.None);
        }

    }
}