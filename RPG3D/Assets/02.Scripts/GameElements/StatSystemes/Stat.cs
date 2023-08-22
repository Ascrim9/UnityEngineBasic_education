using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



/*
 * https://wlsdn629.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0%EC%97%90%EC%84%9C-ChatGPT-%EC%9D%B4%EC%9A%A9%ED%95%98%EA%B8%B0-2?category=1051398
 * 
 * Test Decoparent Ai NOXX System
 * 
 */

namespace RPG.GameElements.Stats
{

    public enum StatType
    {
        HPMax,
        MPMax,
        STR,
        DEF
    }
    public class Stat
    {
        public StatType type;
        public float value
        {
            get => _value;
            private set
            {
                _value = value;
                onValueChanged?.Invoke(value);
            }
        }
        private float _value;
        public event Action<float> onValueChanged;
        public float valueModified
        {
            get => _valueModified;
            private set
            {
                _valueModified = value;
                onValueChanged?.Invoke(value);
            }
        }


        private float _valueModified;
        public event Action<float> onValueModifiedChanged;

        public IEnumerator<StatModifier> modifiers => _modifiers.GetEnumerator();
        private List<StatModifier> _modifiers;


        public void AddModifer(StatModifier modifire)
        {
            _modifiers.Add(modifire);
            valueModified = CalcValueModified();
        }

        public void RemoveModifier(StatModifier modifire)
        {
            _modifiers.Remove(modifire);
            valueModified = CalcValueModified();

        }

        private float CalcValueModified()
        {
            float sumAddFalt = 0.0f;
            float sumAddPercent = 0.0f;
            float sumMulPercent = 0.0f;

            foreach(var modifire in _modifiers)
            {
                switch (modifire.type)
                {
                    case
                }
            }







            return (_value + sumAddFalt) + (_value * sumAddPercent) + (_valueModified + sumMulPercent);
        }





    }
}