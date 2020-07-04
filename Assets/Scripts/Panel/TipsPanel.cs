using System;
using UnityEngine.UI;

namespace SocketDemo
{
    public class TipsPanel:BasePanel
    {
        public Text text;
        public override void OnEnter()
        {
            base.OnEnter();
            gameObject.SetActive(true);
            uiManager.SetTipsPanel(this);
            text.CrossFadeAlpha(0,2.0f,false);
        }

        public void ShowTips(string str,bool sync=false)
        {
            ShowText(str);
        }

        private void ShowText(string str)
        {
            text.text = str;
            text.CrossFadeAlpha(1,1.0f,false);
            Invoke("HideText",2);
        }
        
        private void HideText()
        {
            text.CrossFadeAlpha(0,1.0f,false);
        }
        
    }
}