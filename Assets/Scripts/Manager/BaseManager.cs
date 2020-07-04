﻿using UnityEngine;

namespace SocketDemo
{
    public class BaseManager
    {
        protected GameFace face;

        public BaseManager(GameFace face)
        {
            this.face = face;
        }
        
        public virtual void OnInit()
        {
            
        }
        public virtual void OnDestroy()
        {
            
        }
    }
}