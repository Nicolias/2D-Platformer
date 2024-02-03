using TMPro;
using UnityEngine;

namespace HealthView
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class HealthViewText : AbstractHealthView
    {
        private TMP_Text _healthBarText;

        private void Awake()
        {
            _healthBarText = GetComponent<TMP_Text>();    
        }

        protected override void OnHealthChanged(float currentHealth)
        {
            _healthBarText.text = $"{currentHealth}/{MaxHealth}";
        }
    }
}