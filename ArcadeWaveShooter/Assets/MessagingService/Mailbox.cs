using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
public class Mailbox
{
    private static Mailbox instance { get { if(_instance == null) _instance = new Mailbox(); return _instance; } }
    private static Mailbox _instance;

    private Dictionary<Type, List<Action<object>>> subscriberMap = new Dictionary<Type, List<Action<object>>>();

    private void AddSubscriberWorker<T>(Action<object> subscriber) where T : notnull
    {
        Type type = typeof(T);
        if (!subscriberMap.ContainsKey(type))
        {
            subscriberMap.Add(type, new List<Action<object>>());
        }

        subscriberMap[type].Add(subscriber);
    }

    public void RemoveSubscriberWorker<T>(Action<object> subscriber) where T : notnull
    {
        if (subscriberMap.ContainsKey(typeof(T)))
        {
            subscriberMap[typeof(T)].Remove(subscriber);
        }
    }

    private void InvokeSubscribersWorker<T>(object mail) where T : notnull
    {
        Type type = typeof(T);
        if (subscriberMap.ContainsKey(type))
        {
            foreach (var listener in subscriberMap[type])
            {
                listener.Invoke(mail);
            }
        }
    }

    public static void AddSubscriber<T>(Action<object> subscriber) where T : notnull
    {
        instance.AddSubscriberWorker<T>(subscriber);
    }

    public static void RemoveSubscriber<T>(Action<object> subscriber) where T : notnull
    {
        instance.RemoveSubscriberWorker<T>(subscriber);
    }

    public static void InvokeSubscribers<T>(object mail) where T : notnull
    {
        instance.InvokeSubscribersWorker<T>(mail);
    }

    public static void Cleanup()
    {
        instance.subscriberMap.Clear();
        _instance = null;
    }
}