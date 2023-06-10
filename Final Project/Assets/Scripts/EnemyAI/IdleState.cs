using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : Istate
{
    // Start is called before the first frame update
    private FSM manger;
    private Parameter param;
    private float timer;
    public IdleState(FSM manger) 
    { 
        this.manger = manger;
        this.param = manger.param;
    }

    public void OnEnter() 
    {
        param.animator.SetTrigger("idle");
        
    }
    public void OnUpdate() 
    {
        timer += Time.deltaTime;

        if (param.target != null && param.target.position.x >= param.chasePoints[0].position.x
            && param.target.position.x <= param.chasePoints[1].position.x) 
        {

            manger.TransitionState(StateType.React);
        }
        if (timer >= param.idleTime)
        {
            manger.TransitionState(StateType.Patrol);
        }
    }
    public void OnExit() 
    {
        timer = 0f;
    }
}

public class PatrolState : Istate
{
    // Start is called before the first frame update
    private FSM manger;
    private Parameter param;
    private int patrolPosition;
    public PatrolState(FSM manger)
    {
        this.manger = manger;
        this.param = manger.param;
    }

    public void OnEnter()
    {
        param.animator.SetTrigger("isRun");
        
    }
    public void OnUpdate()
    {
        manger.Flip(param.patrolPoints[patrolPosition]);
        manger.transform.position = Vector2.MoveTowards(manger.transform.position, param.patrolPoints[patrolPosition].position, param.moveSpeed*Time.deltaTime);

        if (param.target != null && param.target.position.x >= param.chasePoints[0].position.x
            && param.target.position.x <= param.chasePoints[1].position.x)
        {

            manger.TransitionState(StateType.React);
        }

        if (Vector2.Distance(manger.transform.position, param.patrolPoints[patrolPosition].position) < 1f) 
        {
            manger.TransitionState(StateType.Patrol);
        }
    }
    public void OnExit()
    {
        patrolPosition++;

        if (patrolPosition >= param.patrolPoints.Length) 
        {
            patrolPosition = 0;
        }
    }
}

public class ChaseState : Istate
{
    // Start is called before the first frame update
    private FSM manger;
    private Parameter param;
    public ChaseState(FSM manger)
    {
        this.manger = manger;
        this.param = manger.param;
    }

    public void OnEnter()
    {
        param.animator.SetTrigger("isRun");
    }
    public void OnUpdate()
    {
        manger.Flip(param.target);
        
        if (param.target) 
        {
            manger.transform.position = Vector2.MoveTowards(manger.transform.position, param.target.position, param.chaseSpeed * Time.deltaTime);
        }
        if (param.target == null || manger.transform.position.x < param.chasePoints[0].position.x || manger.transform.position.x > param.chasePoints[1].position.x) 
        {
            manger.TransitionState(StateType.Idle);
        }
        if (Physics2D.OverlapCircle(param.attackPoint.position, param.attackArea, param.targetLayer)) 
        {
            manger.TransitionState(StateType.Attack);
        }

    }
    public void OnExit()
    {

    }
}
public class ReactState : Istate
{
    // Start is called before the first frame update
    private FSM manger;
    private Parameter param;
    private AnimatorStateInfo info;
    public ReactState(FSM manger)
    {
        this.manger = manger;
        this.param = manger.param;
    }

    public void OnEnter()
    {
        param.animator.SetTrigger("isRun");
        
    }
    public void OnUpdate()
    {
        info = param.animator.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= .95f) 
        {
            manger.TransitionState(StateType.Chase);
        }
    }
    public void OnExit()
    {

    }
}

public class AttackState : Istate
{
    // Start is called before the first frame update
    private FSM manger;
    private Parameter param;
    private AnimatorStateInfo info;
    public AttackState(FSM manger)
    {
        this.manger = manger;
        this.param = manger.param;
    }

    public void OnEnter()
    {
        param.animator.SetTrigger("attack");
    }
    public void OnUpdate()
    {
        info = param.animator.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= .95f)
        {
            manger.TransitionState(StateType.Chase);
        }
    }
    public void OnExit()
    {

    }
}
