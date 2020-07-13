using System;
using SocketDemoProtocol;

namespace Request
{
    public class GameExitRequest:BaseRequest
    {
        private MainPack pack;
        public override void Start()
        {
            requestCode = RequestCode.Game;
            actionCode = ActionCode.GameExit;
            base.Start();
        }

        private void Update()
        {
            if (pack!=null)
            {
                GameFace.instance.GameExit(pack);
                pack = null;
            }
        }

        public void SendRequest(string playerName)
        {
            MainPack pack=new MainPack();
            pack.RequestCode = requestCode;
            pack.ActionCode = actionCode;
            pack.ExitGameName = playerName;
            base.SendRequest(pack);
        }

        public override void OnResponse(MainPack pack)
        {
            this.pack = pack;
        }
    }
}