using System;

namespace _02.Scripts.Collections
{
    [Serializable]
    public struct UKeyValuePair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public UKeyValuePair(TKey statType, TValue stat)
        {
            Key = statType;
            Value = stat;
        }
    }
}
