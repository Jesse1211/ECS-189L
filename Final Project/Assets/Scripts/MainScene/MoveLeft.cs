using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Captain.Command
{
    public class MoveLeft : ScriptableObject, IPlayerCommand
    {
        private float speed = 5.0f;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(-this.speed, rigidBody.velocity.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}