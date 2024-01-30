using CharacterNamespace;
using EnemyNamespace;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WarriarsFactory : MonoBehaviour, IToggleable
{
    [SerializeField] private List<EnemyView> _enemiesView;

    private UpdateServise _updateServise;

    private List<IToggleable> _toggleables = new List<IToggleable>();

    [field: SerializeField] public CharacterView CharacterView { get; private set; }

    public void Initialize(UpdateServise updateServise)
    {
        if (updateServise == null) throw new ArgumentNullException();

        _updateServise = updateServise;

        CharacterInitialize();
        EnemiesInitialize();
    }

    public void Enable()
    {
        _toggleables.ForEach(toggleable => toggleable.Enable());
    }

    public void Disable()
    {
        _toggleables.ForEach(toggleable => toggleable.Disable());
    }

    private void CharacterInitialize()
    {
        CharacterView.Initialize(_updateServise);

        _toggleables.Add(CharacterView);
    }

    private void EnemiesInitialize()
    {
        foreach (EnemyView enemyView in _enemiesView)
        {
            enemyView.Initialize(_updateServise, CharacterView);

            _toggleables.Add(enemyView);
        }
    }
}