using UnityEngine;

public class MovementState : BaseState
{
    public override void onEnter(PlayerStateMachine player)
    {
        
    }

    public override void onExit(PlayerStateMachine player)
    {

    }

    public override void onFixedUpdate(PlayerStateMachine player)
    {
        // Read Input
        player.inputVertical = Input.GetAxis("Vertical");
        player.inputHorizontal = Input.GetAxis("Horizontal");

        // Handle Acceleration and Deceleration
        if (player.inputVertical > 0)
        {
            player.currentSpeed += player.acceleration * Time.fixedDeltaTime;
            player.currentSpeed = Mathf.Clamp(player.currentSpeed, -player.reverseSpeed, player.speed);
        }
        else if (player.inputVertical < 0)
        {
            player.currentSpeed -= player.acceleration * Time.fixedDeltaTime;
            player.currentSpeed = Mathf.Clamp(player.currentSpeed, -player.reverseSpeed, player.speed);
        }
        else
        {
            if (player.currentSpeed > 0)
            {
                player.currentSpeed -= player.deceleration * Time.fixedDeltaTime;
                player.currentSpeed = Mathf.Max(player.currentSpeed, 0);
            }
            else if (player.currentSpeed < 0)
            {
                player.currentSpeed += player.deceleration * Time.fixedDeltaTime;
                player.currentSpeed = Mathf.Min(player.currentSpeed, 0);
            }

            if (Mathf.Abs(player.currentSpeed) < 0.1f)
            {
                player.currentSpeed = 0;
                player.SwitchState(player.idleState);
            }
        }

        // Handle Rotation
        float rotation = player.inputHorizontal * player.rotationSpeed * Time.fixedDeltaTime;
        player.transform.Rotate(0, rotation, 0);

        // Apply Movement
        Vector3 movement = player.transform.forward * player.currentSpeed * Time.fixedDeltaTime;
        player.transform.position += movement;
    }

    public override void onUpdate(PlayerStateMachine player)
    {

    }
}
