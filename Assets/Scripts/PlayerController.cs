using System;
using System.Collections;
using System.Collections.Generic;
using Request;
using SocketDemo;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player player;
    [SerializeField] private float speed=3.0f;
    public Transform bullet;
    [SerializeField] private float shootDeltaTime = 0.2f;
    private float timer = 0.0f;
    
    private void Awake()
    {
        player=new Player(this.GetComponent<Transform>(),this.GetComponentInChildren<Transform>(),this.GetComponent<ShootRequest>());
        bullet = ((GameObject)Resources.Load("bullet")).transform;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = false;
    }

    private void Update()
    {
        
        player.Move(GetMoveDir(),speed);
        player.SetDir(GetTargetDir());

        if (CanShoot())
        {
            player.Shoot(bullet);
            timer = shootDeltaTime;
        }
    }

    private bool CanShoot()
    {

        if (Input.GetMouseButton(0)&&timer<=0.0f)
        {
            return true;
        }
        else
        {
            timer -= Time.deltaTime;
            return false;
        }
        
    }
    
    private Vector3 GetMoveDir()
    {
        Vector3 moveDir=Vector3.zero;

        moveDir.y = Input.GetKey(KeyCode.W) ? 1.0f : 0f - (Input.GetKey(KeyCode.S) ? 1.0f : 0f);
        moveDir.x=Input.GetKey(KeyCode.D) ? 1.0f : 0f - (Input.GetKey(KeyCode.A) ? 1.0f : 0f);

        return moveDir.normalized;
    }
    
    private Vector3 GetTargetDir()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(player.transform.position);
        float deltaX = Input.mousePosition.x - playerScreenPos.x;
        float deltaY = Input.mousePosition.y - playerScreenPos.y;
        return new Vector3(deltaX,deltaY,0.0f);
    }
}
