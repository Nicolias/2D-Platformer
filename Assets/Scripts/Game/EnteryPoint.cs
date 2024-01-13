using Character;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnteryPoint : MonoBehaviour
{
    [SerializeField] private List<Enemy.Enemy> _enemies;
    [SerializeField] private List<CoinView> _coins;
    [SerializeField] private CharacterView _characterView;

    [SerializeField] private UIInitializer _uIInitializer;
    [SerializeField] private MedkitSpawner _medkitSpawner;
    [SerializeField] private UpdateServiseView _updateServiseView;

    private Wallet _wallet = new Wallet();

    private void Awake()
    {
        _uIInitializer.Initialize(_wallet, _characterView);
        _updateServiseView.Initialize();

        _characterView.Initialize(_updateServiseView.UpdateServise);
        _medkitSpawner.Initialize(_characterView);

        _enemies.ForEach(enemy => enemy.Initialize(_characterView, _updateServiseView.UpdateServise));
        _coins.ForEach(coin => coin.Initialize(_wallet));
    }

    private void OnEnable()
    {
        _uIInitializer.RestartButtonClicked += RestartGame;
    }

    private void OnDisable()
    {
        _uIInitializer.RestartButtonClicked += RestartGame;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}