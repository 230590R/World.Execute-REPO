using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Debuffs/Example")]
public class WaterDebuff : IDebuff
{
    public override void InflictDebuff(PlayerStats playerStats)
    {
        playerStats.movementSpeed *= 0.9f;
    }


}
