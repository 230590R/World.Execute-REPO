using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDebuff : ScriptableObject {
  public abstract void InflictDebuff(PlayerStats playerStats);
}
