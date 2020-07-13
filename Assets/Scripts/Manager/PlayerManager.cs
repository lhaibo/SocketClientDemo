using System.Collections.Generic;
using DefaultNamespace;
using Google.Protobuf.Collections;
using Request;
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
        private GameObject bullet;
        private Transform spawnPos;
        public override void OnInit()
        {
            base.OnInit();
            character=Resources.Load("Character/player") as GameObject;
            bullet=Resources.Load("bullet") as GameObject;
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
                    gameObject.AddComponent<UpdatePosRequest>();
                    gameObject.AddComponent<ShootRequest>();
                    
                    gameObject.AddComponent<UpdatePosition>();
                    
                    gameObject.AddComponent<PlayerController>();
                }
                else
                {
                    gameObject.AddComponent<RemotePlayer>();
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

        public void UpdatePos(MainPack pack)
        {
            PostionPack postionPack = pack.PlayerPack[0].PostionPack;
            if (players.TryGetValue(pack.PlayerPack[0].PlayerName,out GameObject gameObject))
            {
                Vector3 pos=new Vector3(postionPack.X,postionPack.Y,0);
                float rotZ = postionPack.RotZ;

                gameObject.GetComponent<RemotePlayer>().SetState(pos, rotZ);
            }
        }

        public void Clear()
        {
            players.Clear();
        }

        public void SpawnBullet(MainPack pack)
        {
            Vector3 pos=new Vector3(pack.BulletPack.X,pack.BulletPack.Y,0);
            GameObject.Instantiate(bullet, pos, Quaternion.Euler(0, 0, pack.BulletPack.RotZ));
        }
        
    }
}