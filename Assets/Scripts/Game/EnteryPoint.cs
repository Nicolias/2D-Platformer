using CharacterSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnteryPoint : MonoBehaviour
{
    [SerializeField] private List<Enemy.Enemy> _enemies;
    [SerializeField] private Character _character;
    [SerializeField] private UpdateServise _updateServise;

    [SerializeField] private TMP_Text _moneyCollectedText;
    [SerializeField] private Button _restartGameButton;

    private Wallet _wallet = new Wallet();

    private void Awake()
    {
        _character.Initialize(_updateServise);
        _character.MoneyCollector.Collected += OnWalletChanged;
        _character.Dead += GameOver;

        _enemies.ForEach(enemy => enemy.Initialize(_character, _updateServise));

        Time.timeScale = 1;
    }
    
    private void OnDestroy()
    {
        _character.MoneyCollector.Collected -= OnWalletChanged;
        _character.Dead -= GameOver;
    }

    private void OnWalletChanged()
    {
        _wallet.AddCoin();
        _moneyCollectedText.text = _wallet.Score.ToString();
    }

    private void GameOver()
    {
        Time.timeScale = 0;

        _restartGameButton.gameObject.SetActive(true);
        _restartGameButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        _restartGameButton.onClick.RemoveListener(RestartGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}