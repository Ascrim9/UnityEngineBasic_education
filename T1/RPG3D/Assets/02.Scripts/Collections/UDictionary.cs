using System;
using System.Collections.Generic;
using UnityEngine;

namespace _02.Scripts.Collections
{

    
    [Serializable]
    public class UDictionary<Tkey, Tvalue> : ISerializationCallbackReceiver
    {
        public Tvalue this[Tkey key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }


        public UDictionary(IEnumerable<UKeyValuePair<Tkey, Tvalue>> copy)
        {
            Initialize(copy);
        }

        public UDictionary()
        {
            
        }
            
        [SerializeField] private List<UKeyValuePair<Tkey, Tvalue>> _list;
        private Dictionary<Tkey, Tvalue> _dictionary;

        public void Initialize(IEnumerable<UKeyValuePair<Tkey, Tvalue>> copy)
        {
            _list = new();
        }
        
        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            if (_list is null)
                return;

            _dictionary = new();

            foreach (var VARIABLE in _list)
            {
                _dictionary.Add(VARIABLE.Key, VARIABLE.Value);
            }

        }

        public void Add(Tkey key, Tvalue value) => _dictionary.Add(key, value);

    }
}