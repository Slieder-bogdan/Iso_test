using Game;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class EnemyScript : MonoBehaviour
    {
        public float movementSpeed = 1f;
        public EnemyState state { get; private set; }
        public EnemyStateType stateType;

        private int hp = 100;

        private Dictionary<EnemyStateType, EnemyState> enemyStates;

        private Animator animator;
        public Rigidbody2D rigidBody2D;
        public Collider2D bodyCollider;

        public static readonly string[] directions = { " N", " NW", " W", " SW", " S", " SE", " E", " NE" };
        public string animationName;
        public int animDir = 4;

        public WeaponScript weaponScript;

        AgroTriggerScript agroTriggerScript;
        AttackTriggerScript attackTriggerScript;

        InvinsibleFramesScript invinsibleFramesScript;

        public void AwakeManaged()
        {
            animator = GetComponent<Animator>();
            rigidBody2D = GetComponent<Rigidbody2D>();
            bodyCollider = GetComponent<Collider2D>();
            agroTriggerScript = GetComponentInChildren<AgroTriggerScript>();
            attackTriggerScript = GetComponentInChildren<AttackTriggerScript>();
        }
        public void StartManaged()
        {
            enemyStates = new Dictionary<EnemyStateType, EnemyState>()
            {
                [EnemyStateType.Run] = new EnemyRunState(this),
                [EnemyStateType.Static] = new EnemyStaticState(this),
                [EnemyStateType.Attack] = new EnemyAttackState(this)
            };
            agroTriggerScript.StartManaged();
            attackTriggerScript.StartManaged();
            SetState(EnemyStateType.Static);
            weaponScript.StartManaged();

            invinsibleFramesScript = this.AddComponent<InvinsibleFramesScript>();
            invinsibleFramesScript.OnTimerEnd += OnTimerEnd;
        }
        public void UpdateManaged()
        {

        }
        public void FixedUpdateManaged()
        {
            animator.Play(stateType.ToString() + directions[animDir]);
            state.FixedUpdateManaged();
            weaponScript.FixedUpdateManaged();
        }

        public void SetState(EnemyStateType stateType)
        {
            this.stateType = stateType;
            state = enemyStates[stateType];

            state.Init();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayerWeapon"))
            {
                hp -= 10;
                invinsibleFramesScript.StartTimer();
                this.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                if (hp <= 0)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
        public void OnTimerEnd()
        {
            this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        }
    }
}