using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;
namespace Project
{
    public class GunSlingerFSM : MonoBehaviour
    {
        public Parameter param;
        private Istate currentState;
        private Dictionary<StateType, Istate> Enemystates = new Dictionary<StateType, Istate>();

        void Start()
        {

            Enemystates.Add(StateType.Idle, new GunDlingerIdleState(this));
            Enemystates.Add(StateType.Patrol, new GunDlingerPatrolState(this));
            Enemystates.Add(StateType.Chase, new GunDlingerChaseState(this));
            Enemystates.Add(StateType.React, new GunDlingerReactState(this));
            Enemystates.Add(StateType.Attack, new GunDlingerAttackState(this));
            Enemystates.Add(StateType.IsHit, new GunDlingerHitState(this));
            Enemystates.Add(StateType.Death, new GunDlingerDeathState(this));
            Enemystates.Add(StateType.teleport, new GunDlingerTeleState(this));
            TransitionState(StateType.Idle);
            param.animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (param.hp.GetComponent<Slider>().value > 0)
            {
                currentState.OnUpdate();
            }
            else
            {
                this.GetComponent<CapsuleCollider2D>().enabled = false;
                currentState = Enemystates[StateType.Death];
                currentState.OnEnter();
            }
        }

        public void TransitionState(StateType type)
        {
            if (currentState != null)
            {
                currentState.OnExit();
                param.hp = currentState.GetHp();
            }
            currentState = Enemystates[type];
            currentState.OnEnter();
            currentState.SetUp(param.hp);
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
                param.target.GetComponent<PlayerControllerAnimator>().TakeDamage(10);
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
}
