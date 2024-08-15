using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UICursor : MonoBehaviour {
  [SerializeField] private Canvas m_Canvas;
  [SerializeField] private Transform Target;

  private float angle = 0; private float tAngle = 0;
  private float scale = 1; private float tScale = 1;

  private RectTransform m_RectTransform;
  private Vector3 _defaultScale;

  private void Start() {
    m_RectTransform = GetComponent<RectTransform>();  
    UnityEngine.Cursor.visible = false;
    _defaultScale = m_RectTransform.localScale;
  }

  private void Update() {
    // get the vector from target to self
    Vector3 distance = Target.position - transform.position;


    Vector2 cursorPos;

    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        m_Canvas.transform as RectTransform,
        Input.mousePosition, m_Canvas.worldCamera,
        out cursorPos);

    transform.position = m_Canvas.transform.TransformPoint(cursorPos);

    if (Input.GetMouseButtonDown(0)) {
      float a = Vector3.Angle(Vector3.up, distance);
      angle += 180 * ((a > 0) ? 1 : -1);
      scale = 1.5f;
    }

    angle += (tAngle - angle) * 10f * Time.deltaTime;
    scale += (tScale - scale) * 15f * Time.deltaTime;


    m_RectTransform.rotation = Quaternion.Euler(0, 0, angle);
    m_RectTransform.localScale = _defaultScale * scale;

  }
}
