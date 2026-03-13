using System;
using System.Collections;
using System.Collections.Generic;

namespace Ambertation.Graphics;

public class MeshList : IEnumerable, IDisposable
{
    private List<MeshBox> list = new List<MeshBox>();

    public MeshBox this[int index]
    {
        get => list[index];
        set
        {
            OnRemove(list[index]);
            list[index] = value;
            OnAdd(value);
        }
    }

    public int Count => list.Count;

    public MeshList()
    {
        list = new List<MeshBox>();
    }

    public void Add(MeshBox m)
    {
        OnAdd(m);
        list.Add(m);
    }

    public void Insert(int index, MeshBox m)
    {
        OnAdd(m);
        list.Insert(index, m);
    }

    public void AddRange(MeshBox[] m)
    {
        foreach (MeshBox m2 in m)
        {
            Add(m2);
        }
    }

    public void AddRange(MeshList m)
    {
        foreach (MeshBox item in (IEnumerable)m)
        {
            Add(item);
        }
    }

    protected virtual void OnRemove(MeshBox m)
    {
    }

    protected virtual void OnAdd(MeshBox m)
    {
    }

    public void Remove(MeshBox m)
    {
        OnRemove(m);
        list.Remove(m);
    }

    public void RemoveAt(int index)
    {
        try
        {
            MeshBox m = this[index];
            Remove(m);
        }
        catch
        {
        }
    }

    public void Clear()
    {
        Clear(dispose: false);
    }

    public void Clear(bool dispose)
    {
        if (dispose)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Dispose();
            }
        }
        list.Clear();
    }

    public bool Contains(MeshBox m)
    {
        return list.Contains(m);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return list.GetEnumerator();
    }

    public virtual void Dispose()
    {
        if (list != null)
        {
            Clear(dispose: true);
        }
    }
}
