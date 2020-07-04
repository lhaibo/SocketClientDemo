using System;
using SocketDemo;
using SocketDemoProtocol;

namespace Request
{
    public class CreatRoomRequest:BaseRequest
    {
        private RoomListPanel roomListPanel;
        private MainPack pack=null;
        public override void Start()
        {
            roomListPanel = GetComponent<RoomListPanel>();
            requestCode = RequestCode.Room;
            actionCode = ActionCode.CreatRoom;
            base.Start();
        }

        private void Update()
        {
            if (pack!=null)
            {
                roomListPanel.CreatRoomOnResponse(pack);
                pack = null;
            }
        }

        public void SendRequest(string roomName,int maxNum)
        {
            MainPack pack=new MainPack();
            pack.RequestCode = requestCode;
            pack.ActionCode = actionCode;
            RoomPack roomPack=new RoomPack();
            roomPack.RoomName = roomName;
            roomPack.MaxNum = maxNum;
            pack.RoomPack.Add(roomPack);
            base.SendRequest(pack);
        }

        public override void OnResponse(MainPack pack)
        {
            this.pack = pack;
        }
    }
}