using System.Collections.Generic;
using UnityEngine;

public class UpdateServise : MonoBehaviour
{
    private List<IUpdateable> _updateables = new List<IUpdateable>();
    private List<IUpdateable> _fixedUpdateables = new List<IUpdateable>();

    private void Update()
    {
        int updateablesLength = _updateables.Count;

        for (int i = 0; i < updateablesLength; i++)
            if (i < _updateables.Count)
                _updateables[i].Update(Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        int fixedUpdateablesLength = _fixedUpdateables.Count;

        for (int i = 0; i < fixedUpdateablesLength; i++)
            if (i < _fixedUpdateables.Count)
                _fixedUpdateables[i].Update(Time.fixedDeltaTime);
    }

    public void AddToUpdate(IUpdateable updateable)
    {
        _updateables.Add(updateable);
    }

    public void RemoveFromUpdate(IUpdateable updateable)
    {
        _updateables.Remove(updateable);
    }

    public void AddToFixedUpdate(IUpdateable updateable)
    {
        _fixedUpdateables.Add(updateable);
    }

    public void RemoveFromFixedUpdate(IUpdateable updateable)
    {
        _fixedUpdateables.Remove(updateable);
    }
}
