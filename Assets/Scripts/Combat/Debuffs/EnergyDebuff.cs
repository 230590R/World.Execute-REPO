using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Debuffs/Energy")]
public class EnergyDebuff : IDebuff
{
    public override void InflictDebuff(PlayerStats playerStats)
    {
        playerStats.regen -= 1.0f;
    }


}
