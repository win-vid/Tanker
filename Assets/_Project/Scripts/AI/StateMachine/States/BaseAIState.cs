using UnityEngine;

public abstract class BaseAIState
{
    public abstract void onEnter(AIStateMachine stateMachine);
    public abstract void onUpdate(AIStateMachine stateMachine);
    public abstract void onFixedUpdate(AIStateMachine stateMachine);
    public abstract void onExit(AIStateMachine stateMachine);
}
