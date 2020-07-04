using UnityEngine;

namespace SocketDemo
{
    public class Player
    {
        public Transform transform;
        public Transform gunTransform;
        public bool isSelf;
        
        public Player(Transform transform, Transform gunTransform)
        {
            this.transform = transform;
            this.gunTransform = gunTransform;
        }

        public void SetDir(Vector3 dir)
        {
            transform.up = dir;
        }

        public void Move(Vector3 moveDir,float speed)
        {
            transform.position += moveDir * speed*Time.deltaTime;
        }

        public void Shoot(Transform bullet)
        {
            GameObject.Instantiate(bullet, gunTransform.position+gunTransform.up*0.5f,gunTransform.rotation);
        }

 
    }
}