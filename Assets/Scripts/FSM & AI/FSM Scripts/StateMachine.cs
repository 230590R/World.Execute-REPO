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

  public Transform[] waypoints;

  [SerializeField] private AttackKeyValuePair[] attackDict;
  public Dictionary<string, IAttackController> attacks = new Dictionary<string, IAttackController>(); // convert the atkKVP into a dictionary on start

  public Animator m_Animator;

  public State currentState;
  public State remainInState;
  public HealthController health;

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

  public Stats stats;
  private void Start() {
    m_Seeker = GetComponent<Seeker>();
    m_Rigidbody2D = GetComponent<Rigidbody2D>();

    foreach (var KVP in attackDict) {
      attacks.Add(KVP.key, KVP.value);
    }
    Debug.Log("done");

    InvokeRepeating("UpdatePath", 0f, 0.2f);
  }

  public void Update() {
    elapsedTime += Time.deltaTime;
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



}
