using System;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    public event Action TeleportShowed;
    public event Action DeadShowed;

    public void OnTeleportAnimationShowed()
    {
        TeleportShowed?.Invoke();
    }

    public void OnDeadAnimationShowed()
    {
        DeadShowed?.Invoke();
    }
}