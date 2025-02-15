using ExtensionMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour {
  // variables for the inspector
  public SpriteRenderer m_SpriteRenderer;
  public float movementSpeed;
  [SerializeField] private float _gravityClamp = 10;
  public Vector2 dashSpeed = new Vector2(15, 0);
  public Vector2 wallJumpSpeed = new Vector2(15, 15);
  [SerializeField] private float _jumpStrength = 10;
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

  // velocity vectors
  private Vector2 _dashVelocity = Vector3.zero;
  private Vector2 _wallJumpVelocity = Vector3.zero;
  private Vector2 _knockBackVelocity = Vector3.zero;

  public bool dashing;

  public float prevXVel = 0;

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

    // raycast and determine if you can wall jump
    Vector2 rayDir = Vector2.right * GetPlayerFacingDirection(flipX);
    RaycastHit2D wallHit = Physics2D.Raycast(transform.position, rayDir, wallJumpRayDist, LayerMask.GetMask("Ground"));
    Debug.DrawRay(transform.position, rayDir * wallJumpRayDist, Color.red);

    bool canWallJump = wallHit.collider != null;

    // decelerate the velocities
    _dashVelocity += (-_dashVelocity) * Time.deltaTime * 5f;
    _wallJumpVelocity += (-_wallJumpVelocity) * Time.deltaTime * 5f;
    _knockBackVelocity += (-_knockBackVelocity) * Time.deltaTime * 5f;

    // override the movement velocity
    Vector2 movementVel = new Vector2(horizontalAxis * movementSpeed, 0) * (_grounded ? 1 : 0.75f);
    if (canWallJump) movementVel = Vector2.zero;
    float velY = Mathf.Max(m_Rigidbody2D.velocity.y, _gravityClamp);
    float velX = movementVel.x + _dashVelocity.x + _wallJumpVelocity.x + _knockBackVelocity.x;

    m_Rigidbody2D.velocity = new Vector2(velX, velY);

    // read the dash input buffer
    if (dash) {
      _dashVelocity = dashSpeed * GetPlayerFacingDirection(flipX);
      dash = false;
    }

    // read the jump input buffer, overwrite y if jumping
    if (jump) {
      if (_grounded) {
        m_Rigidbody2D.velocity = new Vector3(m_Rigidbody2D.velocity.x, _jumpStrength);
      }
      else if (canWallJump) {
        _wallJumpVelocity = new Vector2(wallJumpSpeed.x * -GetPlayerFacingDirection(flipX), wallJumpSpeed.y);
        m_Rigidbody2D.velocity = new Vector2(0, _wallJumpVelocity.y);
      }
      jump = false;
    }


    dashing = (_dashVelocity.magnitude >= 3);
  }

  int GetPlayerFacingDirection(bool flipX) {
    return (flipX) ? -1 : 1;
  }

  public void KnockBack(Vector2 knockback) {
    _wallJumpVelocity = knockback;
    m_Rigidbody2D.velocity = new Vector3(m_Rigidbody2D.velocity.x, knockback.y);
  }
}
