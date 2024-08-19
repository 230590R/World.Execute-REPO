using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialougeSO : ScriptableObject
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

    public bool dialougeDone = false;
}
