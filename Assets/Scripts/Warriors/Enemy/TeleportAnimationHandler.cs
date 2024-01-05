using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class TeleportAnimationHandler : MonoBehaviour
    {
        public event UnityAction Showed;

        public void OnAnimationShowed()
        {
            Showed?.Invoke();
        }
    }
}