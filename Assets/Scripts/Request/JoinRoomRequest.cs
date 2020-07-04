using System;
using SocketDemo;
using SocketDemoProtocol;

namespace Request
{
    public class JoinRoomRequest:BaseRequest
    {
        private MainPack pack = null;
        private RoomListPanel roomListPanel;
        public override void Start()
        {
            
            requestCode = RequestCode.Room;
            actionCode = ActionCode.JoinRoom;
            roomListPanel = GetComponent<RoomListPanel>();
            base.Start();
        }

        private void Update()
        {
            if (pack!=null)
            {
                roomListPanel.JoinRoomOnResponse(pack);
                pack = null;
            }
        }

        public void SendRequest(string roomName)
        {
            MainPack pack=new MainPack();
            pack.RequestCode = requestCode;
            pack.ActionCode = actionCode;
            pack.JoinRoomName = roomName;
            base.SendRequest(pack);
        }

        public override void OnResponse(MainPack pack)
        {
            this.pack = pack;
        }
    }
}