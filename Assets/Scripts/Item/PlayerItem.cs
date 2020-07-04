using UnityEngine;
using UnityEngine.UI;

namespace SocketDemo
{
    public class PlayerItem : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void Set(int health)
        {
            slider.value = health;
        }
    }
}