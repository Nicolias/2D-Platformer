using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnteryPoint : MonoBehaviour
{
    [SerializeField] private List<EnemyDetector> _enemies;
    [SerializeField] private Player _player;

    [SerializeField] private TMP_Text _moneyCollectedText;
    [SerializeField] private Button _restartGameButton;

    private Wallet _wallet = new Wallet();

    private void OnEnable()
    {
        Time.timeScale = 1;

        _enemies.ForEach(enemy => enemy.PlayerDetected += GameOver);
        _player.MoneyCollector.Collected += OnWalletChanged;
    }
    
    private void OnDisable()
    {
        _enemies.ForEach(enemy => enemy.PlayerDetected -= GameOver);
        _player.MoneyCollector.Collected -= OnWalletChanged;
    }

    private void OnWalletChanged()
    {
        _wallet.AddCoin();
        _moneyCollectedText.text = _wallet.Score.ToString();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        _player.Movement.enabled = false;

        _restartGameButton.gameObject.SetActive(true);
        _restartGameButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        _restartGameButton.onClick.RemoveListener(RestartGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}