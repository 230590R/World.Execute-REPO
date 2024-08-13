using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

[RequireComponent(typeof(MovementController))]
public class PlayerController : MonoBehaviour {

  private MovementController m_MovementController;
  public Animator m_Animator;
  public SwordController m_SwordController;
  public Rigidbody2D m_Rigidbody2D;
  public GrapplingGun m_GrapplingGunController;

  private SpriteRenderer m_SpriteRenderer;



  // Start is called before the first frame update
  private void Start() {
    m_MovementController = GetComponent<MovementController>();
    m_SpriteRenderer = m_Animator.GetComponent<SpriteRenderer>();
    m_SwordController = GetComponent<SwordController>();
    m_GrapplingGunController = GetComponentInChildren<GrapplingGun>();
    m_Rigidbody2D = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  private void Update() {
    Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


    // read inputs
    float horizontalAxis = Input.GetAxis("Horizontal");
    m_MovementController.horizontalAxis = Input.GetAxis("Horizontal");

    float axisX = Input.GetAxis("Horizontal");
    m_Animator.SetFloat("inputX", Mathf.Abs(axisX));
    if (!axisX.IsZero()) {
      m_SpriteRenderer.flipX = (axisX < 0) ? true : false;

    }
    m_MovementController.flipX = m_SpriteRenderer.flipX;

    m_GrapplingGunController.GrappleControls(KeyCode.Mouse2);

    if (Input.GetKeyDown(KeyCode.Mouse0)) {
      m_SwordController.Slashing = true;
      m_Animator.SetTrigger("attack");
    }

    if (Input.GetKeyDown(KeyCode.Space)) {
      m_MovementController.jump = true;
    }

    bool crouch = false;
    if (Input.GetKey(KeyCode.S)) {
      if (Input.GetAxis("Horizontal").IsZero()) {
        crouch = true;
      }
    }

    m_Animator.SetBool("crouch", crouch);

    if (Input.GetKeyDown(KeyCode.LeftShift)) {
      m_MovementController.dash = true;
      m_Animator.SetTrigger("roll");
    }


    m_Animator.SetBool("grounded", m_MovementController._grounded);
    m_Animator.SetFloat("yVel", m_Rigidbody2D.velocity.y);


    if (Input.GetMouseButton(1)) {
      m_SwordController.ChargeUpSlice(mouse_pos);
    }

    if (Input.GetMouseButtonUp(1)) {
      m_SwordController.ReleaseSlice();
      transform.position = m_SwordController.ProjectedSprite.position;
      //m_Rigidbody2D.velocity = dir * 1.1f;
    }
  }

}
