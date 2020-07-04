using UnityEngine;

namespace SocketDemo
{
    public class BasePanel : MonoBehaviour
    {
        protected UIManager uiManager;

        public UIManager UiManager
        {
            set => uiManager = value;
        }

        public virtual void OnEnter()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnPause()
        {
            gameObject.SetActive(false);

        }

        public virtual void OnResume()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void OnExit()
        {
            gameObject.SetActive(false);

        }
    }
}