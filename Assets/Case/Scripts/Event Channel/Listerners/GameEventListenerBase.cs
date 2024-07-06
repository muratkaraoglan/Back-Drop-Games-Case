using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventListenerBase<T> : MonoBehaviour
{
    public GameEventBaseSO<T> Event;
    public UnityEvent<T> Response;

    private void OnEnable()
    {
        Event.OnEventRaised += OnEventRaised;
    }
    private void OnDisable()
    {
        Event.OnEventRaised -= OnEventRaised;
    }
    private void OnEventRaised(T item)
    {
        Response?.Invoke(item);
    }
}
