using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : BaseState
{
    public override void onEnter(PlayerStateMachine player)
    {

    }

    public override void onExit(PlayerStateMachine player)
    {

    }

    public override void onFixedUpdate(PlayerStateMachine player)
    {
        checkInput(player);
    }

    // Check for input to switch to MovementState (WASD or Arrow Keys)
    void checkInput(PlayerStateMachine player)
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            player.SwitchState(player.movementState);
        }
    }

    public override void onUpdate(PlayerStateMachine player)
    {

    }
}
