using UnityEngine.PlayerLoop;

namespace Player
{
    public abstract class PlayerState
    {
        protected PlayerScript playerScript;

        protected InputInfo inputInfo;

        public PlayerState(PlayerScript playerScript)
        {
            this.playerScript = playerScript;
            inputInfo=playerScript.GetComponent<InputInfo>();
        }
        
        public virtual void Init() { }

        public virtual void UpdateManaged() { }

        public virtual void FixedUpdateManaged() { }

        public virtual void SetHorizontalInput(float inputValue)
        { 
            inputInfo.Move.x = inputValue;
        }
        public virtual void SetVerticalInput(float inputValue)
        {
            inputInfo.Move.y = inputValue;
        }
        public virtual void SetAttackInput(float inputValue)
        {
            inputInfo.Attack=inputValue;
        }

    }
}