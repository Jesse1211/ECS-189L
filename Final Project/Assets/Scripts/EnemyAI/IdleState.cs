using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

namespace Project {
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
            if (param.getHit)
            {
                manger.TransitionState(StateType.IsHit);

            }
            if (!param.firstTele)
            {
                if (param.target != null && param.target.position.x >= param.chasePoints[0].position.x
                && param.target.position.x <= param.chasePoints[1].position.x)
                {

                    manger.TransitionState(StateType.React);
                }
            }
            if (param.firstTele)
            {
                 if (param.target != null && param.target.position.x >= param.chasePoints[2].position.x
                 && param.target.position.x <= param.chasePoints[3].position.x)
                 {

                     manger.TransitionState(StateType.React);
                 }
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
        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetUp(GameObject hp)
        {
            param.hp = hp;
        }
    }

    public class PatrolState : Istate
    {
        // Start is called before the first frame update
        private FSM manger;
        private Parameter param;
        private int patrolPosition;
        private bool telePort = false;
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
            if (param.initTele)
            {
                param.initTele = false;
                if (param.firstTele)
                {
                    patrolPosition = 2;
                }
                else
                {
                    patrolPosition = 0;
                }
            }
            manger.Flip(param.patrolPoints[patrolPosition]);
            manger.transform.position = Vector2.MoveTowards(manger.transform.position, param.patrolPoints[patrolPosition].position, param.moveSpeed * Time.deltaTime);

            if (param.getHit)
            {
                manger.TransitionState(StateType.IsHit);

            }

            if (param.target != null && param.target.position.x >= param.chasePoints[0].position.x
                && param.target.position.x <= param.chasePoints[1].position.x && !param.firstTele)
            {

                manger.TransitionState(StateType.React);
            }
            if (param.target != null && param.target.position.x >= param.chasePoints[1].position.x
               && param.target.position.x <= param.chasePoints[2].position.x && param.firstTele)
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

            if (!param.firstTele)
            {
                patrolPosition++;
                if (patrolPosition >= 2)
                {
                    patrolPosition = 0;
                }
            }

            else 
            {
                patrolPosition++;
                if (patrolPosition >= 4)
                {
                    patrolPosition = 2;
                }
            }
        }
        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetUp(GameObject hp)
        {
            param.hp = hp;
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

            if (param.getHit)
            {
                manger.TransitionState(StateType.IsHit);

            }
            if (param.target)
            {
                manger.transform.position = Vector2.MoveTowards(manger.transform.position, param.target.position, param.chaseSpeed * Time.deltaTime);
            }
            if (!param.firstTele)
            {
                if (param.target == null || manger.transform.position.x < param.chasePoints[0].position.x || manger.transform.position.x > param.chasePoints[1].position.x)
                {
                    manger.TransitionState(StateType.Idle);
                }
            }
            if (param.firstTele)
            {

                if (param.target == null || manger.transform.position.x < param.chasePoints[2].position.x || manger.transform.position.x > param.chasePoints[3].position.x)
                {
                    manger.TransitionState(StateType.Idle);
                }
            }
            if (Physics2D.OverlapCircle(param.attackPoint.position, param.attackArea, param.targetLayer))
            {
                manger.TransitionState(StateType.Attack);
            }

        }
        public void OnExit()
        {

        }
        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetUp(GameObject hp)
        {
            param.hp = hp;
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

            if (param.getHit)
            {
                manger.TransitionState(StateType.IsHit);

            }

            if (info.normalizedTime >= .95f)
            {
                manger.TransitionState(StateType.Chase);
            }
        }
        public void OnExit()
        {

        }
        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetUp(GameObject hp)
        {
            param.hp = hp;
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
            if (param.getHit)
            {
                manger.TransitionState(StateType.IsHit);

            }
            if (info.normalizedTime >= .95f)
            {
                manger.TransitionState(StateType.Chase);
            }
        }
        public void OnExit()
        {

        }
        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetUp(GameObject hp)
        {
            param.hp = hp;
        }
    }
    public  class HitState : Istate
    {
        // Start is called before the first frame update
        private FSM manger;
        private Parameter param;
        private AnimatorStateInfo info;
        private Slider slider;


        public HitState(FSM manger)
        {
            this.manger = manger;
            this.param = manger.param;
            this.slider = param.hp.GetComponentInChildren<Slider>();
        }

        public void OnEnter()
        {
            param.animator.SetBool("isHit", true);
            if (param.getHit)
            {
                slider.value -= 10f;
            }

            
        }
        public void OnUpdate()
        {
            info = param.animator.GetCurrentAnimatorStateInfo(0);

            if (slider.value <= 0)
            {
                manger.TransitionState(StateType.Death);
            }
            if (slider.value >= 10 && slider.value <= 30) 
            {
                manger.TransitionState(StateType.teleport);
            }
            else
            {
                //param.target = GameObject.FindWithTag("Players").transform;
                manger.TransitionState(StateType.Chase);
            }
        }
        public void OnExit()
        {
            param.getHit = false;
        }
        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetUp(GameObject hp)
        {
            param.hp = hp;
        }
    }

    public class DeathState : Istate
    {
        // Start is called before the first frame update
        private FSM manger;
        private Parameter param;
        private AnimatorStateInfo info;
        public DeathState(FSM manger)
        {
            this.manger = manger;
            this.param = manger.param;
        }

        public void OnEnter()
        {
            param.animator.SetBool("die", true);
        }
        public void OnUpdate()
        {
            Debug.Log("acinima");
        }
        public void OnExit()
        {

        }
        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetUp(GameObject hp)
        {
            param.hp = hp;
        }
    }

    public class TeleState : Istate
    {
        // Start is called before the first frame update
        private FSM manger;
        private Parameter param;
        private AnimatorStateInfo info;
        private Slider slider;
        
        public TeleState(FSM manger)
        {
            this.manger = manger;
            this.param = manger.param;
            this.slider = param.hp.GetComponentInChildren<Slider>();
        }

        public void OnEnter()
        {
            param.animator.SetTrigger("teleVanish");
            slider.value += 20;
            
        }
        public void OnUpdate()
        {
            info = param.animator.GetCurrentAnimatorStateInfo(0);

            if (param.firstTele)
            {
                if (info.normalizedTime >= .95f)
                {
                    manger.transform.position = new Vector2(param.telePoints[0].transform.position.x, param.telePoints[0].transform.position.y);
                    manger.TransitionState(StateType.Idle);
                    param.firstTele = false;
                    param.initTele = true;

                }
            }
            else 
            {
                if (info.normalizedTime >= .95f)
                {
                    manger.transform.position = new Vector2(param.telePoints[1].transform.position.x, param.telePoints[1].transform.position.y);
                    manger.TransitionState(StateType.Idle);
                    param.firstTele = true;
                    param.initTele = true;
                }
                
            }
        }
        public void OnExit()
        {
           
        }
        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetUp(GameObject hp)
        {
            param.hp = hp;
        }
    }



   
}