using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour {
  public static PostProcessController Instance;

  private float chromaticIntensity = 0;
  private float tChromaticIntensity = 0;

  private float vignetteIntensity = 0;
  private float tVignetteIntensity = 0;



  private void Awake() {
    if (Instance == null) Instance = this;
    else Destroy(this);


  }


  public Volume volume;
  private ChromaticAberration m_ChromaticAberration;
  private Vignette m_Vignette;

  private void Start() {
    volume.profile.TryGet<ChromaticAberration>(out m_ChromaticAberration);
    volume.profile.TryGet<Vignette>(out m_Vignette);
  }

  private void Update() {
    chromaticIntensity += (tChromaticIntensity - chromaticIntensity) * Time.deltaTime * 5f;
    vignetteIntensity += (tVignetteIntensity - vignetteIntensity) * Time.deltaTime * 5f;



    m_ChromaticAberration.intensity.value = chromaticIntensity;
    m_Vignette.intensity.value = vignetteIntensity;
  }



  public void SetChromatic(float intensity, float newBaseline = -1) {
    if (newBaseline >= 0) tChromaticIntensity = newBaseline;
    chromaticIntensity = intensity;
  }

  public void SetVignette(float intensity, float newBaseline = -1) {
    if (newBaseline >= 0) tVignetteIntensity = newBaseline;
    vignetteIntensity = intensity;
  }
}
