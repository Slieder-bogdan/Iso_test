using UnityEngine.PlayerLoop;

namespace Enemy
{
    public abstract class EnemyState
    {
        protected EnemyScript enemyScript;

        //protected InputInfo inputInfo;

        public EnemyState(EnemyScript enemyScript)
        {
            this.enemyScript = enemyScript;
        }
        
        public virtual void Init() { }

        public virtual void UpdateManaged() { }

        public virtual void FixedUpdateManaged() { }


    }
}