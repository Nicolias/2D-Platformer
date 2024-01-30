using System;
using UnityEngine;

public abstract class WarriarData : ScriptableObject
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpHaight;

    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _atackCoolDown;

    public Movement CreateMovementController(CharacterController characterController)
    {
        if (characterController == null) throw new ArgumentNullException();

        return new Movement(characterController, _moveSpeed, _jumpDuration, _jumpHaight);
    }

    public Health CreateHealth()
    {
        return new Health(_health);
    }

    public Attacker CreateAttacker(UpdateServise updateServise)
    {
        if (updateServise == null) throw new ArgumentNullException();
        return new Attacker(_damage, _atackCoolDown, updateServise);
    }
}
