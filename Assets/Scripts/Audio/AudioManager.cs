using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void ToggleAudioSetting(GameObject go)
    {
        bool active = go.activeSelf;
        go.SetActive(!active);
    }
}
