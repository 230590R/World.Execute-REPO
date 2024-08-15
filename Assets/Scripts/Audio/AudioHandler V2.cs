using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioHandlerV2 : MonoBehaviour
{
    [System.Serializable]
    public class AudioClipCategory
    {
        public string categoryName;
        public List<AudioClip> audioClips;
    }

    [System.Serializable]
    public class AudioPrefab
    {
        public string prefabName;
        public GameObject prefab;
    }

    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clips ----------")]
    public List<AudioClipCategory> audioClipCategories;

    [Header("---------- Prefab Audio Sources ----------")]
    public List<AudioPrefab> audioPrefabs;

    private Dictionary<string, List<AudioClip>> audioClipsDict;
    private Dictionary<string, GameObject> audioPrefabsDict;

    private void Awake()
    {
        // Initialize audio clips dictionary from list
        audioClipsDict = new Dictionary<string, List<AudioClip>>();
        foreach (var category in audioClipCategories)
        {
            audioClipsDict[category.categoryName] = category.audioClips;
        }

        // Initialize audio prefabs dictionary from list
        audioPrefabsDict = new Dictionary<string, GameObject>();
        foreach (var prefab in audioPrefabs)
        {
            audioPrefabsDict[prefab.prefabName] = prefab.prefab;
        }
    }

    private void Start()
    {
        if (audioClipCategories.Count > 0 && audioClipCategories[0].audioClips.Count > 0)
        {
            BGMSource.clip = audioClipCategories[0].audioClips[0];
            BGMSource.Play();
        }
    }

    public void PlaySFX(string clipCategory, int clipIndex)
    {
        if (audioClipsDict.TryGetValue(clipCategory, out List<AudioClip> clips))
        {
            if (clipIndex >= 0 && clipIndex < clips.Count)
            {
                SFXSource.PlayOneShot(clips[clipIndex]);
            }
            else
            {
                Debug.LogWarning($"Clip index {clipIndex} out of range for category {clipCategory}.");
            }
        }
        else
        {
            Debug.LogWarning($"Audio clip category {clipCategory} not found.");
        }
    }

    public void SpawnAudioPrefab(string prefabName, Vector3 position, Quaternion rotation)
    {
        if (audioPrefabsDict.TryGetValue(prefabName, out GameObject prefab))
        {
            Instantiate(prefab, position, rotation);
        }
        else
        {
            Debug.LogWarning($"Audio prefab with name {prefabName} not found.");
        }
    }
}
