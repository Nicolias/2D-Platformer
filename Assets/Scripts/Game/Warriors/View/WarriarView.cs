using HealthView;
using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AnimationEventsHandler))]
public abstract class WarriarView : MonoBehaviour, IDamagable, IToggleable
{
    [SerializeField] private HealthViewSlider _healthView;

    private Movement _movement;
    private WarriarPresenter _warriarPresenter;
    private DetecterHandler _detecterHandler;
    private Transform _transform;

    protected AbstractInput AbstractInput { get; private set; }
    protected AnimationEventsHandler AnimationEventsHandler { get; private set; }
    protected WarriarAnimationHandler AnimationHandler { get; private set; }
    protected UpdateServise UpdateServise { get; private set; }

    protected abstract AbstractDetector Detector { get; }

    public Vector2 Position => _transform.position;

    public event Action Died;

    void IDamagable.Damage(int value)
    {
        _warriarPresenter.Damage(value);
    }

    public void Initialize(UpdateServise updateServise)
    {
        if (updateServise == null) throw new ArgumentNullException();

        gameObject.SetActive(true);
        _transform = transform;

        UpdateServise = updateServise;
        AnimationEventsHandler = GetComponent<AnimationEventsHandler>();
        _movement = GetMovementController(GetComponent<CharacterController>());
        AnimationHandler = new WarriarAnimationHandler(_movement, GetComponent<Animator>(), transform, updateServise);
        AbstractInput = GetMoveInput(_movement);
        _warriarPresenter = GetPresenter();
        _detecterHandler = new DetecterHandler(_warriarPresenter, Detector);

        OnInitialized();
    }

    public void Enable()
    {
        AnimationEventsHandler.DeadShowed += OnDead;

        UpdateServise.AddToFixedUpdate(_movement);

        _detecterHandler.Enable();
        _warriarPresenter.Enable();
        _healthView.Initialize(_warriarPresenter.HealthChangeable);
        _healthView.Enable();

        OnEnabled();
    }

    public void Disable()
    {
        AnimationEventsHandler.DeadShowed -= OnDead;

        UpdateServise.RemoveFromFixedUpdate(_movement);

        _detecterHandler.Disable();
        _warriarPresenter.Disable();
        _healthView.Disable();

        OnDisabled();
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
    protected abstract Movement GetMovementController(CharacterController characterController);
    protected abstract AbstractInput GetMoveInput(Movement movementController);

    private void OnDead()
    {
        Disable();
        gameObject.SetActive(false);
    }
}