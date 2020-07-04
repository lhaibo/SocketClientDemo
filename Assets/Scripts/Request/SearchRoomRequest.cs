using System;
using SocketDemo;
using SocketDemoProtocol;

namespace Request
{
    public class SearchRoomRequest:BaseRequest
    {
        
        private RoomListPanel roomListPanel;
        private MainPack pack=null;
        
        public override void Start()
        {
            roomListPanel = GetComponent<RoomListPanel>();
            requestCode = RequestCode.Room;
            actionCode = ActionCode.SearchRoom;
            base.Start();
        }
        
        private void Update()
        {
            if (pack!=null)
            {
                roomListPanel.SearchRoomOnResponse(pack);
                pack = null;
            }
        }

        public void SendRequest()
        {
            MainPack pack=new MainPack();
            pack.RequestCode = requestCode;
            pack.ActionCode = actionCode;
            pack.Str = "查询房间";
            base.SendRequest(pack);
            
        }

        public override void OnResponse(MainPack pack)
        {
            this.pack = pack;
        }
    }
}