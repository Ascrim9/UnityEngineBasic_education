using System;

namespace _02.Scripts.Collections
{
    public interface INotifyCollectionChanged<T>
    {
        event Action<int, T> onItemChanged;
        event Action<int, T> onItemAdded;
        event Action<int, T> onItemRemoved;
        event Action onCollectionChanged;
    }
}