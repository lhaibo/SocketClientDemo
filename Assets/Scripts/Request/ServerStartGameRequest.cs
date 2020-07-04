using System;
using System.Linq;
using SocketDemo;
using SocketDemoProtocol;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Request
{
    public class ServerStartGameRequest:BaseRequest
    {
        private MainPack pack;
        private RoomPanel roomPanel;
        public override void Start()
        {
            roomPanel = GetComponent<RoomPanel>();
            actionCode = ActionCode.ServerStartGame;
            base.Start();
        }

        private void Update()
        {
            if (pack!=null)
            {
                Debug.Log("游戏正式开始");
                //SceneManager.LoadScene("Game");
                GameFace.instance.allPlayer = pack;
                roomPanel.GameStarting(pack);
                pack = null;
            }
        }

        public override void OnResponse(MainPack pack)
        {
            this.pack = pack;
        }
    }
}