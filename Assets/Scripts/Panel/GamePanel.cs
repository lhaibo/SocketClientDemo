using System;
using System.Collections.Generic;
using System.Net.Mime;
using Request;
using SocketDemoProtocol;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SocketDemo
{
    public class GamePanel:BasePanel
    {
        [SerializeField] private Text time;
        [SerializeField] private Button exitBtn;
        private GameExitRequest gameExitRequest;
        private float startTime;
        
        private void FixedUpdate()
        {
            time.text=""+Mathf.Clamp((int) (Time.time - startTime), 0, 60);
        }

        private void Start()
        {
            gameExitRequest = GetComponent<GameExitRequest>();
            GameFace.instance.AddPlayer(GameFace.instance.allPlayer);
            
            startTime = Time.time;
            exitBtn.onClick.AddListener(OnExitBtnClick);
        }

        // private void Update()
        // {
        //     //如果只剩下一位玩家
        // }

        private void OnExitBtnClick()
        {
            SceneManager.LoadScene("LoginAndLogon");
            uiManager.PushPanel(PanelType.RoomList);
            //发送给服务端
            gameExitRequest.SendRequest(GameFace.instance.Username);
            
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
        }

        public override void OnResume()
        {
            gameObject.SetActive(true);

        }
    }
}