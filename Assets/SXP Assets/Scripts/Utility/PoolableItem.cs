using System.Collections.Generic;
using UnityEngine;

public class PoolableItem<T> : MonoBehaviour
{
    readonly Stack<T> stack;
	System.Func<T> createFunc;

    public PoolableItem(System.Func<T> create) : this(create, 0) {
		createFunc = create;
	}
    public PoolableItem(System.Func<T> createFunction, int size)
    {
        stack = new Stack<T>(size);
        Size = size;
    }

    public T Take()
    {
        return stack.Count > 0 ? stack.Pop() : createFunc();
    }

    public void Return(T item)
    {
        stack.Push(item);
    }

    int size;
    public int Size
    { 
        get { return size; }
        set
        {
            var left = value - size;
            if (left > 0)
                for (int i = 0; i < left; i++)
                    stack.Push(createFunc());

            size = value;
        }
    }

    public int Available
    {
        get { return stack.Count; }
    }
}