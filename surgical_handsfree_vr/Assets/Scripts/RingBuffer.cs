using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBuffer<T>
{
    private T[] _arr;
    private int _at;

    public RingBuffer(T[] arr)
    {
        _arr = arr;
    }

    public T Get()
    {
        return _arr[_at];
    }

    public int GetAt()
    {
        return _at;
    }

    public void SetAt(int at)
    {
        _at = at % _arr.Length;
    }

    public void IterateUp()
    {
        _at = (_at + 1) % _arr.Length;
    }

    public void IterateDown()
    {
        _at = (_at + _arr.Length - 1) % _arr.Length;
    }
}
