using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HealthView
{
    [RequireComponent (typeof(Slider))]
    public sealed class HealtViewSmoothSlider : HealthViewSlider
    {
        [SerializeField] private float _fillSpeed;

        private Coroutine _fillHealthBar;

        protected override void OnHealthChanged(float currentHealth)
        {
            if (_fillHealthBar != null)
                StopCoroutine(_fillHealthBar);

            _fillHealthBar = StartCoroutine(FillHealthBar(currentHealth));
        }

        private IEnumerator FillHealthBar(float currentHealth)
        {
            float currentHealthNormolized = currentHealth / MaxHealth; 

            while (HealthBarSlider.normalizedValue != currentHealthNormolized)
            {
                HealthBarSlider.normalizedValue = Mathf.MoveTowards
                (
                    HealthBarSlider.normalizedValue,
                    currentHealthNormolized,
                    _fillSpeed * Time.deltaTime
                );

                yield return null;
            }
        }
    }
}