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
                && param.target.position.x <= param.chasePoints[1].position.x && param.isFirstPosition)
            {
                manger.TransitionState(StateType.React);
            }
            else if (param.target != null && param.target.position.x >= param.chasePoints[2].position.x
                && param.target.position.x <= param.chasePoints[3].position.x && !(param.isFirstPosition))
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
            patrolPosition = 0;
        }

        public void OnEnter()
        {
            param.animator.SetTrigger("isRun");

        }

        public void OnUpdate()
        {
            UnityEngine.Debug.Log("PatrolState");
            Debug.Log(param.target != null);
            manger.Flip(param.patrolPoints[patrolPosition]);
            manger.transform.position = Vector2.MoveTowards(manger.transform.position, param.patrolPoints[patrolPosition].position, param.moveSpeed * Time.deltaTime);

            if (param.target != null && param.target.position.x >= param.chasePoints[0].position.x
                && param.target.position.x <= param.chasePoints[1].position.x && param.isFirstPosition)
            {
                manger.TransitionState(StateType.React);
            }
            else if (param.target != null && param.target.position.x >= param.chasePoints[2].position.x
                && param.target.position.x <= param.chasePoints[3].position.x && !param.isFirstPosition)
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
            if (param.initiate)
            {
                param.initiate = false;
                if (param.isFirstPosition)
                {
                    patrolPosition = 0;
                }
                else
                {
                    patrolPosition = 2;
                }
            }
            else
            {
                if (param.isFirstPosition)
                {
                    patrolPosition = (patrolPosition + 1) % 2;
                }
                else
                {
                    patrolPosition = (patrolPosition + 1) % 2 + 2;
                }
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
            UnityEngine.Debug.Log("ChaseState");
            manger.Flip(param.target);

            if (param.target)
            {
                manger.transform.position = Vector2.MoveTowards(manger.transform.position, param.target.position, param.chaseSpeed * Time.deltaTime);
            }
            if ((param.target == null || manger.transform.position.x < param.chasePoints[0].position.x || manger.transform.position.x > param.chasePoints[1].position.x) && param.isFirstPosition)
            {
                manger.TransitionState(StateType.Idle);
            }
            if ((param.target == null || manger.transform.position.x < param.chasePoints[2].position.x || manger.transform.position.x > param.chasePoints[3].position.x) && !param.isFirstPosition)
            {
                manger.TransitionState(StateType.Idle);
            }
            if (Physics2D.OverlapCircle(param.attackPoint.position, param.attackArea, param.targetLayer))
            {
                manger.TransitionState(StateType.Attack);
            }
            if (Input.GetKeyDown(KeyCode.J) && Physics2D.OverlapCircle(param.target.GetComponent<PlayerControllerAnimator>().attackPoint.position, 
                                                                       param.target.GetComponent<PlayerControllerAnimator>().attackRange, 
                                                                       param.selfLayer))
            {
                manger.TransitionState(StateType.Hit);
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
            UnityEngine.Debug.Log("ReactState");
            info = param.animator.GetCurrentAnimatorStateInfo(0);

            if (info.normalizedTime >= .95f)
            {
                manger.TransitionState(StateType.Chase);
            }

            if (Input.GetKeyDown(KeyCode.J) && Physics2D.OverlapCircle(param.target.GetComponent<PlayerControllerAnimator>().attackPoint.position, 
                                                                       param.target.GetComponent<PlayerControllerAnimator>().attackRange, 
                                                                       param.selfLayer))
            {
                manger.TransitionState(StateType.Hit);
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
            UnityEngine.Debug.Log("AttackState");
            info = param.animator.GetCurrentAnimatorStateInfo(0);

            if (info.normalizedTime >= 1.0f)
            {
                manger.TransitionState(StateType.Chase);
            }

            if (Input.GetKeyDown(KeyCode.J) && Physics2D.OverlapCircle(param.target.GetComponent<PlayerControllerAnimator>().attackPoint.position, 
                                                                       param.target.GetComponent<PlayerControllerAnimator>().attackRange, 
                                                                       param.selfLayer))
            {
                manger.TransitionState(StateType.Hit);
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
        // private Slider slider;
        // private int previousHp;

        public HitState(FSM manger)
        {
            this.manger = manger;
            this.param = manger.param;
            // this.slider = param.hp.GetComponentInChildren<Slider>();
        }

        public void OnEnter()
        {
            param.animator.SetTrigger("isHit");
        }

        public void OnUpdate()
        {
            Debug.Log("HitState");

            // slider.value = slider.value - 10.0f;

            info = param.animator.GetCurrentAnimatorStateInfo(0);

            if (info.normalizedTime >= 1.5f)
            {
                manger.TransitionState(StateType.Chase);
            }
            // Debug.Log(1);
            // if (hpValue >= 0)
            // {
            //     Debug.Log(2);
            //     hpValue -= 10.0f;
            //     //  Debug.Log("Enemy's health: " + hpValue);
            //     // //if (hpValue <= 0.1f)
            //     // //{
            //     // //    param.animator.SetTrigger("die");
            //     // //    Destory(manger, 1f);
                    
            //     // //    return;
            //     // //}
            //     // param.animator.SetTrigger("isHit");
            //     if (hpValue <= 30)
            //     {
            //         manger.TransitionState(StateType.Teleport);
            //     }
            //     manger.TransitionState(StateType.Chase);
            // }
            // Debug.Log(3);
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
            Debug.Log("TeleportState");
            info = param.animator.GetCurrentAnimatorStateInfo(0);

            if (isFirstPosition)
            {
                isFirstPosition = false;
            }
            else
            {
                isFirstPosition = true;
            }

            if (info.normalizedTime >= .95f)
            {
                manger.transform.position = new Vector2(0, 0); // Teleport position
                manger.TransitionState(StateType.Healing);
            }

            if (Input.GetKeyDown(KeyCode.J) && Physics2D.OverlapCircle(param.target.GetComponent<PlayerControllerAnimator>().attackPoint.position, 
                                                                       param.target.GetComponent<PlayerControllerAnimator>().attackRange, 
                                                                       param.selfLayer))
            {
                manger.TransitionState(StateType.Hit);
            }
        }

        public void OnExit()
        {
            param.initiate = true;
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
            Debug.Log("Healing State");
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
            Debug.Log("DeathState");
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
