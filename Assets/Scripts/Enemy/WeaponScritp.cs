using System;
using UnityEngine;

namespace Enemy
{
    public class WeaponScript : MonoBehaviour
    {
        private EnemyScript enemyScript;
        private Collider2D weaponCollider;

        public void StartManaged()
        {
            enemyScript = GetComponentInParent<EnemyScript>();
            weaponCollider = GetComponent<Collider2D>();
        }

        public void FixedUpdateManaged()
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, enemyScript.animDir * 45);
            if (enemyScript.stateType == EnemyStateType.Attack) { weaponCollider.enabled = true; }
            else { weaponCollider.enabled = false; }
        }
    }
}