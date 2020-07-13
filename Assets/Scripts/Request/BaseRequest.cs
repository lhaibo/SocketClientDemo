using System;
using SocketDemoProtocol;
using UnityEngine;

namespace Request
{
    public class BaseRequest : MonoBehaviour
    {
        protected RequestCode requestCode;
        protected ActionCode actionCode;
        
        
        public ActionCode ActionCode
        {
            get => actionCode;
        }

        public virtual void Start()
        {
            GameFace.instance.AddRequest(this);
            Debug.Log(this);
        }

        public virtual void OnDestroy()
        {
            GameFace.instance.RemoveRequest(actionCode);
        }

        public virtual void OnResponse(MainPack pack)
        {
            
        }
        
        public virtual void SendRequest(MainPack pack)
        {
            GameFace.instance.Send(pack);
        }

        protected void SendRequestUDP(MainPack pack)
        {
            GameFace.instance.SendTo(pack);
        }
    }
}