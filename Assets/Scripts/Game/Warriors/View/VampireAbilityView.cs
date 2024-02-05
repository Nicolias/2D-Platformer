using System;
using UnityEngine;

namespace CharacterNamespace
{
    [RequireComponent(typeof(LineRenderer))]
    public class VampireAbilityView : MonoBehaviour
    {
        [SerializeField] private Detector _detector;

        [SerializeField] private int _drainDuration;
        [SerializeField] private int _drainValuePerSeconds;

        private UpdateServise _updateServise;
        private VampireAbilityPresenter _presenter;

        private LineRenderer _lineRenderer;
        private IDamagable _target;

        private Transform _transform;

        public Vector2 Position => _transform.position;

        private void Awake()
        {
            _transform = transform;
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.V))
                _presenter.Enable();

            _lineRenderer.SetPosition(0, Position);
            _lineRenderer.SetPosition(1, _target == null ? Position : _target.Position);
        }

        public void Initialize(UpdateServise updateServise, Health characterHealth)
        {
            if (updateServise == null)
                throw new ArgumentNullException();

            if (characterHealth == null)
                throw new ArgumentNullException();

            _updateServise = updateServise;

            _presenter = new VampireAbilityPresenter
                (
                    this,
                    new VampireAbilityModel
                    (
                        characterHealth,
                        _drainDuration,
                        _drainValuePerSeconds
                    )
                );
        }

        public void Enable()
        {
            _detector.Detected += OnDetected;
            _detector.Lost += OnLost;

            _updateServise.AddToUpdate(_presenter);
        }

        public void Disable()
        {
            _detector.Detected -= OnDetected;
            _detector.Lost -= OnLost;

            _updateServise.RemoveFromUpdate(_presenter);
            _presenter.Disable();
        }

        public void DrawLine(IDamagable target)
        {
            _target = target;
        }

        private void OnDetected(IDamagable damagable)
        {
            _presenter.Add(damagable);
        }

        private void OnLost(IDamagable damagable)
        {
            _presenter.Remove(damagable);
        }
    }
}
