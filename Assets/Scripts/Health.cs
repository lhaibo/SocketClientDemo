using UnityEngine;
using UnityEngine.UI;

namespace SocketDemo
{
    public class Health : MonoBehaviour
    {
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }
    }
}