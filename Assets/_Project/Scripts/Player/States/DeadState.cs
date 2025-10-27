using UnityEngine;

public class DeadState : BaseState
{
    public override void onEnter(PlayerStateMachine player)
    {
        ScoreManager.instance.updateScore();
        player.currentSpeed = 0f;
        player.turret.setAlive(false);
    }

    public override void onExit(PlayerStateMachine player)
    {

    }

    public override void onFixedUpdate(PlayerStateMachine player)
    {

    }

    public override void onUpdate(PlayerStateMachine player)
    {

    }
}
