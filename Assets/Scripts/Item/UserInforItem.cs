using UnityEngine;
using UnityEngine.UI;

namespace SocketDemo
{
    public class UserInforItem:MonoBehaviour
    {
        [SerializeField] private Text playerName;
        [SerializeField] private Text winCount;

        public void SetPlayerInfor(string playerName,int winCount)
        {
            this.playerName.text = playerName;
            this.winCount.text = "" + winCount;
        }
        
    }
}