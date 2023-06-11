using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project { 
    public enum StateType
    {
        Idle, Patrol, Chase, React, Attack, Death, IsHit, teleport
    }

    [Serializable]
    public class Parameter
    {
        public GameObject hp;
        public float moveSpeed;
        public float chaseSpeed;
        public float idleTime;
        public Transform[] patrolPoints;
        public Transform[] chasePoints;
        public Transform[] telePoints;
        public Animator animator;
        public Transform target;
        public LayerMask targetLayer;
        public Transform attackPoint;
        public float attackArea;
        public bool getHit;
        public bool firstTele = false;
        public bool initTele = false;
    }

}
