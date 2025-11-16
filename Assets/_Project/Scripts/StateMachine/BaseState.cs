using UnityEngine;

public abstract class BaseState
{
    public abstract void onEnter(PlayerStateMachine stateMachine);
    public abstract void onUpdate(PlayerStateMachine stateMachine);
    public abstract void onFixedUpdate(PlayerStateMachine stateMachine);
    public abstract void onExit(PlayerStateMachine stateMachine);
}
