using System.Collections.Generic;
using UnityEngine;

namespace SocketDemo
{
    public class UIManager : BaseManager
    {
        private Dictionary<PanelType,BasePanel> panelDict=new Dictionary<PanelType, BasePanel>();
        private Dictionary<PanelType,string> panelPathDict=new Dictionary<PanelType, string>();
        private Stack<BasePanel> panelStack=new Stack<BasePanel>();
        public Transform Canvas { get;set; }

        private TipsPanel tipsPanel;
        
        public UIManager(GameFace face) : base(face)
        {
        }

        public override void OnInit()
        {
            base.OnInit();
            InitPanel();
            // PushPanel(PanelType.Start);
            // PushPanel(PanelType.Tips);
        }
        /// <summary>
        /// 关闭当前ui
        /// </summary>
        public void PopPanel()
        {
            if (panelStack.Count == 0) return;

            BasePanel topPanel = panelStack.Pop();
            topPanel.OnExit();

            BasePanel panel = panelStack.Peek();
            panel.OnResume();
        }
        
        
        /// <summary>
        /// 把ui显示在界面上
        /// </summary>
        /// <param name="panelType"></param>
        public BasePanel PushPanel(PanelType panelType)
        {
            if (panelDict.TryGetValue(panelType,out BasePanel panel))
            {
                if (panelStack.Count>0)
                {
                    BasePanel topPanel=panelStack.Peek();
                    topPanel.OnPause();
                }
                
                panelStack.Push(panel);
                panel.OnEnter();
                return panel;
            }
            else
            {
                BasePanel newPanel= SpawnPanel(panelType);
                if (panelStack.Count>0)
                {
                    BasePanel topPanel=panelStack.Peek();
                    topPanel.OnPause();
                }
                panelStack.Push(newPanel);
                newPanel.OnEnter();
                return newPanel;
            }
        }

        /// <summary>
        /// 实例化对应的ui
        /// </summary>
        /// <param name="panelType"></param>
        private BasePanel SpawnPanel(PanelType panelType)
        {
            if (panelPathDict.TryGetValue(panelType,out string path))
            {
                GameObject gameObject = GameObject.Instantiate(Resources.Load(path), Canvas) as GameObject;
                
                panelDict.Add(panelType,gameObject.GetComponent<BasePanel>());
                panelDict[panelType].UiManager = this;
                return panelDict[panelType];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPanel()
        {
            string panelPath = "Panel/";
            string[] path = new[] {"StartPanel", "LoginPanel", "LogonPanel", "TipsPanel","RoomListPanel","RoomPanel","GamePanel"};
            
            panelPathDict.Add(PanelType.Tips,panelPath+path[3]);
            panelPathDict.Add(PanelType.Start,panelPath+path[0]);
            panelPathDict.Add(PanelType.Login,panelPath+path[1]);
            panelPathDict.Add(PanelType.Logon,panelPath+path[2]);
            panelPathDict.Add(PanelType.RoomList,panelPath+path[4]);
            panelPathDict.Add(PanelType.Room,panelPath+path[5]);
            panelPathDict.Add(PanelType.Game,panelPath+path[6]);
            //Canvas=GameObject.Find("Canvas").transform;
        }

        public void SetTipsPanel(TipsPanel tipsPanel)
        {
            this.tipsPanel = tipsPanel;
        }
        
        public void ShowTips(string str,bool sync=false)
        {
            if(tipsPanel.gameObject.activeSelf==false)tipsPanel.gameObject.SetActive(true);
            tipsPanel.ShowTips(str);
        }

        public void ClearUI()
        {
            panelDict.Clear();
            panelStack.Clear();
            tipsPanel = null;
            Canvas = null;
        }
    }
}