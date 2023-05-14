using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Captain.Command;

namespace Captain.Command
{
    public class Jump : ScriptableObject, IJump
    {
        private float speed = 5.0f;
        private const int MAX_JUMP = 2;
        private const float JumpDuration = 1.0f;
        private const float restDuration = 1.5f;
        private int currentJump;
        private float timeCounter;
        private bool ableJump;

        public Jump()
        {
            currentJump = 0;
            ableJump = true;
            timeCounter = JumpDuration;
        }

        public void Execute(GameObject gameObject)
        {
            Debug.Log("Clicking jump:" + ableJump + timeCounter);

            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                if (timeCounter > 0 && currentJump < MAX_JUMP && ableJump) // Able to jump
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.y, this.speed);
                    currentJump++;
                }
            }            
        }

        public void countingTime()
        {
            // timeCounter updates if haven't jump, finished jump, or finished rest
            if (ableJump && currentJump == 0) // haven't jump yet
            {
                timeCounter = JumpDuration;
            }
            else if(ableJump && timeCounter < 0) // too late to jump
            {
                timeCounter = JumpDuration;
            }
            else if (ableJump && currentJump == 2) // jumped twice, need to rest
            {
                Debug.Log("NO MORE JUMP");
                ableJump = false;
                timeCounter = restDuration;
            }
            else if (!ableJump && timeCounter < 0) // finished rest, able to jump
            {
                Debug.Log("Now you can jump");
                ableJump = true;
                timeCounter = JumpDuration;
                currentJump = 0;
            }
            else
            {
                timeCounter -= Time.deltaTime;
            }
        }
    }
}