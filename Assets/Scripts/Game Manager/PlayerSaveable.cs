using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Saveable/PlayerSaveable")]
public class PlayerSaveable : ScriptableObject {

  List<IDebuff> debuffs = new List<IDebuff>();
  float health;

  public void Save(PlayerController player) {
    var healthController = player.GetComponent<HealthController>();
    health = healthController.health;
    //var pController = player.GetComponent<PlayerController>();


    var pStatController = player.GetComponent<PlayerStats>();
    debuffs.Clear();
    foreach(var pDebuff in pStatController.debuffs) {
      debuffs.Add(pDebuff);
    }
  }

  public void Load(PlayerController player) {
    var healthController = player.GetComponent<HealthController>();
    //var pController = player.GetComponent<PlayerController>();
    healthController.health = health;

    var pStatController = player.GetComponent<PlayerStats>();
    pStatController.debuffs.Clear();
    foreach (var pDebuff in debuffs) {
      pStatController.debuffs.Add(pDebuff);
    }
  }
}
