using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour {
  public Material dissolveMat;
  public float duration = 1;
  private float elapsed;

  private Material m_Material;
  bool dissolving = false;

  private float fade = 1;

  private void Start() {
    var m_SprRenderer = GetComponent<SpriteRenderer>();
    m_SprRenderer.material = dissolveMat;
    m_Material = m_SprRenderer.material;
  }

  private void OnEnable() {
    elapsed = 0;
    dissolving = true;

  }

  private void Update() {
    if (!dissolving) return;

    m_Material.SetFloat("_Fade", fade);

    if (elapsed <= duration) {
      elapsed += Time.deltaTime;

    }
    fade = Mathf.Lerp(1, 0, (elapsed/duration));


  }
}
