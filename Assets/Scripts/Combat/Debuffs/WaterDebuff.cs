using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Debuffs/Water")]
public class WaterDebuff : IDebuff
{
    public override void InflictDebuff(PlayerStats playerStats)
    {
        playerStats.movementSpeed *= 0.9f;
        playerStats.attackCooldown += 0.25f;
    }


}
