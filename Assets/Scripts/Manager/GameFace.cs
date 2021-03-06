﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Request;
using SocketDemo;
using SocketDemoProtocol;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFace : MonoBehaviour
{
    public static GameFace instance;
    private ClientManager clientManager;
    private RequestManager requestManager;
    private UIManager uiManager;
    private PlayerManager playerManager;
    
    public string  Username { get; set; }
    public MainPack allPlayer { get; set; }
    public PanelType initPanelType=PanelType.Start;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        
        uiManager=new UIManager(this);
        clientManager=new ClientManager(this);
        requestManager=new RequestManager(this);
        playerManager=new PlayerManager(this);
        
        uiManager.OnInit();
        requestManager.OnInit();
        clientManager.OnInit();
        playerManager.OnInit();
    }

    private void Start()
    {
        
        
    }

    private void OnDestroy()
    {
        clientManager.OnDestroy();
        requestManager.OnDestroy();
    }

    public void Send(MainPack pack)
    {
        clientManager.Send(pack);
    }
    
    public void HandleResponse(MainPack pack)
    {
        //处理
        requestManager.HandleReponse(pack);
    }

    public void AddRequest(BaseRequest request)
    {
        requestManager.AddRequest(request);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestManager.RemoveRequest(actionCode);
    }

    public void ShowTips(string str,bool sync=false)
    {
        uiManager.ShowTips(str,sync);
    }

    public void SetSelfID(string id)
    {
        playerManager.CurPlayerID = id;
    }

    public void AddPlayer(MainPack packs)
    {
        playerManager.AddPlayer(packs);
    }

    public void RemovePlayer(string id)
    {
        playerManager.RemovePlayer(id);
    }

    public void SpawnBullet(MainPack pack)
    {
        playerManager.SpawnBullet(pack);
    }
    
    public void GameExit(MainPack pack)
    {
        if (pack.Str=="房主退出")
        {
            playerManager.Clear();
            GameFace.instance.initPanelType = PanelType.RoomList;
            SceneManager.LoadScene("LoginAndLogon");
        }
        else
        {
            playerManager.RemovePlayer(pack.ExitGameName);
        }
    }
    
    
    public void ShowInitPanel()
    {
        uiManager.ClearUI();
        uiManager.Canvas = GameObject.Find("Canvas").GetComponent<Transform>();
        uiManager.PushPanel(PanelType.Tips);
        uiManager.PushPanel(initPanelType);
    }

    public void UpdatePos(MainPack pack)
    {
        playerManager.UpdatePos(pack);
    }

    public void SendTo(MainPack pack)
    {
        pack.Username = Username;
        clientManager.SendTo(pack);
    }
}
