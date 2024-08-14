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
  [SerializeField] private Vector2 _wallJumpSpeed = new Vector2(50, 50);
  [SerializeField] private float groundRayDist;
  [SerializeField] private float wallJumpRayDist;

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
  private Vector2 _wallJumpVelocity = Vector3.zero;
    [SerializeField] private bool _wallJump;


  


  // Start is called before the first frame update
  private void Start() {
    m_Rigidbody2D = GetComponent<Rigidbody2D>();
  }

  private void Update() {
    // update rotation
    float targetAngle = Vector2.SignedAngle(Vector2.left, groundTangent);
    spriteAngle = Mathf.LerpAngle(spriteAngle, targetAngle, Time.deltaTime * 10f);
    m_SpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, spriteAngle);
  }

  private void FixedUpdate() {
    // raycast and determine groundedness
    RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, groundRayDist, LayerMask.GetMask("Ground"));
    _grounded = groundHit.collider != null;
    groundTangent = _grounded
    ? Vector2.Perpendicular(groundHit.normal)
    : Vector2.zero;

    Debug.DrawRay(groundHit.point, groundTangent, Color.yellow);

    // read the dash input buffer
    if (dash) {
      _dashVectorVelocity = _dashSpeed * GetPlayerFacingDirection(flipX);
      dash = false;
    }

    WallJump();

    // deaccelerate other velocities
    _dashVectorVelocity += (-_dashVectorVelocity) * Time.deltaTime * 5f;
    _wallJumpVelocity += (-_wallJumpVelocity) * Time.deltaTime * 5f;

    // override the movement velocity
    Vector2 movementVel = new Vector2(horizontalAxis * movementSpeed, 0) * (_grounded ? 1 : 0.75f);
    if (_wallJump) movementVel = Vector2.zero;
    m_Rigidbody2D.velocity = new Vector2(movementVel.x, m_Rigidbody2D.velocity.y) + _dashVectorVelocity + _wallJumpVelocity;

    // read the jump input buffer, overwrite y if jumping
    if (jump) {
      jump = false;
      if (_grounded) {
        m_Rigidbody2D.velocity = new Vector3(m_Rigidbody2D.velocity.x, 10);
      }
      else if (_wallJump) {
        _wallJumpVelocity = new Vector2(-_wallJumpSpeed.x * GetPlayerFacingDirection(flipX), _wallJumpSpeed.y);
      }
    }
  }

  int GetPlayerFacingDirection(bool flipX) {
    return (flipX) ? -1 : 1;
  }

  // zhen yi code
  private void WallJump() {
    Vector2 rayDir = Vector2.right * GetPlayerFacingDirection(flipX);
    RaycastHit2D wallHit = Physics2D.Raycast(transform.position, rayDir, wallJumpRayDist, LayerMask.GetMask("Ground"));
    Debug.DrawRay(transform.position, rayDir * wallJumpRayDist, Color.red);

    _wallJump = wallHit.collider != null;
  }
}
