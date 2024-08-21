using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineController : MonoBehaviour {
  public static CineController Instance { get; private set; }

  private CinemachineVirtualCamera m_virtualCamera;

  private float _shakeTime = 0;
  private float _shakeTimeTotal = 1;
  private float _startingIntensity = 0;

  private float _zoomTime = 0;
  private float _zoomTimeTotal = 1;
  private float _zoomScale = 1;

  private float _originalOrthoSize;



  private void Awake() {
    Instance = this; // singleton
    m_virtualCamera = GetComponent<CinemachineVirtualCamera>();
    _originalOrthoSize = m_virtualCamera.m_Lens.OrthographicSize;
    ShakeCamera(0, 0); // "clean" the cinemachine
  }

  public void ShakeCamera(float intensity, float time) {
    var cineBasicMultiChannelPerlin = m_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    cineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    _shakeTime = time;
    _shakeTimeTotal = time;
    _startingIntensity = intensity;
  }

  public void ZoomCamera(float size, float time) {
    _zoomTime = time;
    _zoomTimeTotal = time;
    _zoomScale = size;
  }

  private void Update() {
    // update shaketimer
    _shakeTime -= Time.unscaledDeltaTime;
    if (_shakeTime < 0) {
      _shakeTime = 0;
      var cineBasicMultiChannelPerlin = m_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
      cineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }
    else {
      var cineBasicMultiChannelPerlin = m_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
      cineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(0f, _startingIntensity, ((_shakeTime / _shakeTimeTotal)));
      //m_virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(8f, 9f, (1 - (_shakeTime / _shakeTimeTotal)));
    }


    _zoomTime -= Time.unscaledDeltaTime;
    if (_zoomTime < 0) {
      _zoomTime = 0;
      m_virtualCamera.m_Lens.OrthographicSize = 4f;
    }
    else {
      m_virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_originalOrthoSize, _originalOrthoSize * (1f/_zoomScale), _zoomTime / _zoomTimeTotal);
    }

  }
}
