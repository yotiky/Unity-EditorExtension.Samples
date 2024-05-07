using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class EditorInitializer
{
    static EditorInitializer()
    {
        Debug.Log("InitializeOnLoad");
    }

    [InitializeOnLoadMethod]
    private static void Initalize()
    {
        Debug.Log("InitalizeOnLoadMethod");
    }
}
