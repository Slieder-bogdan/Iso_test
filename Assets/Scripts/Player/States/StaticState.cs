using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class StaticState : PlayerState
    {
        public StaticState(PlayerScript playerScript) : base(playerScript) { }

        public override void Init()
        {

        }

        public override void FixedUpdateManaged()
        {
            if (playerScript.inputInfo.Attack!=0)
            {
                playerScript.SetState(PlayerStateType.Attack);
            }
            else if (playerScript.inputInfo.Move.x != 0 || playerScript.inputInfo.Move.y != 0)
            {
                playerScript.SetState(PlayerStateType.Run);
            }
        }
    }
}