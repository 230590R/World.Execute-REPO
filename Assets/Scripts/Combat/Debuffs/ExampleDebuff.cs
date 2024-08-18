using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Debuffs/Example")]
public class ExampleDebuff : IDebuff {
  public override void InflictDebuff(PlayerStats playerStats) {
    playerStats.movementSpeed *= 0.5f;
    playerStats.regen -= 1f;
  }


}
