using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyRunState : EnemyState
    {
        PlayerScript playerScript;
        public EnemyRunState(EnemyScript enemyScript) : base(enemyScript) { }
        
        public override void Init()
        {
            playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        }

        public override void FixedUpdateManaged()
        {
            Vector2 inputVector = new Vector2
                (playerScript.rigidBody2D.position.x - enemyScript.rigidBody2D.position.x,
                (playerScript.rigidBody2D.position.y - enemyScript.rigidBody2D.position.y) / 2);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * enemyScript.movementSpeed;
            Vector2 newPos = enemyScript.rigidBody2D.position + movement * Time.fixedDeltaTime;
            ChangeDirection(movement);
            enemyScript.rigidBody2D.MovePosition(newPos);
        }

        private void ChangeDirection(Vector2 dir)
        {
            Vector2 normDir = dir.normalized;
            float step = 360f / 8;
            float angle = Vector2.SignedAngle(Vector2.up, normDir);
            angle += step / 2;
            if (angle < 0)
            {
                angle += 360;
            }
            float stepCount = angle / step;
            enemyScript.animDir = Mathf.FloorToInt(stepCount);
        }

    }
}