using ExtensionMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour {
  // variables for the inspector
  public SpriteRenderer m_SpriteRenderer;
  [SerializeField] private float movementSpeed;
  [SerializeField] private Vector2 _dashSpeed = new Vector2(100, 0);
  [SerializeField] private float groundRayDist;

  // public variables for player controller to interface with
  [HideInInspector] public bool _grounded;
  [HideInInspector] public bool jump;
  [HideInInspector] public bool dash;
  [HideInInspector] public bool flipX;
  [HideInInspector] public float horizontalAxis;

  // private variables
  private Rigidbody2D m_Rigidbody2D;
  private Vector2 groundTangent;
  private float spriteAngle;
  private Vector2 _dashVectorVelocity = Vector3.zero;


  // Start is called before the first frame update
  private void Start() {
    m_Rigidbody2D = GetComponent<Rigidbody2D>();
  }

  private void Update() {
    // raycast and determine groundedness
    Vector2 rayDir = Vector2.down;

    RaycastHit2D groundHit = Physics2D.Raycast(transform.position, rayDir, groundRayDist, LayerMask.GetMask("Ground"));
    _grounded = groundHit.collider != null;
    groundTangent = _grounded
      ? Vector2.Perpendicular(groundHit.normal)
      : Vector2.zero;

    Debug.DrawRay(groundHit.point, groundTangent, Color.yellow);


    // update rotation
    float targetAngle = Vector2.SignedAngle(Vector2.left, groundTangent);
    spriteAngle = Mathf.LerpAngle(spriteAngle, targetAngle, Time.deltaTime * 10f);
    m_SpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, spriteAngle);
  }



  private void FixedUpdate() {
    // read the dash input buffer
    if (dash) {
      int dir = (flipX) ? -1 : 1;
      _dashVectorVelocity = _dashSpeed * dir;
      dash = false;
    }

    // deaccelerate dash vel
    _dashVectorVelocity += (-_dashVectorVelocity) * Time.deltaTime * 5f;

    // set the movement velocity
    Vector2 movementVel = new Vector2(horizontalAxis * movementSpeed, 0) * (_grounded ? 1 : 0.75f);
    m_Rigidbody2D.velocity = new Vector2(movementVel.x, m_Rigidbody2D.velocity.y) + _dashVectorVelocity;

    // read the jump input buffer, overwrite y if jumping
    if (jump) {
      jump = false;
      if (_grounded) {
        m_Rigidbody2D.velocity = new Vector3(m_Rigidbody2D.velocity.x, 10);
      }
    }
  }


}
