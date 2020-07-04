using System;
using SocketDemo;
using SocketDemoProtocol;

namespace Request
{
    public class ChatRequest:BaseRequest
    {
        private string chatStr=null;
        private RoomPanel roomPanel;
        public override void Start()
        {
            requestCode = RequestCode.Room;
            actionCode = ActionCode.Chat;
            roomPanel = GetComponent<RoomPanel>();
            base.Start();
        }

        private void Update()
        {
            if (chatStr!=null)
            {
                roomPanel.ChatOnResponse(chatStr);
                chatStr = null;
            }
        }

        public void SendRequest(string str)
        {
            MainPack pack=new MainPack();
            pack.RequestCode = requestCode;
            pack.ActionCode = actionCode;
            pack.ChatStr = str;
            base.SendRequest(pack);
        }

        public override void OnResponse(MainPack pack)
        {
            chatStr = pack.ChatStr;
        }
    }
}