using System;

namespace Core.Data.CachedObjects.Collections
{
  public struct ItemStructList<T>
  {
    public T[] List;
    public int Count;

    public ItemStructList(int capacity)
    {
      List = new T[capacity];
      Count = 0;
    }

    public void EnsureIndex(int index)
    {
      int delta = index + 1 - Count;
      if (delta <= 0)
        return;
      Add(delta);
    }

    public bool IsValidIndex(int index)
    {
      if (index >= 0)
        return index < Count;
      return false;
    }

    public int IndexOf(T value)
    {
      int num = -1;
      for (int index = 0; index < Count; ++index)
      {
        if (List[index].Equals(value))
        {
          num = index;
          break;
        }
      }
      return num;
    }

    public bool Contains(T value)
    {
      return IndexOf(value) != -1;
    }

    public void Add(T item)
    {
      List[Add(1, false)] = item;
      Count = Count + 1;
    }

    public void Add(ref T item)
    {
      List[Add(1, false)] = item;
      Count = Count + 1;
    }

    public int Add()
    {
      return Add(1, true);
    }

    public int Add(int delta)
    {
      return Add(delta, true);
    }

    private int Add(int delta, bool incrCount)
    {
      if (List != null)
      {
        if (Count + delta > List.Length)
        {
          T[] objArray = new T[Math.Max(List.Length * 2, Count + delta)];
          List.CopyTo(objArray, 0);
          List = objArray;
        }
      }
      else
        List = new T[Math.Max(delta, 2)];
      int num = Count;
      if (!incrCount)
        return num;
      Count = Count + delta;
      return num;
    }

    public void Sort()
    {
      if (List == null)
        return;
      Array.Sort<T>(List, 0, Count);
    }

    public void AppendTo(ref ItemStructList<T> destinationList)
    {
      for (int index = 0; index < Count; ++index)
        destinationList.Add(ref List[index]);
    }

    public T[] ToArray()
    {
      T[] objArray = new T[Count];
      Array.Copy(List, 0, objArray, 0, Count);
      return objArray;
    }

    public void Clear()
    {
      Array.Clear(List, 0, Count);
      Count = 0;
    }

    public void Remove(T value)
    {
      int destinationIndex = IndexOf(value);
      if (destinationIndex == -1)
        return;
      Array.Copy(List, destinationIndex + 1, List, destinationIndex, Count - destinationIndex - 1);
      Array.Clear(List, Count - 1, 1);
      Count = Count - 1;
    }
  }
}
