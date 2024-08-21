using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public Sprite image;
     
    public string name;
    public string description;

    public int cost;

    public float imageWidth;
    public float imageHeight;

    public bool stackable = false;
    public float maxStackCount = 10;
}