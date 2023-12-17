using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MoveState : PlayerState
    {
        public MoveState(PlayerScript playerScript):base(playerScript) { }

        public override void Init()
        {

        }

        public override void FixedUpdateManaged()
        {
            Vector2 inputVector = new Vector2(playerScript.inputInfo.Move.x, playerScript.inputInfo.Move.y / 2);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * playerScript.movementSpeed;
            Vector2 newPos = playerScript.rigidBody2D.position + movement * Time.fixedDeltaTime;
            playerScript.rigidBody2D.MovePosition(newPos);
            if (playerScript.inputInfo.Attack != 0)
            {
                playerScript.SetState(PlayerStateType.Attack);
            }
            else if (inputVector.x==0&&inputVector.y==0)
            {
                playerScript.SetState(PlayerStateType.Static);
            }
        }
    }
}