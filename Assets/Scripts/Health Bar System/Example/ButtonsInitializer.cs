using UnityEngine;
using UnityEngine.UI;

public class ButtonsInitializer : MonoBehaviour
{
    [SerializeField] private HealthBarExample _healthBarExample;

    [SerializeField] private Button _healButton;
    [SerializeField] private Button _damageButton;

    private void OnEnable()
    {
        _healButton.onClick.AddListener(_healthBarExample.Heal);
        _damageButton.onClick.AddListener(_healthBarExample.Damage);
    }

    private void OnDisable()
    {
        _healButton.onClick.RemoveListener(_healthBarExample.Heal);
        _damageButton.onClick.RemoveListener(_healthBarExample.Damage);
    }
}