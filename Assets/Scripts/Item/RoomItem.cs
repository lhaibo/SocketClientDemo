using System;
using Request;
using SocketDemoProtocol;
using UnityEngine;
using UnityEngine.UI;

namespace SocketDemo
{
    public class RoomItem:MonoBehaviour
    {
        public Button join;
        public Text roomName;
        public Text playerNum;
        public Text roomState;

        private RoomListPanel roomListPanel;

        public RoomListPanel RoomListPanel
        {
            get
            {
                return roomListPanel;
            }
            set
            {
                roomListPanel = value;
            }
        }

        private void Start()
        {
            join.onClick.AddListener(OnJoinClick);
        }

        private void OnJoinClick()
        {
            roomListPanel.JoinRoom(roomName.text);
        }

        public void SetRoomInfor(string roomName, int currentNum,int maxNum, RoomState state)
        {
            this.roomName.text = roomName;
            this.playerNum.text = currentNum+"/"+maxNum;
            switch (state)
            {
                case RoomState.Gaming:
                    this.roomState.text = "游戏中";
                    break;
                case RoomState.Waitting:
                    this.roomState.text = "等待加入";
                    break;
                case RoomState.Full:
                    this.roomState.text = "房间人数已满";
                    break;
            }
        }
    }
}