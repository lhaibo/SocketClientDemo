using System;
using SocketDemo;
using SocketDemoProtocol;

namespace Request
{
    public class PlayerRequest:BaseRequest
    {
        private MainPack pack = null;
        private RoomPanel roomPanel;
        public override void Start()
        {
            requestCode = RequestCode.Room;
            actionCode = ActionCode.PlayerList;
            roomPanel = GetComponent<RoomPanel>();
            base.Start();
        }

        private void Update()
        {
            if (pack!=null)
            {
                roomPanel.UpdatePlayerList(pack);
                pack = null;
            }
        }

        public override void OnResponse(MainPack pack)
        {
            this.pack = pack;
        }
    }
}