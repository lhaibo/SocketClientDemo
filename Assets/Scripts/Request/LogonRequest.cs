using System;
using SocketDemo;
using SocketDemoProtocol;
using UnityEngine;

namespace Request
{
    public class LogonRequest:BaseRequest
    {
        private LogonPanel logonPanel;
        private MainPack pack;
        public override void Start()
        {
            requestCode = RequestCode.User;
            actionCode = ActionCode.Logon;
            logonPanel = this.GetComponent<LogonPanel>();
            base.Start();
        }

        private void Update()
        {
            if (pack!=null)
            {
                logonPanel.OnResponse(pack);
                pack = null;
            }
        }

        public override void OnResponse(MainPack pack)
        {
            this.pack = pack;
        }

        public void SendRequest(string username,string password)
        {
            MainPack pack=new MainPack();
            pack.RequestCode = requestCode;
            pack.ActionCode = actionCode;
            LoginPack loginPack=new LoginPack();
            loginPack.Username = username;
            loginPack.Password = password;
            pack.LoginPack = loginPack;
            
            base.SendRequest(pack);
        }
    }
}