using System;
using System.Collections.Generic;

public class UpdateServise : IUpdateable, IFixedUpdateable
{
    private List<IUpdateable> _updateables = new List<IUpdateable>();
    private List<IFixedUpdateable> _fixedUpdateables = new List<IFixedUpdateable>();

    void IUpdateable.Update(float timeBetweenFrame)
    {
        int updateablesLength = _updateables.Count;

        for (int i = 0; i < updateablesLength; i++)
            if (i < _updateables.Count)
                _updateables[i].Update(timeBetweenFrame);
    }

    void IFixedUpdateable.Update(float timeBetweenFrame)
    {
        int fixedUpdateablesLength = _fixedUpdateables.Count;

        for (int i = 0; i < fixedUpdateablesLength; i++)
            if (i < _fixedUpdateables.Count)
                _fixedUpdateables[i].Update(timeBetweenFrame);
    }

    public void AddToUpdate(IUpdateable updateable)
    {
        if (updateable == null)
            throw new ArgumentNullException();

        _updateables.Add(updateable);
    }

    public void RemoveFromUpdate(IUpdateable updateable)
    {
        if (updateable == null)
            throw new ArgumentNullException();

        _updateables.Remove(updateable);
    }

    public void AddToFixedUpdate(IFixedUpdateable updateable)
    {
        if (updateable == null)
            throw new ArgumentNullException();

        _fixedUpdateables.Add(updateable);
    }

    public void RemoveFromFixedUpdate(IFixedUpdateable updateable)
    {
        if (updateable == null)
            throw new ArgumentNullException();

        _fixedUpdateables.Remove(updateable);
    }
}