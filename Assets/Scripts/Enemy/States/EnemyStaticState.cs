using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyStaticState : EnemyState
    {
        public EnemyStaticState(EnemyScript enemyScript) : base(enemyScript) { }

        public override void Init()
        {

        }

        public override void FixedUpdateManaged()
        {
        }
    }
}