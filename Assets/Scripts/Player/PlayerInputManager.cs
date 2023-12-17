using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerScript playerScript;

    private PlayerInputAsset playerInputAsset;

    private InputAction moveAction;
    private InputAction attackAction;

    Vector2 prevoiusMoveInput;
    float prevoiusAttackInput;

    int currentDirection = 4;
    public void AwakeManaged()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();

        playerInputAsset = new PlayerInputAsset();

        moveAction = playerInputAsset.Player.Move;
        attackAction = playerInputAsset.Player.Attack;
    }
    public void UpdateManaged()
    {
        PollMoveInput();
        PollAttackInput();
    }

    private void PollMoveInput()
    {
        Vector2 currentMoveInput = moveAction.ReadValue<Vector2>();

        if (currentMoveInput.x > 0 && prevoiusMoveInput.x <= 0)
        {
            playerScript.state.SetHorizontalInput(1);
        }
        else if (currentMoveInput.x < 0 && prevoiusMoveInput.x >= 0)
        {
            playerScript.state.SetHorizontalInput(-1);

        }
        else if (currentMoveInput.x == 0 && prevoiusMoveInput.x != 0)
        {
            playerScript.state.SetHorizontalInput(0);

        }

        if (currentMoveInput.y > 0 && prevoiusMoveInput.y <= 0)
        {
            playerScript.state.SetVerticalInput(1);
        }
        else if (currentMoveInput.y < 0 && prevoiusMoveInput.y >= 0)
        {
            playerScript.state.SetVerticalInput(-1);
        }
        else if (currentMoveInput.y == 0 && prevoiusMoveInput.y != 0)
        {
            playerScript.state.SetVerticalInput(0);
        }
        GetDirection(currentMoveInput);
        prevoiusMoveInput = currentMoveInput;
    }
    private void PollAttackInput()
    {
        float currentAttackInput = attackAction.ReadValue<float>();
        if (currentAttackInput != prevoiusAttackInput)
        {
            playerScript.state.SetAttackInput(currentAttackInput);
        }
        prevoiusAttackInput = currentAttackInput;
    }

    private void GetDirection(Vector2 dir)
    {
        if (playerScript.stateType == PlayerStateType.Run && (dir.x!=0 || dir.y!=0))
        {
            Vector2 normDir = dir.normalized;
            float step = 360f / 8;
            float angle = Vector2.SignedAngle(Vector2.up, normDir);
            angle += step / 2;
            if (angle < 0)
            {
                angle += 360;
            }
            float stepCount = angle / step;
            currentDirection = Mathf.FloorToInt(stepCount);
            playerScript.animDir = currentDirection;
        }

    }

    private void OnEnable()
    {
        moveAction.Enable();
        attackAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        attackAction.Disable();
    }

}
