using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionEventHandler : MonoBehaviour
{
  [SerializeField] EquipController equipController;

  private void OnEnable() {
    SceneTransition.OnTransition += equipController.FindPlayerReference;
  }

  private void OnDisable() {

    SceneTransition.OnTransition -= equipController.FindPlayerReference;
  }
}
