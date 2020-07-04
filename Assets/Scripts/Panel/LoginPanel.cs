using Request;
using SocketDemoProtocol;
using UnityEngine;
using UnityEngine.UI;

namespace SocketDemo
{
    public class LoginPanel:BasePanel
    {
        private LoginRequest loginRequest;
        // Start is called before the first frame update
        public InputField username, password;
        public Button loginBtn,logonBtn;
        
        private void Awake()
        {
            loginRequest = GetComponent<LoginRequest>();
        }
    
        private void OnLoginClick()
        {
            if (username.text == "" || password.text == "")
            {
                Debug.Log("用户名或密码不能为空");
            }
            else
            {
                Debug.Log("点击登录");
                loginRequest.SendRequest(username.text,password.text);
                
            }
        }
    
        private void OnLogonBtnClick()
        {
            uiManager.PushPanel(PanelType.Logon);
        }
        
        private void Start()
        {
            loginBtn.onClick.AddListener(OnLoginClick);
            logonBtn.onClick.AddListener(OnLogonBtnClick);
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

        public void OnResponse(MainPack pack)
        {
            switch (pack.ReturnCode)
            {
                case ReturnCode.Succeed:
                    //Debug.Log("注册成功!");
                    uiManager.ShowTips("登录成功");
                    GameFace.instance.Username = username.text;
                    uiManager.PushPanel(PanelType.RoomList);
                    break;
                case ReturnCode.Fail:
                    uiManager.ShowTips("登录失败");
                    //Debug.Log("注册失败!");
                    break;
                default:
                    Debug.Log("returnCode存在问题!");
                    break;
            }
        }
    }
}