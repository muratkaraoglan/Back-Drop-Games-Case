using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventBaseSO<T> : ScriptableObject
{
    public event System.Action<T> OnEventRaised;

    public void Raise(T item)
    {
        OnEventRaised?.Invoke(item);
    }
}
