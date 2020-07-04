using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace SocketDemo
{
    public class GameManager : MonoBehaviour
    {
        public string ip="127.0.0.1";

        private Socket Client;

        private void Awake()
        {
            IPAddress ipAddress=IPAddress.Parse(ip);
            Client=new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Client.Connect(new IPEndPoint(ipAddress, 8885));
                Debug.Log("连接服务器成功！");
            }
            catch (Exception e)
            {
                Debug.Log("连接服务器失败！"+e.Message);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            Client.Send(Encoding.ASCII.GetBytes("client Say Hello to server"));
            Debug.Log("连接成功!");
        }

        private void Update()
        {
            //Debug.Log(Time.frameCount);
            
        }
    }
}