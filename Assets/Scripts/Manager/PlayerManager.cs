using System.Collections.Generic;
using Google.Protobuf.Collections;
using SocketDemoProtocol;
using UnityEngine;

namespace SocketDemo
{
    public class PlayerManager:BaseManager
    {
        public PlayerManager(GameFace face) : base(face)
        {
        }
        
        Dictionary<string,GameObject> players=new Dictionary<string, GameObject>();

        private GameObject character;
        private Transform spawnPos;
        public override void OnInit()
        {
            character=Resources.Load("Character/player") as GameObject;
            base.OnInit();
            
            
        }

        public string CurPlayerID
        {
            get;
            set;
        }
        
        public void AddPlayer(MainPack pack)
        {
            spawnPos = GameObject.Find("spawnPos").transform;
            foreach (var player in pack.PlayerPack)
            {
                GameObject gameObject= GameObject.Instantiate(character, spawnPos.position, Quaternion.identity);
                if (player.PlayerName.Equals(face.Username))
                {
                    //添加本地角色使用的脚本
                    gameObject.AddComponent<PlayerController>();
                }
                else
                {
                    //添加其他客户端角色使用的脚本
                }
                players.Add(player.PlayerName,gameObject);
            }
        }

        public void RemovePlayer(string playerName)
        {
            if (players.TryGetValue(playerName,out GameObject gameObject))
            {
                GameObject.Destroy(gameObject);
                players.Remove(playerName);
                
            }
            else
            {
                Debug.Log("移除角色出错");
            }
        }
    }
}