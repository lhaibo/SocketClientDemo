using System;
using System.Collections.Generic;
using Request;
using SocketDemoProtocol;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SocketDemo
{
    public class RoomPanel:BasePanel
    {
        [SerializeField] private Transform userInfor;
        [SerializeField] private Button enterBtn;
        [SerializeField] private Button exitBtn;
        [SerializeField] private Button startBtn;
        [SerializeField] private InputField inputField;
        [SerializeField] private GameObject userInforItem;
        [SerializeField] private Text chatContent;
        [SerializeField] private RoomExitRequest roomExitRequest;
        [SerializeField] private ChatRequest chatRequest;
        [SerializeField] private StartGameRequest startGameRequest;
        private void Awake()
        {
            startGameRequest = GetComponent<StartGameRequest>();
            chatRequest = GetComponent<ChatRequest>();
            roomExitRequest = GetComponent<RoomExitRequest>();
        }

        private void Start()
        {
            startBtn.onClick.AddListener(StartBtnOnClick);
            exitBtn.onClick.AddListener(ExitBtnOnClick);
            enterBtn.onClick.AddListener(EnterBtnOnClick);
        }

        private void StartBtnOnClick()
        {
            startGameRequest.SendRequest();
        }

        private void ExitBtnOnClick()
        {
            roomExitRequest.SendRequest();
        }

        private void EnterBtnOnClick()
        {
            if (inputField.text=="")
            {
                uiManager.ShowTips("聊天信息不能为空");
            }
            else
            {
                chatRequest.SendRequest(inputField.text);
                chatContent.text += "我:" + inputField.text+"\n";
                inputField.text = "";
            }
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
        /// <summary>
        /// 更新玩家列表
        /// </summary>
        /// <param name="pack"></param>
        public void UpdatePlayerList(MainPack pack)
        {
            for (int i = 0; i < userInfor.childCount; i++)
            {
                Destroy(userInfor.GetChild(i).gameObject);
            }

            foreach (PlayerPack player in pack.PlayerPack)
            {
                UserInforItem inforItem = Instantiate(userInforItem, Vector3.zero, Quaternion.identity,userInfor)
                    .GetComponent<UserInforItem>();
                inforItem.SetPlayerInfor(player.PlayerName,player.WinCount);
            }
        }

        public void ExitOnResponse()
        {
            uiManager.PopPanel();
        }

        public void ChatOnResponse(string chatStr)
        {
            chatContent.text += chatStr+"\n";
        }

        public void StartGameResponse(MainPack pack)
        {
            switch (pack.ReturnCode)
            {
                case ReturnCode.Fail:
                    uiManager.ShowTips("开始游戏失败!您不是房主!");
                    break;
                case ReturnCode.Succeed:
                    uiManager.ShowTips("游戏即将开始!");
                    break;
                default:
                    Debug.Log("returnCode有问题!");
                    break;
            }
        }

        public void GameStarting(MainPack pack)
        {
            SceneManager.LoadScene("Game");
        }
    }
}