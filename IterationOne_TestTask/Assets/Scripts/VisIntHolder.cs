using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VisIntHolder : MonoBehaviour
{
    public IntHolder intHolder;
    public Text text;
    public string prefix;
    public string suffix;
    private void Start()
    {
        if (!intHolder)
            return;
        
        intHolder.OnValueChanged.AddListener(HandleValueChange);
        HandleValueChange(intHolder.value);
    }

    public void HandleValueChange(int value)
    {
        if(!text)
            return;


        text.text = $"{prefix}{value}{suffix}";
        //text.DOFade(0, 0.1f).OnComplete(() => { text.DOFade(1, 02f); });
        text.transform.localScale = Vector3.one;
        text.transform.DOPunchScale(-Vector3.one * 0.2f, 0.3f);
    }
}
