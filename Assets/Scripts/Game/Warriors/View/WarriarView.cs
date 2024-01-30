using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AnimationEventsHandler))]
public abstract class WarriarView : MonoBehaviour, IDamagable, IToggleable
{
    private MovementController _movement;
    private WarriarPresenter _warriarPresenter;
    private AbstractInput _input;
    private DetecterHandler _detecterHandler;

    protected AnimationEventsHandler AnimationEventsHandler { get; private set; }
    protected WarriarAnimationHandler AnimationHandler { get; private set; }
    protected UpdateServise UpdateServise { get; private set; }

    protected abstract AbstractDetector Detector { get; }

    public event Action Died;

    public void Initialize(UpdateServise updateServise)
    {
        if (updateServise == null) throw new ArgumentNullException();

        gameObject.SetActive(true);

        UpdateServise = updateServise;

        _movement = GetMovementController(GetComponent<CharacterController>());
        _warriarPresenter = GetPresenter();
        _input = GetMoveInput(_movement);

        AnimationEventsHandler = GetComponent<AnimationEventsHandler>();
        AnimationHandler = new WarriarAnimationHandler(_movement, GetComponent<Animator>(), transform, updateServise);
        _detecterHandler = new DetecterHandler(_warriarPresenter, Detector);

        OnInitialized();
    }

    void IToggleable.Enable()
    {
        AnimationEventsHandler.DeadShowed += OnDead;

        UpdateServise.AddToFixedUpdate(_movement);
        UpdateServise.AddToFixedUpdate(_input);

        _detecterHandler.Enable();
        _warriarPresenter.Enable();

        OnEnabled();
    }

    void IToggleable.Disable()
    {
        AnimationEventsHandler.DeadShowed -= OnDead;

        UpdateServise.RemoveFromFixedUpdate(_movement);
        UpdateServise.RemoveFromFixedUpdate(_input);

        _detecterHandler.Disable();
        _warriarPresenter.Disable();

        OnDisabled();
    }

    void IDamagable.Damage(int value)
    {
        _warriarPresenter.Damage(value);
    }

    public void ShowAttackAnimation()
    {
        AnimationHandler.Animator.SetTrigger(AnimatorData.Params.Attack);
    }

    public void Diy()
    {
        Died?.Invoke();
        AnimationHandler.Animator.SetTrigger(AnimatorData.Params.Diy);
    }

    protected virtual void OnInitialized() { }

    protected virtual void OnEnabled(){}

    protected virtual void OnDisabled(){}

    protected abstract WarriarPresenter GetPresenter();
    protected abstract MovementController GetMovementController(CharacterController characterController);
    protected abstract AbstractInput GetMoveInput(MovementController movementController);

    private void OnDead()
    {
        gameObject.SetActive(false);
    }
}