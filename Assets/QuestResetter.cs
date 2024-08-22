using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestResetter : MonoBehaviour {

  [SerializeField] QuestObj[] quests; 
  private void Start() {
    for (int i = 0; i < quests.Length; i++) {
      quests[i].isComplete = false;
    }
  }

}
