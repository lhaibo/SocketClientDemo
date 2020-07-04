using System;
using System.Collections.Generic;
using Request;
using SocketDemoProtocol;
using UnityEngine;
using UnityEngine.UI;

namespace SocketDemo
{
    public class RoomListPanel:BasePanel
    {
        [SerializeField] private Button logoutBtn;
        [SerializeField] private Button searchBtn;
        [SerializeField] private Button creatRoomBtn;
        [SerializeField] private InputField roomNameInputField;
        [SerializeField] private Transform roomList;
        [SerializeField] private GameObject roomItem;
        [SerializeField] private CreatRoomRequest creatRoomRequest;
        [SerializeField] private SearchRoomRequest searchRoomRequest;
        [SerializeField] private JoinRoomRequest joinRoomRequest;
        [SerializeField] private Slider numSlider;
        private void Awake()
        {
            joinRoomRequest = GetComponent<JoinRoomRequest>();
            creatRoomRequest = GetComponent<CreatRoomRequest>();
            searchRoomRequest = GetComponent<SearchRoomRequest>();
        }

        private void Start()
        {
            logoutBtn.onClick.AddListener(OnLogoutBtnClick);
            searchBtn.onClick.AddListener(OnSearchBtnClick);
            creatRoomBtn.onClick.AddListener(OnCreatRoomBtnClick);
        }

        /// <summary>
        /// 注销登陆
        /// </summary>
        private void OnLogoutBtnClick()
        {
             uiManager.PushPanel(PanelType.Login);
        }
        /// <summary>
        /// 获取房间
        /// </summary>
        private void OnSearchBtnClick()
        {
            searchRoomRequest.SendRequest();
        }
        /// <summary>
        /// 创建房间
        /// </summary>
        private void OnCreatRoomBtnClick()
        {
            if (roomNameInputField.text=="")
            {
                uiManager.ShowTips("房间名不能为空");
                return;
            }
            creatRoomRequest.SendRequest(roomNameInputField.text,(int)numSlider.value);
        }
        
        
        public override void OnEnter()
        {
            gameObject.SetActive(true);
        }

        public override void OnExit()
        {
            gameObject.SetActive(false);
        }

        public override void OnPause()
        {
            gameObject.SetActive(false);
        }

        public override void OnResume()
        {
            gameObject.SetActive(true);
        }

        public void CreatRoomOnResponse(MainPack pack)
        {
            //面板做出处理
            switch (pack.ReturnCode)
            {
                case ReturnCode.Succeed:
                    //Debug.Log("注册成功!");
                    uiManager.ShowTips("创建成功");
                    RoomPanel roomPanel=uiManager.PushPanel(PanelType.Room) as RoomPanel;
                    roomPanel.UpdatePlayerList(pack);
                    break;
                case ReturnCode.Fail:
                    uiManager.ShowTips("创建失败");
                    //Debug.Log("注册失败!");
                    break;
                default:
                    Debug.Log("returnCode存在问题!");
                    break;
            }
        }

        public void SearchRoomOnResponse(MainPack pack)
        {
            switch (pack.ReturnCode)
            {
                case ReturnCode.Succeed:
                    //Debug.Log("注册成功!");
                    uiManager.ShowTips("查询成功!一共有"+pack.RoomPack.Count+"个房间");
                    break;
                case ReturnCode.Fail:
                    uiManager.ShowTips("查询出错!");
                    
                    //Debug.Log("注册失败!");
                    break;
                case ReturnCode.NoRoom:
                    uiManager.ShowTips("没有房间!");
                    break;
                default:
                    Debug.Log("returnCode存在问题!");
                    break;
            }
            UpdateRoomList(pack);
        }

        private void UpdateRoomList(MainPack pack)
        {
            //清空房间列表
            for (int i = 0; i < roomList.childCount; i++)
            {
                Destroy(roomList.GetChild(i).gameObject);
            }
            //添加房间
            foreach (RoomPack roomPack in pack.RoomPack)
            {
                RoomItem item = Instantiate(roomItem, Vector3.zero, Quaternion.identity,roomList).GetComponent<RoomItem>();
                item.RoomListPanel = this;

                item.SetRoomInfor(roomPack.RoomName,roomPack.CurrentNum,roomPack.MaxNum,roomPack.RoomState);
            }
            
        }
        
        public void JoinRoomOnResponse(MainPack pack)
        {
            switch (pack.ReturnCode)
            {
                case ReturnCode.Succeed:
                    //Debug.Log("注册成功!");
                    uiManager.ShowTips("加入房间成功!");
                    RoomPanel roomPanel=uiManager.PushPanel(PanelType.Room) as RoomPanel;
                    roomPanel.UpdatePlayerList(pack);
                    break;
                case ReturnCode.Fail:
                    uiManager.ShowTips("加入房间失败");
                    //Debug.Log("注册失败!");
                    break;
                case ReturnCode.NoRoom:
                    uiManager.ShowTips("没有此房间!");
                    break;
                            
                default:
                    Debug.Log("returnCode存在问题!");
                    break;
            }
        }

        public void JoinRoom(string roomName)
        {
            joinRoomRequest.SendRequest(roomName);
        }
    }
}