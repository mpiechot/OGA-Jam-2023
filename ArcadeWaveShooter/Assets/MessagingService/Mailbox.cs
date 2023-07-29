using System;
using System.Collections.Generic;

public class Mailbox
{
    private static Mailbox instance { get { if(_instance == null) _instance = new Mailbox(); return _instance; } }
    private static Mailbox _instance;

    private Dictionary<Type, List<Delegate>> subscriberMap = new Dictionary<Type, List<Delegate>>();

    private void AddSubscriberWorker<T>(Action<T> subscriber) where T : notnull
    {
        Type type = typeof(T);
        if (!subscriberMap.ContainsKey(type))
        {
            subscriberMap.Add(type, new List<Delegate>());
        }

        subscriberMap[type].Add(subscriber);
    }

    public void RemoveSubscriberWorker<T>(Action<T> subscriber) where T : notnull
    {
        if (subscriberMap.ContainsKey(typeof(T)))
        {
            subscriberMap[typeof(T)].Remove(subscriber);
        }
    }

    private void InvokeSubscribersWorker<T>(T mail) where T : notnull
    {
        Type type = typeof(T);
        if (subscriberMap.ContainsKey(type))
        {
            foreach (var listener in subscriberMap[type])
            {
                ((Action<T>)listener).Invoke(mail);
            }
        }
    }

    public static void AddSubscriber<T>(Action<T> subscriber) where T : notnull
    {
        instance.AddSubscriberWorker(subscriber);
    }

    public static void RemoveSubscriber<T>(Action<T> subscriber) where T : notnull
    {
        instance.RemoveSubscriberWorker(subscriber);
    }

    public static void InvokeSubscribers<T>(T mail) where T : notnull
    {
        instance.InvokeSubscribersWorker(mail);
    }

    public static void Cleanup()
    {
        instance.subscriberMap.Clear();
        _instance = null;
    }
}