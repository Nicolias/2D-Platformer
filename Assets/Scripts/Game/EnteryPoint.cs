using CharacterNamespace;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnteryPoint : MonoBehaviour
{
    [SerializeField] private UIInitializer _uIInitializer;
    [SerializeField] private WarriarsFactory _warriarsFactory;
    [SerializeField] private UpdateServiseView _updateServiseView;

    [SerializeField] private List<CoinView> _coins;
    [SerializeField] private MedkitSpawner _medkitSpawner;

    private Wallet _wallet = new Wallet();

    private void Awake()
    {
        _updateServiseView.Initialize();

        _warriarsFactory.Initialize(_updateServiseView.UpdateServise);
        CharacterView characterView = _warriarsFactory.CharacterView;

        _uIInitializer.Initialize(_wallet, characterView);
        _medkitSpawner.Initialize(characterView);
        _coins.ForEach(coin => coin.Initialize(_wallet));

    }

    private void OnEnable()
    {
        _uIInitializer.RestartButtonClicked += RestartGame;
        _warriarsFactory.Enable();
    }

    private void OnDisable()
    {
        _uIInitializer.RestartButtonClicked += RestartGame;
        _warriarsFactory.Disable();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
