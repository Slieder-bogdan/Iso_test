using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        PlayerInputManager playerInputManager;

        EnemyManager enemyManager;

        private PlayerScript playerScript;


        private void Awake()
        {
            playerInputManager = GetComponent<PlayerInputManager>();
            playerInputManager.AwakeManaged();

            enemyManager = GetComponent<EnemyManager>();
            enemyManager.AwakeManaged();

            playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
            playerScript.AwakeManaged();
        }

        void Start()
        {
            playerScript.StartManaged();

            enemyManager.StartManaged();
        }

        void Update()
        {
            playerInputManager.UpdateManaged();

            playerScript.UpdateManaged();
        }

        void FixedUpdate()
        {
            playerScript.FixedUpdateManaged();

            enemyManager.FixedUpdateManaged();
        }

        private void LateUpdate()
        {
            
        }
    }
}