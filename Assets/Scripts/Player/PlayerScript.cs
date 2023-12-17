using Game;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        public float movementSpeed = 1f;
        public InputInfo inputInfo;
        public PlayerState state {get; private set;}
        public PlayerStateType stateType;

        private Dictionary<PlayerStateType, PlayerState> playerStates;

        private Animator animator;
        public Rigidbody2D rigidBody2D;
        public Collider2D bodyCollider;

        public static readonly string[] directions = { " N", " NW", " W", " SW", " S", " SE", " E", " NE" };
        public string animationName;
        public int animDir = 4;

        public SwordScript swordScript;

        InvinsibleFramesScript invinsibleFramesScript;

        private int hp = 100;

        public void AwakeManaged()
        {
            inputInfo = GetComponent<InputInfo>();
            animator = GetComponent<Animator>();
            rigidBody2D = GetComponent<Rigidbody2D>();
            bodyCollider = GetComponent<Collider2D>();
        }
        public void StartManaged()
        {
            playerStates = new Dictionary<PlayerStateType, PlayerState>()
            {
                [PlayerStateType.Run] = new MoveState(this),
                [PlayerStateType.Static] = new StaticState(this),
                [PlayerStateType.Attack] = new AttackState(this)
            };

            SetState(PlayerStateType.Static);
            swordScript.StartManaged();

            invinsibleFramesScript=this.AddComponent<InvinsibleFramesScript>();
            invinsibleFramesScript.OnTimerEnd += OnTimerEnd;
        }
        public void UpdateManaged()
        {
            
        }
        public void FixedUpdateManaged()
        {
            animator.Play(stateType.ToString() + directions[animDir]);
            state.FixedUpdateManaged();
            swordScript.FixedUpdateManaged();
        }

        public void SetState(PlayerStateType stateType)
        {
            this.stateType = stateType;
            state = playerStates[stateType];

            state.Init();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyWeapon"))
            {
                if (!invinsibleFramesScript.started)
                {
                    hp -= 10;
                    invinsibleFramesScript.StartTimer();
                    this.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    if (hp <= 0)
                    {
                        SceneManager.LoadScene("EndScene");
                    }
                }
            }
        }

        public void OnTimerEnd()
        {
            this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        }
    }
}