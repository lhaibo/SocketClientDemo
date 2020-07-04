using System;
using System.Linq;
using SocketDemo;
using SocketDemoProtocol;

namespace Request
{
    public class StartGameRequest:BaseRequest
    {
        private MainPack pack;
        private RoomPanel roomPanel;
        public override void Start()
        {
            roomPanel = GetComponent<RoomPanel>();
            requestCode = RequestCode.Room;
            actionCode = ActionCode.StartGame;
            base.Start();
        }

        private void Update()
        {
            if (pack!=null)
            {
                //GameFace.instance.AddPlayer(pack);
                roomPanel.StartGameResponse(pack);
                pack = null;
            }
        }

        public void SendRequest()
        {
            MainPack pack=new MainPack();
            pack.RequestCode = requestCode;
            pack.ActionCode = actionCode;
            pack.Str = "开始游戏";
            base.SendRequest(pack);
        }


        public override void OnResponse(MainPack pack)
        {
            this.pack = pack;
        }
    }
}