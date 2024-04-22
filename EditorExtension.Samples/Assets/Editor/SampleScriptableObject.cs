using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SampleScriptableObject : ScriptableObject
{
    [SerializeField] private string text;

    public string Text
    {
        get => text;
        set => text = value;
    }
}
