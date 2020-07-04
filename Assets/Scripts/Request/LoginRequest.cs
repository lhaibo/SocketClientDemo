using System;
using SocketDemo;
using SocketDemoProtocol;
using UnityEngine;

namespace Request
{
    public class LoginRequest:BaseRequest
    {
        private LoginPanel loginPanel;
        private MainPack pack;
        public override void Start()
        {
            requestCode = RequestCode.User;
            actionCode = ActionCode.Login;
            loginPanel=GetComponent<LoginPanel>();
            base.Start();
        }

        private void Update()
        {
            if (pack!=null)
            {
                loginPanel.OnResponse(pack);
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