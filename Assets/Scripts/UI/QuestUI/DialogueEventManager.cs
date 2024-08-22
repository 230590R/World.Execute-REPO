using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventManager : MonoBehaviour {

  [SerializeField] NPCController[] NPCs;
  [SerializeField] BossController Boss;

  private void OnEnable() {
    Debug.Log("asdasd");
    for (int i = 0; i < NPCs.Length; i++) {
      DialogueManager.onDialogueEnd += NPCs[i].OnDialogueExit;
    }
    DialogueManager.onDialogueEnd += Boss.StartCombat;
  }

  private void OnDisable() {
    for (int i = 0; i < NPCs.Length; i++) {
      DialogueManager.onDialogueEnd -= NPCs[i].OnDialogueExit;
    }
    DialogueManager.onDialogueEnd -= Boss.StartCombat;
  }
}
