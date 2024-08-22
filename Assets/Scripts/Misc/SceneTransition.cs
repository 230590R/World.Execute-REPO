using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {
  public static SceneTransition Instance;
  public Animator transition;

  public static event System.Action OnTransition;

  private void Awake() {
    if (Instance == null) Instance = this;
    else Destroy(this.gameObject);
  }

  public void SwitchScene(string sceneName) {
    StartCoroutine(SwitchSceneCoroutine(sceneName));
  }

  IEnumerator SwitchSceneCoroutine(string sceneName) {
    transition.SetTrigger("Start");



    yield return new WaitForSeconds(1);

    SceneManager.LoadScene(sceneName);

    yield return null;

    transition.SetTrigger("End");
    OnTransition?.Invoke();
  }
}
