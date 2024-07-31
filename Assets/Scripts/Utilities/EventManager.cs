using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void EventReceiver(params object[] parameters);

    private Dictionary<string, EventReceiver> eventDictionary = new Dictionary<string, EventReceiver>();
    EventReceiver thisEvent;

    public void StartListening(string eventName, EventReceiver listener)
    {
        if (this.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            this.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            this.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StopListening(string eventName, EventReceiver listener)
    {
        if (this == null) return;
        if (this.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            this.eventDictionary[eventName] = thisEvent;
        }
    }

    public void TriggerEvent(string eventName, params object[] parameters)
    {
        if (this.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(parameters);
        }
    }

    internal void StartListening(string v, object removeBlockTf)
    {
        throw new NotImplementedException();
    }
}
