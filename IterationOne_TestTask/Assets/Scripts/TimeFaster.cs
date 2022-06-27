using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFaster : MonoBehaviour
{
    public IntHolder money;

    public void MakeFaster()
    {
        if(!money)
            return;
        if(money.value <= 0 )
            return;

        money.value -= 1;
        Time.timeScale += 0.1f;
    }
}
