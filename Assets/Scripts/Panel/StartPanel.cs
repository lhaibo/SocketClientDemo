using System;
using UnityEngine.UI;

namespace SocketDemo
{
    public class StartPanel:BasePanel
    {
        public Button startBtn;

        private void Start()
        {
            startBtn.onClick.AddListener(startButton);
        }

        private void startButton()
        {
            OnExit();
            uiManager.PushPanel(PanelType.Login);
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