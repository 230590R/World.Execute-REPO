using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class Stats : ScriptableObject {
  public float movementSpeed = 5;

    public float runMultiplier = 1.5f;
    public float maxHp = 100;
    public float detectionRange = 5f;
}
