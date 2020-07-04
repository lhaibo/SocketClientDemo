using System;
using SocketDemo;
using SocketDemoProtocol;
using UnityEngine;

namespace Request
{
    public class RoomExitRequest:BaseRequest
    {
        private bool isExit = false;
        private RoomPanel roomPanel;
        public override void Start()
        {
            roomPanel = GetComponent<RoomPanel>();
            requestCode = RequestCode.Room;
            actionCode = ActionCode.Exit;
            base.Start();
        }

        private void Update()
        {
            if (isExit)
            {
                roomPanel.ExitOnResponse();
                isExit = false;
            }
        }

        public void SendRequest()
        {
            MainPack pack=new MainPack();
            pack.RequestCode = requestCode;
            pack.ActionCode = actionCode;
            pack.Str = "退出房间";

            base.SendRequest(pack);
        }

        public override void OnResponse(MainPack pack)
        {
            isExit = true;
            Debug.Log("离开房间");
        }
    }
}