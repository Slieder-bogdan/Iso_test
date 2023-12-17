using System;
using UnityEngine;

namespace Player
{
    public class SwordScript : MonoBehaviour
    {
        private PlayerScript playerScript;
        private Collider2D swordCollider;

        public void StartManaged()
        {
            playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
            swordCollider = GetComponent<Collider2D>();
        }

        public void FixedUpdateManaged()
        {
            this.gameObject.transform.eulerAngles=new Vector3(0,0,playerScript.animDir*45);
            if(playerScript.stateType==PlayerStateType.Attack) { swordCollider.enabled = true; }
            else { swordCollider.enabled = false; }
        }
    }
}