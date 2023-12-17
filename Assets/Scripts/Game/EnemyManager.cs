using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class EnemyManager : MonoBehaviour
    {
        public string nextScene;
        public List<EnemyScript> enemies;
        bool areAnyEnemiesLeft;
        public void AwakeManaged()
        {
            foreach (var es in enemies)
            {
                es.AwakeManaged();
            }
        }

        public void StartManaged()
        {
            foreach (var es in enemies)
            {
                es.StartManaged();
            }
        }

        public void FixedUpdateManaged()
        {
            areAnyEnemiesLeft = false;
            foreach (var es in enemies)
            {
                if (es.gameObject.activeSelf)
                {
                    areAnyEnemiesLeft = true;
                    es.FixedUpdateManaged();
                }
            }
            if (!areAnyEnemiesLeft)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}