using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnteryPoint : MonoBehaviour
{
    [SerializeField] private List<EnemyDetector> _enemies;
    [SerializeField] private MoneyCollector _moneyCollector;

    [SerializeField] private TMP_Text _moneyCollectedText;

    private Wallet _wallet = new Wallet();

    private void OnEnable()
    {
        _enemies.ForEach(enemy => enemy.PlayerDetected += GameOver);
        _moneyCollector.Collected += OnWalletChanged;
    }
    
    private void OnDisable()
    {
        _enemies.ForEach(enemy => enemy.PlayerDetected -= GameOver);
        _moneyCollector.Collected -= OnWalletChanged;
    }

    private void OnWalletChanged()
    {
        _wallet.AddCoin();
        _moneyCollectedText.text = _wallet.Score.ToString();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
    }
}