using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEditor.Search;
using UnityEngine;
using Pathfinding;
using UnityEditor;

public class StateMachine : MonoBehaviour {
  [Serializable] public class AttackKeyValuePair { public string key; public IAttackController value; }

  public Transform Player;
  public Transform Target;
  public Transform AimedTarget;

  public Transform[] waypoints;

  [SerializeField] private AttackKeyValuePair[] attackDict;
  public Dictionary<string, IAttackController> m_Attacks = new Dictionary<string, IAttackController>(); // convert the atkKVP into a dictionary on start

  public Animator m_Animator;

  public State currentState;
  public State remainInState;
  public HealthController m_HealthController;

  [HideInInspector] public Rigidbody2D m_Rigidbody2D;
  [HideInInspector] public SpriteRenderer m_SpriteRenderer;
  [HideInInspector] public Seeker m_Seeker;
  [HideInInspector] public Path path;
  [HideInInspector] public int nextPathWaypoint;

  public float elapsedTime;
  public bool pathDone = true;

  public bool usingWaypoints = true;
  public float pathWaypointThreshold = 0.5f;
  public int wp = 0;

  public int atkIndex;
  public bool atkBuffer;
  public bool atkSwitchBuffer;
    public float movementMultiplier;

  public Stats stats;
  private void Start() {
    m_Seeker = GetComponent<Seeker>();
    m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = m_Animator.GetComponent<SpriteRenderer>();

    foreach (var KVP in attackDict) {
      m_Attacks.Add(KVP.key, KVP.value);
    }

    InvokeRepeating("UpdatePath", 0f, 0.2f);

    currentState.Enter(this);
  }

  public void Update() {
    elapsedTime += Time.deltaTime;

    foreach (var KVP in attackDict) {
      var atkController = KVP.value;
      atkController.Inactive();
    }
    currentState.UpdateState(this); 
  }

  public void OnDrawGizmos() {
    Gizmos.color = Color.blue;
    Gizmos.DrawWireSphere(Target.position, 0.5f);
  }

  public void TransitionToState(State nextState) {
    if (nextState == remainInState) return; // skip if just remaining in the same state

    // exit the current state and enter the next state
    currentState.Exit(this);
    currentState = nextState;
    nextState.Enter(this);
    elapsedTime = 0;
  }

  public void UpdatePath() {
    if (m_Seeker.IsDone())
      m_Seeker.StartPath(m_Rigidbody2D.position, Target.position, OnPathComplete);

    nextPathWaypoint = 0;
  }

  public void OnPathComplete(Path p) {
    if (p.error) return;
    path = p;
  }

  public string GetStateName() {
    return currentState.name;
  }

  public IAttackController GetAttackController(string key) {
    if (m_Attacks.TryGetValue(key, out var atkController)) {
      return atkController;
    }
    // otherwise, debug log and return null
    string error = String.Format("{0} attack controller can't be found.", key);
    Debug.Log(error);
    return null;
  }

}
