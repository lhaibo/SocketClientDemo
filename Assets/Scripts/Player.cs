using Request;
using UnityEngine;

namespace SocketDemo
{
    [System.Serializable]
    public class Player
    {
        public Transform transform;
        public Transform gunTransform;
        public bool isSelf;

        private ShootRequest shootRequest;
        
        public Player(Transform transform, Transform gunTransform,ShootRequest shootRequest)
        {
            this.transform = transform;
            this.gunTransform = gunTransform;
            this.shootRequest = shootRequest;
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
            Vector3 gunTransformPosition = gunTransform.position+gunTransform.up*0.5f;
            shootRequest.SendRequest(gunTransformPosition,gunTransform.eulerAngles.z);
            GameObject.Instantiate(bullet, gunTransformPosition,gunTransform.rotation);
        }

 
    }
}