using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttackState : EnemyState
    {
        float counter = 0.0f;
        Vector2 movement;

        public EnemyAttackState(EnemyScript enemyScript) : base(enemyScript) { }

        public override void Init()
        {
            movement = CalcDirectionVector();
        }

        public override void FixedUpdateManaged()
        {
            if (counter > 1.0f)
            {
                counter = 0.0f;
                enemyScript.SetState(EnemyStateType.Run);
            }
            else
            {
                counter += Time.fixedDeltaTime;
                Vector2 attackVector = enemyScript.rigidBody2D.position + movement * Time.fixedDeltaTime * 2 * Math.Abs(counter - 0.9f);
                enemyScript.rigidBody2D.MovePosition(attackVector);
            }
        }

        private Vector2 CalcDirectionVector()
        {
            Vector2 inputVector = new Vector2((float)(-1 * Math.Sin(enemyScript.animDir * Math.PI / 4)), 
                (float)(Math.Cos(enemyScript.animDir * Math.PI / 4)) / 2);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * enemyScript.movementSpeed;
            return movement;
        }
    }
}