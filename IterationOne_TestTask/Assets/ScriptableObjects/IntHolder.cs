using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/IntHolder", order = 1)]
public class IntHolder : ScriptableObject
{
    public UnityEvent<int> OnValueChanged;

    [SerializeField]
    private int _value;

    private int _min = 0;
    private int _max = Int32.MaxValue;

    public int value
    {
        get
        {
            return _value;
        }
        set
        {
            if(_value == value)
                return;

            value = Mathf.Clamp(value, _min, _max);
            
            _value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}
