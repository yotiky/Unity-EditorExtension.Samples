using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Fruit
{
    Apple,
    Orange,
    GoldenPeach,
}
public class SerializedObjectSampleComponent : MonoBehaviour
{
    [Serializable]
    public class SubClass
    {
        public double doubleValue;
    }
    
    [SerializeField] private bool boolValue;
    [SerializeField] private int intValue;
    [SerializeField] private float floatValue;
    [SerializeField] private string stringValue;
    [SerializeField] private GameObject objectReferenceValue;
    [SerializeField] private List<int> list = new List<int>();
    [SerializeField] private Fruit enumValue;
    [SerializeField] private SubClass subClassReference;
}
