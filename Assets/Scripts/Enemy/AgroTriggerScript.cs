using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroTriggerScript : MonoBehaviour
{
    EnemyScript enemyScript;
    public void StartManaged()
    {
        enemyScript=GetComponentInParent<EnemyScript>();
    }
    public void FixedUpdateManaged()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& enemyScript.stateType!=EnemyStateType.Attack)
        {
            enemyScript.SetState(EnemyStateType.Run);
        }
    }
}
