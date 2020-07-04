using System;
using System.Collections;
using System.Collections.Generic;
using Request;
using SocketDemo;
using SocketDemoProtocol;
using UnityEngine;
using UnityEngine.UI;

public class LogonPanel : BasePanel
{
    private LogonRequest logonRequest;
    // Start is called before the first frame update
    public InputField username, password;
    public Button logonBtn,returnBtn;
    private void Awake()
    {
        logonRequest = GetComponent<LogonRequest>();
        
    }

    private void OnLogonClick()
    {
        if (username.text == "" || password.text == "")
        {
            Debug.Log("用户名或密码不能为空");
        }
        else
        {
            Debug.Log("点击注册");
            logonRequest.SendRequest(username.text,password.text);
            
        }
    }
    
    private void OnReturnBtnClick()
    {
        uiManager.PushPanel(PanelType.Login);
    }
    
    private void Start()
    {
        logonBtn.onClick.AddListener(OnLogonClick);
        returnBtn.onClick.AddListener(OnReturnBtnClick);
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
                uiManager.ShowTips("注册成功");
                uiManager.PushPanel(PanelType.Login);
                Debug.Log("注册成功ok!");
                break;
            case ReturnCode.Fail:
                uiManager.ShowTips("注册失败");
                Debug.Log("注册失败!");
                break;
            default:
                Debug.Log("returnCode存在问题!");
                break;
        }
    }
}
