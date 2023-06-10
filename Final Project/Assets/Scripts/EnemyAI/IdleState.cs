using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class IdleState : Istate
    {
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
            //UnityEngine.Debug.Log(111);
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

        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetHp(GameObject hp)
        {
            param.hp = hp;
        }
    }

    public class PatrolState : Istate
    {
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
            //UnityEngine.Debug.Log(222);
            manger.Flip(param.patrolPoints[patrolPosition]);
            manger.transform.position = Vector2.MoveTowards(manger.transform.position, param.patrolPoints[patrolPosition].position, param.moveSpeed * Time.deltaTime);

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

        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetHp(GameObject hp)
        {
            param.hp = hp;
        }
    }

    public class ChaseState : Istate
    {
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
            if (param.hp.GetComponentInChildren<Slider>().value == 20)
            {
                manger.TransitionState(StateType.Teleport);
            }
        }

        public void OnExit()
        {

        }

        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetHp(GameObject hp)
        {
            param.hp = hp;
        }
    }

    public class ReactState : Istate
    {
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

        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetHp(GameObject hp)
        {
            param.hp = hp;
        }
    }

    public class AttackState : Istate
    {
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

        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetHp(GameObject hp)
        {
            param.hp = hp;
        }
    }

    public class HitState : Istate
    {
        // Start is called before the first frame update
        private FSM manger;
        private Parameter param;
        private AnimatorStateInfo info;
        private int previousHp;

        public HitState(FSM manger)
        {
            this.manger = manger;
            this.param = manger.param;
        }

        public void OnEnter()
        {
            param.animator.SetTrigger("hit");
        }

        public void OnUpdate()
        {
            var hpValue = param.hp.GetComponentInChildren<Slider>().value;
            if (hpValue >= 0)
            {
                hpValue -= 10.0f;
                 Debug.Log("Enemy's health: " + hpValue);
                //if (hpValue <= 0.1f)
                //{
                //    param.animator.SetTrigger("die");
                //    Destory(manger, 1f);
                    
                //    return;
                //}
                param.animator.SetTrigger("isHit");
            }
        }

        public void OnExit()
        {

        }

        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetHp(GameObject hp)
        {
            param.hp = hp;
        }
    }

    public class TeleportState : Istate
    {
        // Start is called before the first frame update
        private FSM manger;
        private Parameter param;
        private AnimatorStateInfo info;

        public TeleportState(FSM manger)
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
                manger.transform.position = new Vector2(0, 0); // Teleport position
                manger.TransitionState(StateType.Healing);
            }
        }

        public void OnExit()
        {

        }

        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetHp(GameObject hp)
        {
            param.hp = hp;
        }
    }

    public class HealingState : Istate
    {
        // Start is called before the first frame update
        private FSM manger;
        private Parameter param;
        private AnimatorStateInfo info;
        private float previousHp;

        public HealingState(FSM manger)
        {
            this.manger = manger;
            this.param = manger.param;
        }

        public void OnEnter()
        {
            param.animator.SetTrigger("idle");
            previousHp = param.hp.GetComponentInChildren<Slider>().value;
        }

        public void OnUpdate()
        {
            // If not been attacked, will heal until hp full
            var hpValue = param.hp.GetComponentInChildren<Slider>().value;

            if (previousHp >= hpValue) // attack signal
            {
                hpValue += 1;
            }
            else
            {
                manger.TransitionState(StateType.Idle);
            }
            previousHp = hpValue;
        }

        public void OnExit()
        {

        }

        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetHp(GameObject hp)
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
        private int previousHp;

        public DeathState(FSM manger)
        {
            this.manger = manger;
            this.param = manger.param;
        }

        public void OnEnter()
        {
            param.animator.SetTrigger("hit");
        }

        public void OnUpdate()
        {
            var hpValue = param.hp.GetComponentInChildren<Slider>().value;
            if (hpValue <= 0.1f)
            {
                param.animator.SetTrigger("die");
                //Destory(manger, 1f);
            }
        }

        public void OnExit()
        {

        }

        public GameObject GetHp()
        {
            return param.hp;
        }

        public void SetHp(GameObject hp)
        {
            param.hp = hp;
        }
    }
}
