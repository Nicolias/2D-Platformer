using CharacterNamespace;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInitializer : MonoBehaviour
{
    [SerializeField] private EnteryPoint _enteryPoint;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Button _restartGameButton;

    private Wallet _wallet;
    private IDieable _character;

    public event Action RestartButtonClicked;

    public void Initialize(Wallet wallet, IDieable character)
    {
        _wallet = wallet;
        _character = character;

        wallet.Changed += OnWalletChanged;
        character.Died += OnCharacterDied;

        _restartGameButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        _wallet.Changed -= OnWalletChanged;
        _character.Died -= OnCharacterDied;

        _restartGameButton.onClick.RemoveListener(RestartGame);
    }

    private void OnWalletChanged(int currentScore)
    {
        _scoreText.text = currentScore.ToString();
    }

    private void OnCharacterDied()
    {
        _restartGameButton.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        RestartButtonClicked?.Invoke();
    }
}
