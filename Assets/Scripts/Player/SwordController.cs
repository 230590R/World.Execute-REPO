using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {

  public Animator m_SlashAnimator;
  public SpriteRenderer m_SpriteRenderer;
  public Transform ProjectedSprite;
  private MovementController m_MovementController;
  private MeleeController m_MeleeController;

  [SerializeField] private bool _slashBuffer;
  [SerializeField] private bool _slashing;
  public bool Slashing {
    get { return _slashBuffer; }
    set { _slashBuffer = value; }
  }

  [SerializeField] private Vector2 _slashDir;
  public float force;


  private float range;
  [SerializeField] private float maxRange = 5f;

  private Vector2 _raydir = Vector2.right;

  // Start is called before the first frame update
  private void Start() {
    m_MovementController = GetComponent<MovementController>();
    m_MeleeController = GetComponent<MeleeController>();
  }

  // Update is called once per frame
  private void Update() {
    if (_slashBuffer) {
      RotateSlashSprite();
      m_SlashAnimator.SetTrigger("Slash");

      m_MeleeController.Attack(_slashDir);
      _slashBuffer = false;
    }

  }

  private void RotateSlashSprite() {
    _slashDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
    float angle = Mathf.Atan2(_slashDir.y, _slashDir.x) * Mathf.Rad2Deg;
    m_SlashAnimator.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    m_SlashAnimator.GetComponent<SpriteRenderer>().flipY =
      Mathf.Abs(angle) > 90
      ? true
      : false;
    // rotate player
    m_SpriteRenderer.flipX = Mathf.Abs(angle) > 90
      ? true
      : false;
  }


  public void ChargeUpSlice(Vector2 mouse_pos) {
    Vector2 dir = (Vector3)mouse_pos - transform.position;
    _raydir = (mouse_pos - (Vector2)transform.position).normalized;

    ProjectedSprite.gameObject.SetActive(true);

    // project out the sprite
    range = Mathf.Min(maxRange, range + (Time.unscaledDeltaTime * 5f * maxRange));

    float distance = Mathf.Min(range, dir.magnitude);
    RaycastHit2D teleportResult = Physics2D.Raycast(transform.position, dir.normalized, distance, LayerMask.GetMask("Ground"));
    if (teleportResult.collider != null) {
      Vector2 teleportDir = (teleportResult.point - (Vector2)transform.position).normalized;
      Vector2 targetPos = teleportResult.point - teleportDir * 0.5f;

      ProjectedSprite.position = targetPos;
    }
    else {
      ProjectedSprite.position = (Vector2)transform.position + (distance * dir.normalized);
    }

    LineRenderer line = ProjectedSprite.gameObject.GetComponent<LineRenderer>();
    line.SetPosition(0, transform.position);
    line.SetPosition(1, ProjectedSprite.position);
  }

  public void ReleaseSlice() {
    Vector2 dir = ProjectedSprite.position - transform.position;
    Slice(dir.magnitude);
    //transform.position = ProjectedSprite.position;
    //m_Rigidbody2D.velocity = dir * 1.1f;
    ProjectedSprite.gameObject.SetActive(false);
    range = 0;
  }


  public void Slice(float distance) {
    RaycastHit2D[] RayHits = Physics2D.RaycastAll(transform.position, _raydir, distance);
    bool hasHit = false;
    foreach (RaycastHit2D hit in RayHits) {
      if (hit.collider == null) continue;
      if (hit.collider.gameObject.GetComponent<SliceableV2>() == null) continue; // only if it is sliceable

      hit.collider.gameObject.GetComponent<SliceableV2>().Slice(new Ray2D(hit.point, _raydir));

      hasHit = true;
    }
    if (hasHit) {
      //CinemachineShake.Instance.ShakeCamera(7.5f, 0.5f);
      //TimeManager.Instance.SlowMotion(0.05f, 1f);
    }
  }
}
