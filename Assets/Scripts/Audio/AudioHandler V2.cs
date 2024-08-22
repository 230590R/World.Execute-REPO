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

  public static AudioHandlerV2 Instance;

    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clips ----------")]
    public List<AudioClipCategory> audioClipCategories;


    private Dictionary<string, List<AudioClip>> audioClipsDict;

    private Dictionary<Transform, AudioSource> activeAudioSources = new Dictionary<Transform, AudioSource>();
    private void Awake()
    {

        audioClipsDict = new Dictionary<string, List<AudioClip>>();
        foreach (var category in audioClipCategories)
        {
            audioClipsDict[category.categoryName] = category.audioClips;
        }


    if (Instance == null) {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else {
      Destroy(gameObject);
    }
  }

    //private void Start()
    //{
    //    if (audioClipCategories.Count > 0 && audioClipCategories[0].audioClips.Count > 0)
    //    {
    //        BGMSource.clip = audioClipCategories[0].audioClips[0];
    //        BGMSource.Play();
    //    }
    //}



    //public void PlaySFX(string clipCategory, int clipIndex)
    //{
    //    if (audioClipsDict.TryGetValue(clipCategory, out List<AudioClip> clips))
    //    {
    //        if (clipIndex >= 0 && clipIndex < clips.Count)
    //        {
    //            SFXSource.PlayOneShot(clips[clipIndex]);
    //        }
    //        else
    //        {
    //            Debug.LogWarning($"Clip index {clipIndex} out of range for category {clipCategory}.");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning($"Audio clip category {clipCategory} not found.");
    //    }
    //}


    //public void PlaySFX(string clipCategory, int clipIndex, Transform callerTransform)
    //{
    //    if (audioClipsDict.TryGetValue(clipCategory, out List<AudioClip> clips))
    //    {
    //        if (clipIndex >= 0 && clipIndex < clips.Count)
    //        {
    //            GameObject tempAudio = new GameObject("TempAudio");
    //            tempAudio.transform.position = callerTransform.position;

    //            AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();

    //            tempAudioSource.outputAudioMixerGroup = SFXSource.outputAudioMixerGroup;
    //            tempAudioSource.rolloffMode = AudioRolloffMode.Linear;
    //            tempAudioSource.spatialBlend = 1.0f;
    //            tempAudioSource.minDistance = 0.3f;
    //            tempAudioSource.maxDistance = 15.0f;
    //            tempAudioSource.dopplerLevel = 0.0f;
    //            tempAudioSource.spread = 60.0f;

    //            tempAudioSource.clip = clips[clipIndex];
    //            tempAudioSource.Play();

    //            // Attach a follower script to make the audio follow the caller's transform
    //            tempAudio.AddComponent<AudioFollower>().target = callerTransform;

    //            Destroy(tempAudio, clips[clipIndex].length);
    //        }
    //        else
    //        {
    //            Debug.LogWarning($"Clip index {clipIndex} out of range for category {clipCategory}.");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning($"Audio clip category {clipCategory} not found.");
    //    }
    //}

    public void PlaySFX(string clipCategory, int clipIndex, Transform callerTransform, bool shouldLoop = false)
    {
        if (audioClipsDict.TryGetValue(clipCategory, out List<AudioClip> clips))
        {
            if (clipIndex >= 0 && clipIndex < clips.Count)
            {
       
                if (activeAudioSources.ContainsKey(callerTransform))
                {
 
                    Destroy(activeAudioSources[callerTransform].gameObject);
                    activeAudioSources.Remove(callerTransform);
                }

                GameObject tempAudio = new GameObject("TempAudio");
                tempAudio.transform.position = callerTransform.position;

                AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();

                tempAudioSource.outputAudioMixerGroup = SFXSource.outputAudioMixerGroup;
                tempAudioSource.rolloffMode = AudioRolloffMode.Linear;
                tempAudioSource.spatialBlend = 1.0f;
                tempAudioSource.minDistance = 0.3f;
                tempAudioSource.maxDistance = 15.0f;
                tempAudioSource.dopplerLevel = 0.0f;
                tempAudioSource.spread = 60.0f;

                tempAudioSource.clip = clips[clipIndex];
                tempAudioSource.loop = shouldLoop;  
                tempAudioSource.Play();

 
                tempAudio.AddComponent<AudioFollower>().target = callerTransform;

     
                activeAudioSources[callerTransform] = tempAudioSource;

                if (!shouldLoop)
                {
                    Destroy(tempAudio, clips[clipIndex].length);
                }
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

    public void PlaySFXIfNotPlaying(string clipCategory, int clipIndex, Transform callerTransform, bool shouldLoop = false, bool overrideAudio = true)
    {

        if (activeAudioSources.TryGetValue(callerTransform, out AudioSource activeSource))
        {
 
            if (activeSource == null)
            {
 
                activeAudioSources.Remove(callerTransform);
            }
            else if (activeSource.clip == audioClipsDict[clipCategory][clipIndex] && activeSource.isPlaying)
            {
     
                return;
            }
            else if (overrideAudio)
            {

                Destroy(activeSource.gameObject);
                activeAudioSources.Remove(callerTransform);
            }
            else
            {

                PlayTempSFX(clipCategory, clipIndex, callerTransform, shouldLoop);
                return;
            }
        }


        PlaySFX(clipCategory, clipIndex, callerTransform, shouldLoop);
    }


    private void PlayTempSFX(string clipCategory, int clipIndex, Transform callerTransform, bool shouldLoop)
    {
        if (audioClipsDict.TryGetValue(clipCategory, out List<AudioClip> clips))
        {
            if (clipIndex >= 0 && clipIndex < clips.Count)
            {
                GameObject tempAudio = new GameObject("1TempAudio");
                tempAudio.transform.position = callerTransform.position;

                AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();

                tempAudioSource.outputAudioMixerGroup = SFXSource.outputAudioMixerGroup;
                tempAudioSource.rolloffMode = AudioRolloffMode.Linear;
                tempAudioSource.spatialBlend = 1.0f;
                tempAudioSource.minDistance = 0.3f;
                tempAudioSource.maxDistance = 15.0f;
                tempAudioSource.dopplerLevel = 0.0f;
                tempAudioSource.spread = 60.0f;

                tempAudioSource.clip = clips[clipIndex];
                tempAudioSource.loop = shouldLoop;
                tempAudioSource.Play();


                tempAudio.AddComponent<AudioFollower>().target = callerTransform;

                if (!shouldLoop)
                {
                    Destroy(tempAudio, clips[clipIndex].length);
                }
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

    public void PlayBGM(string clipCategory, int clipIndex)
    {
        if (audioClipsDict.TryGetValue(clipCategory, out List<AudioClip> clips))
        {
            if (clipIndex >= 0 && clipIndex < clips.Count)
            {
                BGMSource.PlayOneShot(clips[clipIndex]);
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

    public void PlaySFXAtPosition(string clipCategory, int clipIndex, Vector3 position)
    {
        if (audioClipsDict.TryGetValue(clipCategory, out List<AudioClip> clips))
        {
            if (clipIndex >= 0 && clipIndex < clips.Count)
            {
                GameObject tempAudio = new GameObject("TempAudio");
                tempAudio.transform.position = position;

                AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();

                tempAudioSource.outputAudioMixerGroup = SFXSource.outputAudioMixerGroup;
                tempAudioSource.rolloffMode = AudioRolloffMode.Linear;
                tempAudioSource.spatialBlend = 1.0f;
                tempAudioSource.minDistance = 0.3f;
                tempAudioSource.maxDistance = 15.0f;
                tempAudioSource.dopplerLevel = 0.0f;
                tempAudioSource.spread = 60.0f;

                tempAudioSource.clip = clips[clipIndex];
                tempAudioSource.Play();

                // Destroy the temporary GameObject after the clip finishes playing
                Destroy(tempAudio, clips[clipIndex].length);
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

    public void StopPlayingSFX(Transform callerTransform)
    {
        if (activeAudioSources.TryGetValue(callerTransform, out AudioSource activeSource))
        {
            // Stop the audio and destroy the GameObject
            activeSource.Stop();
            Destroy(activeSource.gameObject);

            // Remove the audio source from the dictionary
            activeAudioSources.Remove(callerTransform);
        }
        //else
        //{
        //    Debug.LogWarning("No active sound effect found for this object.");
        //}
    }
}
