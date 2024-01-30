using System;
using UnityEngine;

public abstract class WarriarData : ScriptableObject
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpHaight;

    [SerializeField] private int _helth;
    [SerializeField] private int _damage;
    [SerializeField] private int _atackCoolDown;

    public MovementController CreateMovementController(CharacterController characterController)
    {
        if (characterController == null) throw new ArgumentNullException();

        return new MovementController(characterController, _moveSpeed, _jumpDuration, _jumpHaight);
    }

    public Health CreateHealth()
    {
        return new Health(_helth);
    }

    public Attacker CreateAttacker(UpdateServise updateServise)
    {
        if (updateServise == null) throw new ArgumentNullException();
        return new Attacker(_damage, _atackCoolDown, updateServise);
    }
}
