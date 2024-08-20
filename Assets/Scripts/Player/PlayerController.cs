using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MovementController))]
public class PlayerController : MonoBehaviour {

  public PlayerStats m_Stats;

  private MovementController m_MovementController;
  public Animator m_Animator;
  public SwordController m_SwordController;
  public Rigidbody2D m_Rigidbody2D;
  private GrapplingGun m_GrapplingGunController;
  private HealthController m_HealthController;

  private SpriteRenderer m_SpriteRenderer;
  public Collider2D m_Collider;

  public PlayerSaveable m_Saveable;
  public bool readData = true;

  [SerializeField] static private bool firstLoad = true;

  private float atkCD;
  private float rollCD;

  private bool dead = false;

  // Start is called before the first frame update
  private void Start() {
    m_MovementController = GetComponent<MovementController>();
    m_SpriteRenderer = m_Animator.GetComponent<SpriteRenderer>();
    m_SwordController = GetComponent<SwordController>();
    m_GrapplingGunController = GetComponentInChildren<GrapplingGun>();
    m_Rigidbody2D = GetComponent<Rigidbody2D>();
    m_HealthController = GetComponent<HealthController>();

    if (firstLoad) {
      firstLoad = false;
      Debug.Log("asdasd");
    }
    else m_Saveable.Load(this);

    if (m_HealthController.health <= 0) {
      m_HealthController.health = m_HealthController.maxHealth;
    }

    InvokeRepeating("Regen", 0, 0.5f);
  }

  private void OnDestroy() {
    m_Saveable.Save(this);
  }

  // Update is called once per frame
  private void Update() {
    if (dead) {
      m_Animator.transform.localPosition = Vector3.Lerp(m_Animator.transform.localPosition, new Vector3(0, -0.2f, 0), Time.deltaTime * 2);

      if (Input.GetKeyDown(KeyCode.E)) {
        Respawn();
      }
      return;
    }

    // check health
    if (m_HealthController.health <= 0) {
      // apply death effects
      CineController.Instance.ShakeCamera(5, 0.2f);
      CineController.Instance.ZoomCamera(1.75f, 2f);
      TimeController.Instance.SlowTime(0.1f, 2f);


      m_Animator.SetBool("dead", true);
      m_Animator.SetBool("grounded", true);
      m_MovementController.horizontalAxis = 0;

      CancelInvoke();

      dead = true;
      return;
    }


    UpdateStats();

    atkCD = Mathf.Max(0, atkCD - Time.deltaTime);
    rollCD = Mathf.Max(0, rollCD - Time.deltaTime);

    Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    // read inputs
    float horizontalAxis = Input.GetAxis("Horizontal");
    m_MovementController.horizontalAxis = Input.GetAxis("Horizontal");

    float axisX = Input.GetAxis("Horizontal");
    m_Animator.SetFloat("inputX", Mathf.Abs(axisX));


    if (!axisX.IsZero()) 
      m_SpriteRenderer.flipX = (axisX < 0);


    m_MovementController.flipX = m_SpriteRenderer.flipX;

    m_GrapplingGunController.GrappleControls(KeyCode.Mouse2);

    if (Input.GetKeyDown(KeyCode.Mouse0) && atkCD <= 0) {
      m_SwordController.Slashing = true;
      m_Animator.SetTrigger("attack");
      atkCD = m_Stats.attackCooldown;
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

    if (Input.GetKeyDown(KeyCode.LeftShift) && rollCD <= 0) {
      m_MovementController.dash = true;
      m_Animator.SetTrigger("roll");
      rollCD = m_Stats.rollCooldown;
    }

    if (m_MovementController.dashing) {
      m_Collider.excludeLayers = LayerMask.GetMask("Enemy");
      Debug.Log("DASHING");
    }
    else {
      m_Collider.excludeLayers = LayerMask.GetMask("Nothing");

    }

    m_Animator.SetBool("grounded", m_MovementController._grounded);
    m_Animator.SetFloat("yVel", m_Rigidbody2D.velocity.y);


    if (Input.GetMouseButton(1)) {
      m_SwordController.ChargeUpSlice(mouse_pos);
    }

    if (Input.GetMouseButtonUp(1)) {
      m_SwordController.ReleaseSlice();
      transform.position = m_SwordController.ProjectedSprite.position;
    }
  }

  private void UpdateStats() {
    m_MovementController.movementSpeed = m_Stats.movementSpeed;
    m_MovementController.dashSpeed = m_Stats.dashSpeed;
    m_MovementController.wallJumpSpeed = m_Stats.wallJumpSpeed;
    m_HealthController.maxHealth = m_Stats.maxHealth;
  }


  private void Regen() {
    m_HealthController.health += m_Stats.regen;
  }


  private void Respawn() {
    m_HealthController.health = m_HealthController.maxHealth;
    SceneManager.LoadScene("HubScene");
  }
}
