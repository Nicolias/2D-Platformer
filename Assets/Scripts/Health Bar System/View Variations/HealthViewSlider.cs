using UnityEngine;
using UnityEngine.UI;

namespace HealthView
{
    [RequireComponent(typeof(Slider))]
    public class HealthViewSlider : AbstractHealthView
    {
        private Transform _cameraTransform;
        private Transform _transform;
        protected Slider HealthBarSlider;

        private void Update()
        {
            if (_cameraTransform != null)
                _transform.rotation = _cameraTransform.rotation;
        }

        protected sealed override void OnInitialized()
        {
            _transform = transform;
            _cameraTransform = Camera.main.transform;
            HealthBarSlider = GetComponent<Slider>();

            HealthBarSlider.minValue = 0;
            HealthBarSlider.value = MaxHealth;
            HealthBarSlider.maxValue = MaxHealth;
        }

        protected override void OnHealthChanged(float currentHealth)
        {
            HealthBarSlider.value = currentHealth;
        }
    }
}