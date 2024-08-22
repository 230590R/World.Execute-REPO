using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherV2 : MonoBehaviour
{
    public string SceneName;
    public string PlayerTag = "Player";
    public KeyCode InputKey = KeyCode.E;
    public string DoorID;
    public bool isLevelFinisher = false;

    [SerializeField] private bool isPlayerInTrigger = false;

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(InputKey))
        {


            if (!string.IsNullOrEmpty(SceneName))
            {
                Debug.Log("Enter");
                PlayerPrefs.SetString("LastUsedDoorID", DoorID);
                PlayerPrefs.Save();

                if (isLevelFinisher)
                {
                    GetComponent<LevelFinisher>().FinishLevel(DoorID);
                }

                //SceneManager.LoadScene(SceneName);
                SceneTransition.Instance.SwitchScene(SceneName);


            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(PlayerTag))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag))
        {
            isPlayerInTrigger = false;
        }
    }
}
