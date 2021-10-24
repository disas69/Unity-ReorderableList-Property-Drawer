using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int[] Numbers;
    public List<string> Texts;
    public List<TestObject> Objects;
}

[Serializable]
public class TestObject
{
    public int ID;
    public GameObject GameObject;
    public Vector3 Position;
}