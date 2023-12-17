using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AttackState : PlayerState
    {
        float counter = 0.0f;
        Vector2 movement;

        public AttackState(PlayerScript playerScript) : base(playerScript) { }

        public override void Init()
        {
            movement= CalcDirectionVector();
        }

        public override void FixedUpdateManaged()
        {
            if (counter > 1.0f)
            {
                counter = 0.0f;
                playerScript.SetState(PlayerStateType.Static);
            }
            else
            {
                counter += Time.fixedDeltaTime;
                Vector2 attackVector = playerScript.rigidBody2D.position + movement * Time.fixedDeltaTime *2*Math.Abs(counter-0.9f);
                playerScript.rigidBody2D.MovePosition(attackVector);
            }
        }

        private Vector2 CalcDirectionVector()
        {
            Vector2 inputVector = new Vector2((float)(-1 * Math.Sin(playerScript.animDir * Math.PI / 4)), (float)(Math.Cos(playerScript.animDir * Math.PI / 4)) / 2);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * playerScript.movementSpeed;
            return movement;
        }
    }
}