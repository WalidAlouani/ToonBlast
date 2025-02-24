using System;
using System.Collections.Generic;
using UnityEngine;

public class PropertyEvent<T>
{
    private readonly List<Act<T>> Callbacks = new List<Act<T>>();

    public void Subscribe(Action<T> onChanged, MonoBehaviour mb = null, int priority = 0)
    {
        var newAct = new Act<T>
        {
            Mb = mb,
            HasMb = mb != null,
            Changed = onChanged,
            Priority = priority
        };

        if (Callbacks.Count == 0)
        {
            Callbacks.Add(newAct);
            return;
        }

        int insertIndex = 0;
        for (int i = 0; i < Callbacks.Count; i++)
        {
            if (newAct.Priority > Callbacks[i].Priority)
            {
                insertIndex = i;
                break;
            }
            insertIndex = i + 1;
        }

        Callbacks.Insert(insertIndex, newAct);
    }

    public void Unsubscribe(Action<T> onChanged)
    {
        Callbacks.RemoveAll(el => el.Changed == onChanged);
    }

    public void Unsubscribe(MonoBehaviour mb)
    {
        Callbacks.RemoveAll(el => el.Mb == mb);
    }

    public void Publish(T value)
    {
        Callbacks.RemoveAll(el =>
        {
            try
            {
                // Here comes the magic: if monoBehaviour has been already removed we'll have null here
                if (el.HasMb && el.Mb == null)
                    return true;

                if (!el.HasMb || el.Mb.gameObject.activeInHierarchy && el.Mb.enabled)
                    el.Changed?.Invoke(value);

                return false;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return false;
            }
        });
    }

    public void Clear()
    {
        Callbacks.Clear();
    }

    private class Act<TT>
    {
        public Action<TT> Changed;
        public MonoBehaviour Mb;
        public bool HasMb;
        public int Priority;
    }
}