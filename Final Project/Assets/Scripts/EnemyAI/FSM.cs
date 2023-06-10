using System;
using System.Collections;   
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;


public enum StateType
{
    Idle, Patrol, Chase, React, Attack, Transfering, Healing
}

[Serializable]
public class Parameter
{
    public int hp;
    public float moveSpeed;
    public float chaseSpeed;
    public float idleTime;
    public Transform[] patrolPoints;
    public Transform[] chasePoints;
    public Animator animator;
    public Transform target;
    public LayerMask targetLayer;
    public Transform attackPoint;
    public float attackArea;
}
public class FSM : MonoBehaviour
{
    public Parameter param;
    private Istate currentState;
    private Dictionary<StateType, Istate> Enemystates = new Dictionary<StateType, Istate>();
    void Start()
    {
        Enemystates.Add(StateType.Idle, new IdleState(this));
        Enemystates.Add(StateType.Patrol, new PatrolState(this));
        Enemystates.Add(StateType.Chase, new ChaseState(this));
        Enemystates.Add(StateType.React, new ReactState(this));
        Enemystates.Add(StateType.Attack, new AttackState(this));
        Enemystates.Add(StateType.Transfering, new TransferingState(this));
        Enemystates.Add(StateType.Healing, new HealingState(this));
        Enemystates.Add(StateType.damaged, new damagedState(this));
        
        TransitionState(StateType.Idle);
        param.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(StateType type)
    {
        if (currentState != null) 
        { 
            currentState.OnExit();
        }
        currentState = Enemystates[type];
        currentState.OnEnter();
    }

/// <summary>
/// find the player position 
/// </summary>
/// <param name="Target"></param>
    public void Flip(Transform Target) 
    {
        if (Target != null) 
        {
            if (transform.position.x > Target.position.x) 
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x < Target.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("Player")) 
        { 
            param.target = other.transform;
            Debug.Log("abcs");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            param.target = null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(param.attackPoint.position, param.attackArea);
    }

}
