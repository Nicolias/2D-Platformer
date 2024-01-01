using UnityEngine;

public static class AnimatorData
{
    public static class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int Teleport = Animator.StringToHash(nameof(Teleport));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }
}