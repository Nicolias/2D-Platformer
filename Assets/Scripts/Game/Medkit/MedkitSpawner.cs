using CharacterSystem;
using System.Collections.Generic;
using UnityEngine;

public class MedkitSpawner : MonoBehaviour
{
    [SerializeField] private MedkitView _medkitPrefab;
    [SerializeField] private List<Transform> _spawnPositons;

    private List<MedkitView> _medkitViews = new List<MedkitView>();

    private MedkitView _currentMedkit;

    public void Initialize(Character character)
    {
        for (int i = 0; i < _spawnPositons.Count; i++)
        {
            MedkitView newMedkit = Instantiate(_medkitPrefab, _spawnPositons[i]);
            newMedkit.Initialize(character.AttackAndHealth.Health);
            newMedkit.gameObject.SetActive(false);
            _medkitViews.Add(newMedkit);
        }

        ShowNextMedkit();
    }

    private void OnDestroy()
    {
        HideCurrentMedkit();
    }

    private void OnPickedUp()
    {
        HideCurrentMedkit();
        ShowNextMedkit();
    }

    private void ShowNextMedkit()
    {
        int currentMedkitIndex = _medkitViews.IndexOf(_currentMedkit);
        int nextMedkitIndex = currentMedkitIndex;

        while (currentMedkitIndex == nextMedkitIndex)
            nextMedkitIndex = Random.Range(0, _medkitViews.Count);

        _currentMedkit = _medkitViews[nextMedkitIndex];
        _currentMedkit.gameObject.SetActive(true);

        _currentMedkit.PickedUp += OnPickedUp;
    }

    private void HideCurrentMedkit()
    {
        _currentMedkit.PickedUp -= OnPickedUp;
        _currentMedkit.gameObject.SetActive(false);
    }
}