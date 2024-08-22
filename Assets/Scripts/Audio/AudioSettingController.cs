using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioSettingController : MonoBehaviour
{
    public enum VOLUME_TYPE
    {
        MASTER,
        SFX,
        BGM
    }

    [SerializeField] VOLUME_TYPE volumeType = VOLUME_TYPE.MASTER;

    [SerializeField] GameObject parentOfVolumeBars;
    List<GameObject> volumeBars = new List<GameObject>();

    [SerializeField] Sprite endingBarSprite;
    [SerializeField] Sprite startingBarSprite;

    [SerializeField] AudioMixer mixer;

    [SerializeField] TMPro.TMP_Text textMeshPro;

    static bool reset = true;

    int volumeBarsCount = 0;

    private void Awake()
    {
        RectTransform[] volumeBarsRect = parentOfVolumeBars.GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < volumeBarsRect.Length; i++)
        {
            volumeBars.Add(volumeBarsRect[i].gameObject);
        }
        volumeBars.RemoveAt(0);

        volumeBarsCount = volumeBars.Count - 1;
    }

    private void Start()
    {
        if (reset)
          PresetVolume();

          SetVolume();
          LoadVolume();
  }

    public void IncreaseVolume()
    {
        if (volumeBarsCount >= volumeBars.Count) return;

        if (volumeBars[volumeBarsCount].activeSelf == false)
            volumeBars[volumeBarsCount].SetActive(true);
        else if (volumeBars[volumeBarsCount].activeSelf && volumeBarsCount != volumeBars.Count - 1)
            volumeBars[volumeBarsCount + 1].SetActive(true);

        SetVolume();

        if (volumeBarsCount == 0)
            volumeBars[volumeBarsCount].GetComponent<Image>().sprite = startingBarSprite;

        if (volumeBarsCount < volumeBars.Count - 1)
        {
            volumeBarsCount++;
        }
    }

    public void DecreaseVolume()
    {
        if (volumeBarsCount <= -1) return;

        if (volumeBars[volumeBarsCount].activeSelf == true)
        {
            volumeBars[volumeBarsCount].SetActive(false);
            volumeBars[volumeBarsCount].GetComponent<Image>().sprite = endingBarSprite;
        }

        SetVolume();

        if (volumeBarsCount > 0)
        {
            volumeBarsCount--;
        }
    }

    void SetVolume()
    {
        int volumeBarsActive = 0;
        for (int i = 0; i < volumeBars.Count; i++)
        {
            if (volumeBars[i].activeSelf)
                volumeBarsActive++;
        }

        float volumeCount = (100.0f / volumeBars.Count) * volumeBarsActive;
        float volume = -80.0f + volumeCount;

        textMeshPro.text = (Mathf.RoundToInt((volumeCount / 100.0f) * 100.0f)).ToString() + "%";

        switch (volumeType)
        {
            case VOLUME_TYPE.MASTER:
                mixer.SetFloat("MasterVolume", volume);
                PlayerPrefs.SetInt("MasterVolume", volumeBarsActive);
                break;
            case VOLUME_TYPE.SFX:
                mixer.SetFloat("SFX", volume);
                PlayerPrefs.SetInt("SFX", volumeBarsActive);
                break;
            case VOLUME_TYPE.BGM:
                mixer.SetFloat("BGM", volume);
                PlayerPrefs.SetInt("BGM", volumeBarsActive);
                break;
        }
    }

    void LoadVolume()
    {
        int volumeBarsActive = 0;

        switch (volumeType)
        {
            case VOLUME_TYPE.MASTER:
                volumeBarsActive = PlayerPrefs.GetInt("MasterVolume");
                break;
            case VOLUME_TYPE.SFX:
                volumeBarsActive = PlayerPrefs.GetInt("SFX");
                break;
            case VOLUME_TYPE.BGM:
                volumeBarsActive = PlayerPrefs.GetInt("BGM");
                break;
        }

        for (int i = 0; i < volumeBars.Count; i++)
        {
            if (volumeBars[i].activeSelf)
                volumeBars[i].SetActive(false);
        }

        for (int i = 0; i < volumeBarsActive; i++)
        {
            if (volumeBars[i].activeSelf == false)
                volumeBars[i].SetActive(true);
        }

        volumeBarsCount = volumeBarsActive - 1;
    }


    void PresetVolume() 
    {
      reset = false;
      int amount = (int)(volumeBars.Count / 2.0f);

      switch (volumeType) 
      {
        case VOLUME_TYPE.MASTER:
          PlayerPrefs.SetInt("MasterVolume", amount);
          break;
        case VOLUME_TYPE.SFX:
          PlayerPrefs.SetInt("SFX", amount);
          break;
        case VOLUME_TYPE.BGM:
          PlayerPrefs.SetInt("BGM", amount);
          break;
      }
    }
}
