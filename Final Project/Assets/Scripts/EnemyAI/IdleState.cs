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
        UnityEngine.Debug.Log(111);
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
        UnityEngine.Debug.Log(222);
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
        UnityEngine.Debug.Log(333);
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
        if (param.hp == 20)
        {
            manger.TransitionState(StateType.Transfering);
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
        UnityEngine.Debug.Log(444);
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
        param.animator.SetTrigger("pistolWhip");
    }
    public void OnUpdate()
    {
        UnityEngine.Debug.Log(555);
        info = param.animator.GetCurrentAnimatorStateInfo(0);
        UnityEngine.Debug.Log(info.normalizedTime);

        if (info.normalizedTime >= .1f)
        {
            manger.TransitionState(StateType.Chase);
        }
    }
    public void OnExit()
    {

    }
}

public class damagedState : Istate
{
    // Start is called before the first frame update
    private FSM manger;
    private Parameter param;
    private AnimatorStateInfo info;  
    private int previousHp;

    public damagedState(FSM manger)
    {
        this.manger = manger;
        this.param = manger.param;
    }

    public void OnEnter()
    {
        param.animator.SetTrigger("hit");
        previousHp = param.hp;
    }
    public void OnUpdate()
    {
        if(param.hp >= 0)
        {
            param.hp -= 10.0f;
            // Debug.Log("Enemy's health: " + health);
            if(param.hp <= 0.1f)
            {
                anim.SetTrigger("die");
                Destroy(manger, 1f);
                return;
            }

            anim.SetTrigger("isHit");
        }
    }
    public void OnExit()
    {

    }
}

public class TransferingState : Istate
{
    // Start is called before the first frame update
    private FSM manger;
    private Parameter param;
    private AnimatorStateInfo info;
    public TransferingState(FSM manger)
    {
        this.manger = manger;
        this.param = manger.param;
    }

    public void OnEnter()
    {
        param.animator.SetTrigger("smokeVanish");
    }
    public void OnUpdate()
    {
        info = param.animator.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= .95f)
        {
            manger.transform.position = new Vector2(0,0); // Teleport position
            manger.TransitionState(StateType.Healing);
        } 
    }
    public void OnExit()
    {
        
    }
}

public class HealingState : Istate
{
    // Start is called before the first frame update
    private FSM manger;
    private Parameter param;
    private AnimatorStateInfo info;  
    private int previousHp;

    public HealingState(FSM manger)
    {
        this.manger = manger;
        this.param = manger.param;
    }

    public void OnEnter()
    {
        param.animator.SetTrigger("idle");
        previousHp = param.hp;
    }
    public void OnUpdate()
    {
        // If not been attacked, will heal until hp full
        if (previousHp >= param.hp) // attack signal
        {
            param.hp += 1;
        }
        else
        {
            manger.TransitionState(StateType.Idle);    
        }
        previousHp = param.hp;
    }
    public void OnExit()
    {

    }
}
