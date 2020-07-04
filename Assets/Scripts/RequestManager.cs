using System.Collections.Generic;
using Request;
using SocketDemoProtocol;
using UnityEngine;

namespace SocketDemo
{
    public class RequestManager : BaseManager
    {
        public RequestManager(GameFace face) : base(face)
        {
        }
        
        private Dictionary<ActionCode,BaseRequest> requestDict=new Dictionary<ActionCode, BaseRequest>();

        public void AddRequest(BaseRequest request)
        {
            requestDict.Add(request.ActionCode,request);
        }

        public void RemoveRequest(ActionCode actionCode)
        {
            requestDict.Remove(actionCode);
        }

        public void HandleReponse(MainPack pack)
        {
            if (requestDict.TryGetValue(pack.ActionCode,out BaseRequest request))
            {
                request.OnResponse(pack);
            }
            else
            {
                Debug.Log("没有找到对应的处理!");
            }
        }
    }
}