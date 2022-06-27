using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    public static GeneratorManager instance;
    public IntHolder money;

    private void Awake()
    {
        instance = this;

        if (money)
            money.value = 0;
    }

    public void DeltaMoney(int delta)
    {
        if(!money)
            return;

        money.value += delta;
    }
    
}
